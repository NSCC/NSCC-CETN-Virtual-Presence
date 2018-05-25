##### This file contains procedures for moving the robot's head #####

set pan_increment 5
set tilt_increment 2
set max_pan 115
set max_tilt 30

# Tilt Head Upward
proc tiltUp {} {

	global motor max_tilt tilt_increment
	
	# The old way
	#decr_vel tilt
	
	if {$motor(tilt.pos.p1) > [expr $max_tilt * -1]} {
		set motor(tilt.pos.next) [expr $motor(tilt.pos.p1) - $tilt_increment]
		trap_axis tilt
		set motor(tilt.pos.p1) $motor(tilt.pos.next)
	}	
}


# Tilt Head Downward
proc tiltDown {} {

	global motor max_tilt tilt_increment

	# The old way
	#incr_vel tilt
	
	if {$motor(tilt.pos.p1) < $max_tilt} {
		set motor(tilt.pos.next) [expr $motor(tilt.pos.p1) + $tilt_increment]
		trap_axis tilt
		set motor(tilt.pos.p1) $motor(tilt.pos.next)
	}		
}


# Pan Head Right
proc panRight {} {
	global motor max_pan pan_increment
	
	# The old way
	#incr_vel pan 
	
	if {$motor(pan.pos.p1) < $max_pan} {
		set motor(pan.pos.next) [expr $motor(pan.pos.p1) + $pan_increment]
		trap_axis pan
		set motor(pan.pos.p1) $motor(pan.pos.next)
	}	
	
	# send back the pan position so that we can update the GUI
	puts -nonewline $::sock "pan$motor(pan.pos.p1)"
	flush $::sock
}


# Pan Head Left
proc panLeft {} {
	global motor max_pan pan_increment
	
	# The old way
	#decr_vel pan 
	
	if {$motor(pan.pos.p1) > [expr $max_pan * -1]} {
		set motor(pan.pos.next) [expr $motor(pan.pos.p1) - $pan_increment]
		trap_axis pan
		set motor(pan.pos.p1) $motor(pan.pos.next)	
	}	
	
	#send back the pan position so we can update the GUI
	puts -nonewline $::sock "pan$motor(pan.pos.p1)"
	flush $::sock
}


# controls the movement of the head from mouse input
proc moveHead {data} {

	global motor max_pan max_tilt
	
	# Get the new x and y positions from the command data
	set y [parseDataY $data]
	set x [parseDataX $data]	
	
	# Set the new y position (checking to make sure we don't try to move too far causing an error)
	if {$motor(tilt.pos.p1) <= $max_tilt && $motor(tilt.pos.p1) >= [expr $max_tilt * -1]} {
		set motor(tilt.pos.next) [expr $motor(tilt.pos.p1) + $y]
	} else {
		if {$motor(tilt.pos.p1) < 0} {
			set motor(tilt.pos.next) [expr $max_tilt * -1]
		} else {
			set motor(tilt.pos.next) $max_tilt
		}		
	}
	
	# Set the new x position (checking to make sure we don't try to move too far causing an error)
	if {$motor(pan.pos.p1) <= $max_pan && $motor(pan.pos.p1) >= [expr $max_pan * -1]} {
		set motor(pan.pos.next) [expr $motor(pan.pos.p1) + $x]
	} else {
		if {$motor(pan.pos.p1) < 0} {
			set motor(pan.pos.next) [expr $max_pan * -1]
		} else {
			set motor(pan.pos.next) $max_pan
		}		
	}
	
	# Move to the new position
	trap_axis tilt
	trap_axis pan
	
	# update the current positions with the new positions
	set motor(tilt.pos.p1) $motor(tilt.pos.next)
	set motor(pan.pos.p1) $motor(pan.pos.next)
	
	# send back the pan position so we can update the GUI
	puts -nonewline $::sock "pan$motor(pan.pos.p1)"
	flush $::sock
}

proc moveHeadToCenter {} {
	
	global motor
	
	# set next position to center
	set motor(tilt.pos.next) 0
	set motor(pan.pos.next) 0
	
	# move the head
	trap_axis tilt
	trap_axis pan
	
	# update current position
	set motor(tilt.pos.p1) $motor(tilt.pos.next)
	set motor(pan.pos.p1) $motor(pan.pos.next)
	
	# send back the pan position so we can update the GUI
	puts -nonewline $::sock "pan$motor(pan.pos.p1)"
	flush $::sock
	
}

# parses the x value from the move head command
proc parseDataX {data} {
	set indexY [string first "y" $data ]
	set x [string range $data 2 [expr $indexY - 1]]	
	return $x
}


# parses the y value from the move head command
proc parseDataY {data} {
	set indexY [string first "y" $data] 
	set l [string length $data]
	set y [string range $data [expr $indexY + 1] [expr $l - 1]]
	return $y
}