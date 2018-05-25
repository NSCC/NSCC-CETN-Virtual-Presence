#!/bin/sh
# the next line restarts using wish \
exec wish8.4 "$0" "$@"

package require Iwidgets 
package require snack 2.2
package require BWidget 

source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/parse.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/system.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/motors.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/digital.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/analog.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/susi.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/camera.tcl"	;   # SEB addition
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/eapi.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/aware.tcl"	; # For RP-VITA, iRobot aware interface


source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/NSCC_sound.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/NSCC_tts.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/NSCC_headControl.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/NSCC_motionControl.tcl"
source "C:/Program Files/Intouch Health/Installation Utilities/RP Test/NSCC_sensors.tcl"

set interfaceOnly 0

#########################################################
############### CODE TO CREATE THE GUI ##################
#########################################################

# Set a Global Font for Labels, Buttons, Other Widgets.
set ft "{Inconsolata} 24 bold"
option add *font $ft

# Set a Global Background Color
set bgc	"lightgrey"
set ebg "white"
option add *background $bgc
option add *backdrop $bgc

# Set Time
set t [clock format [clock seconds] -format "%b %d %Y"]

# Create Top Level Window, with focus control
toplevel .t -class TopLevel -takefocus 1

# Title for Top Level Window
wm title .t "NSCC RP Application"

# Make Window Default to Maximum Sized
#wm attributes .t -fullscreen 1
wm attributes .t -topmost yes
#wm state .t zoomed

# Size Parent Window . to non-visible
wm overrideredirect . yes
wm geometry . 1x1+0+0

# Handles clicking the window's close button to shut down the application properly
wm protocol .t WM_DELETE_WINDOW {
		# Create MessageBox to Determine if User Wants to Exit
		if {[tk_messageBox -icon question \
				-type yesno -default no \
				-message "Do you wish to Exit?" \
				-title   "Quit Application?"] == "yes"} {
	            quitApp;	# Run Exit Routine
		}
	}

# Create a Text Window for System Responses
iwidgets::scrolledtext .t.response -labeltext "Response" -wrap none\
  -vscrollmode static -hscrollmode static \
  -width 5i -height 2i\
  -labelfont "$ft italic" \
  -textfont $ft
	
# Create a Text Window to Caputre UART transactions
iwidgets::scrolledtext .t.uart -labeltext "HI TODD" -wrap none \
	-vscrollmode static -hscrollmode static \
	-width 5i -height 2i\
	-labelfont "$ft italic" \
	-textfont $ft 
	
# Configure Tags
.t.response tag configure ERROR -background red
.t.response tag configure PASS	-foreground green
.t.response tag configure FAIL	-foreground red
.t.response tag configure GOOD	-background green

#grid configure .t.response 	-column 0 -row 0 -padx .2c -pady .2c
pack .t.response -fill both -expand true
#grid configure .t.uart 		-column 1 -row 0 -padx .2c -pady .2c
pack .t.uart -fill both -expand true

system_msg "Initializing..." 0


##############################################################################
###### FROM THIS POINT ON WE ARE DEALING WITH CODE TO CONTROL THE ROBOT ######
##############################################################################

# initialize the bot
initialize

# turn on the speaker
set eapi(initialized) 1


# TRYING TO TURN ON MICROPHONE


.t.response insert end "complete.\n" GOOD
.t.response yview end


# function to center head on start-up
proc centerHead {} {
	global motor
	set motor(smoothpos) 1
	calibrate_vel pt
	center_axis tilt
	center_axis pan
	set motor(pan.pos.p1) 0
	set motor(tilt.pos.p1) 0
}


# function to set default values for the pan and tilt motors (haven't played around with these values yet)
proc setUpPanTiltMotors {} {
	global motor
	update_oas $motor(all.hexchan) $motor(offatstop)    
	set motor(trap) 1
	set motor(tilt.pos.en) 1
	set motor(pan.pos.en) 1
	set motor(tilt.pos.time) 2000
	set motor(pan.pos.time) 2000
	set motor(tilt.pos.vel) 100
	set motor(pan.pos.vel) 100
}


