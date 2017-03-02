#!/bin/sh
# the next line restarts using wish \
exec wish8.4 "$0" "$@"

package require Iwidgets 
#package require -exact snack 2.2
package require BWidget 

source "./parse.tcl"
source "./system.tcl"
source "./motors.tcl"
source "./digital.tcl"
source "./analog.tcl"
source "./susi.tcl"
source "./camera.tcl"	;   # SEB addition
source "./eapi.tcl"
source "./aware.tcl"	; # For RP-VITA, iRobot aware interface

# Create GUI

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
wm title .t "Test Program"

# Make Window Default to Maximum Sized
#wm attributes .t -fullscreen 1
wm attributes .t -topmost yes
wm state .t zoomed

# Size Parent Window . to non-visible
wm overrideredirect . yes
wm geometry . 1x1+0+0

# Create a Text Window for System Responses
iwidgets::scrolledtext .t.response -labeltext "Response" -wrap none\
  -vscrollmode static -hscrollmode static \
  -width 5i -height 2i\
  -labelfont "$ft italic" \
  -textfont $ft
	
# Create a Text Window to Caputre UART transactions
iwidgets::scrolledtext .t.uart -labeltext "UART" -wrap none \
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

# initialize the bot
initialize

.t.response insert end "complete.\n" GOOD
.t.response yview end

# test 1 - move bot ahead
#set_speed y 0.1
#after 5000
#set_speed y 0

bind .t "<Key w>" {
set_speed y 1
after 250
slow_stop y 1}

bind .t "<Key s>" {
set_speed y -1
after 250
slow_stop y 1}

bind .t "<Key a>" {
set_speed x -1
after 250
slow_stop x 1}

bind .t "<Key d>" {
set_speed x 1
after 250
slow_stop x 1}

bind .t "<Key e>" {
set_speed r -1
after 250
slow_stop r 1}

bind .t "<Key q>" {
set_speed r 1
after 250
slow_stop r 1}


bind .t "<Key l>" {
incr_vel pan
incr_vel pan
incr_vel pan
incr_vel pan
after 250
slow_stop pan 1}

bind .t "<Key j>" {
decr_vel pan
decr_vel pan
decr_vel pan
decr_vel pan
after 250
slow_stop pan 1}

bind .t "<Key k>" {
incr_vel tilt
incr_vel tilt
incr_vel tilt
incr_vel tilt
after 250
slow_stop tilt 1}

bind .t "<Key i>" {
decr_vel tilt
decr_vel tilt
decr_vel tilt
decr_vel tilt
after 250
slow_stop tilt 1}