# set the speed (maybe this might be able to be changed by the user eventually) 
set currentSpeed 0.5

# set the default sound bank
set soundBank 0

# tracks whether or not we are connecte dto the client
set disconnected 1

# center the head at start
centerHead

# ready the head's pan and tilt motors
setUpPanTiltMotors

# Allows the server to listen for an incoming connection
proc server {chan addr port} {
	global motor disconnected soundBank
	
	# configure the socket and how received messages are handled
	fconfigure $chan -blocking 0 
	fileevent $chan readable [list server_eval $chan]
	
	# Conform connection success by sending hello
	puts -nonewline $chan "\"Hello!\" from the Robot"	
	flush $chan
	after 20
	
	set disconnected 0
	
	# reset sound bank in case of a reconnect
	set soundBank 0
	
	# Send current head position in case of a reconnect
	puts -nonewline $chan "pan$motor(pan.pos.p1)"
	flush $chan
	after 20
	
	# Store the socket in a variable to be used globally
	set ::sock $chan
	
	#start polling the battery
	after 100 checkBattery
	
	
	# write connection message to the display
	.t.response insert end "Client has connected.\n" GOOD
}

# variables to keep track of which way the robot is moving
set isMovingForward false
set isMovingBackward false
set isMovingLeft false
set isMovingRight false
set isRotatingLeft false
set isRotatingRight false


# Once a connection is made, this is where commands are received and processed
proc server_eval {chan} {

	global isMovingForward isMovingBackward isMovingLeft isMovingRight isRotatingLeft isRotatingRight disconnected headIsMoving
	
	# TRYING TO CHECK FOR A LOST CONNECTION
	#set err [fconfigure $chan -error]
	#.t.response insert end "ERROR: $err"
	
	if {![eof $chan]} {		
		
		# read from the stream, if there is an error everything stops
		if { [catch {read $chan} msg] } {
			stopAll			
		}			
		
		# The socket has been closed unintentionally
		# NOTE: If the network connection is lost, this takes a while (approx. 10secs)
		if {$msg < 0} {
			stopAll
			set disconnected 1
			.t.response insert end "Connection to client has been lost.\n" 
		}
		
		#echo back the command for testing 
		#puts -nonewline $chan "Command Received = $msg\n"	
		#flush $chan		
		
		# Handle the incoming commands
		if {$msg ==	"w"} {
			if {![checkFrontBlocked]} {
				set isMovingForward true
				moveForward					
				sendResponse Forward
			} else {
				sendResponse "Front Blocked"
			}		
		}
		if {$msg == "s"} {			
			if {![checkRearBlocked]} {
				set isMovingBackward true
				moveBackward
				sendResponse Backward
			} else {
				sendResponse "Rear Blocked"
			}		
		}
		if {$msg ==	"a"} {
			if {![checkRotateLeft]} {
				set isRotatingLeft true
				rotateLeft
				sendResponse "Rotate Left"
			} else {
				sendResponse "Left Rotation Blocked"
			}		
		}
		if {$msg ==	"d"} {
			if {![checkRotateRight]} {
				set isRotatingRight true
				rotateRight
				sendResponse "Rotate Right"
			} else {
				sendResponse "Right Rotation Blocked"
			}		
		}
		if {$msg ==	"q"} {
			if {![checkLeftBlocked]} {
				set isMovingLeft true
				moveLeft
				sendResponse "Strafe Left"
			} else {
				sendResponse "Left Blocked"
			}			
		}
		if {$msg ==	"e"} {
			if {![checkRightBlocked]} {
				set isMovingRight true
				moveRight
				sendResponse "Strafe Right"
			} else {
				sendResponse "Right Blocked"
			}			
		}
		if {$msg ==	"i"} {
			tiltUp			
			sendResponse "Tilt Head Up"
		}
		if {$msg ==	"k"} {
			tiltDown
			sendResponse "Tilt Head Down"
		}
		if {$msg ==	"j"} {
			panLeft
			sendResponse "Pan Head Left"
		}
		if {$msg ==	"l"} {
			panRight
			sendResponse "Pan Head Right"
		}			
		if {[string index $msg 0] == "h"} {
			sendResponse "Move Head"
			moveHead $msg			
		}		
		if {$msg ==	"cen" || $msg == "space"} {
			sendResponse "Centering Head"
			# Need to do this several times to get to center for some reason
			#centerHead
			#centerHead
			#centerHead	
			moveHeadToCenter
			
		}
		if {$msg ==	"stop"} {
			stopAll
			sendResponse "Stop"
			set isMovingForward false
			set isMovingBackward false
			set isMovingLeft false
			set isMovingRight false
			set isRotatingLeft false
			set isRotatingRight false			
		}
		if {[string index $msg 0 ] == "p"} {
			playSound $msg
		}
		if {[string range $msg 0 1] == "##"} {
			speakMessage [string range $msg 2 [string length $msg]]
		}
		if {[string index $msg 0] == "B"} {
			setSoundBank [string index $msg 1]
		}
		if {$msg ==	"quit"} {
			stopAll
			close $chan
			set disconnected 1
			.t.response insert end "Client diconnected.\n"			
		}
		
	} else {
		stopAll
		close $chan
	}	
}


# Sends a reponse message to the client
proc sendResponse {msg} {
	puts -nonewline $::sock $msg
	flush $::sock
	after 50
}


# Stop all motors
proc stopAll {} {
	set_speed y 0	
	set_speed x 0
	set_speed r 0
	#slow_stop pan 1
	#slow_stop tilt 1
}


# Quit the application
proc quitApp {} {
	
	# send quit message to the client
	if {[catch {puts $::sock "quit"}]} {
		.t.response insert end "Connection lost\n"
	}
	if {[catch {flush $::sock}]} {
		.t.response insert end "Connection lost\n"
	}
	#close the socket
	if {[catch {close $::sock}]} {
		.t.response insert end "Connection lost\n"
	}
	
	stopAll	
	quit
}


# Polls the sensors to stop motors if something impedes the path while in motion
proc checkSensors {} {
	global isMovingForward isMovingBackward isMovingLeft isMovingRight isRotatingLeft isRotatingRight
	
	if {$isMovingForward && [checkFrontBlocked]} {		
		.t.response insert end "BLOCKED FRONT\n"		
		set_speed y 0
	} elseif {$isMovingBackward && [checkRearBlocked]} {
		.t.response insert end "BLOCKED REAR\n"		
		set_speed y 0
	} elseif {$isMovingLeft && [checkLeftBlocked]} {
		.t.response insert end "BLOCKED LEFT\n"		
		set_speed x 0
	} elseif {$isMovingRight && [checkRightBlocked]} {
		.t.response insert end "BLOCKED RIGHT\n"		
		set_speed x 0		
	} elseif {$isRotatingLeft && [checkRotateLeft]} {
		.t.response insert end "BLOCKED ROTATE LEFT\n"		
		set_speed r 0
	} elseif {$isRotatingRight && [checkRotateRight]} {
		.t.response insert end "BLOCKED ROTATE RIGHT\n"		
		set_speed r 0
	}
	
	# refresh the GUI
	update
	
	# call this function in a loop
	after 0 checkSensors
}

proc checkBattery {} {
	global adc disconnected
	
	if {$disconnected == 1} {
		return
	} else {
		puts -nonewline $::sock "batt$adc(battery)"
		flush $::sock
		after 30000 checkBattery
	}	
}


# Start the server on localhost:33000
set server [socket -server server 33000]
.t.response insert end "Server is listening on port 33000.\n"

# start checking the IR sensors
after 0 checkSensors




