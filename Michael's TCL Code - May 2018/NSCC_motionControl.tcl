#### This file contains procedures for controlling the robot's motion ####


# move forward
proc moveForward {} {	
	global currentSpeed
	set_speed y $currentSpeed	
}


#move backward
proc moveBackward {} {	
	global currentSpeed
	set_speed y [expr {$currentSpeed * -1}]	
}


# Rotate Right
proc rotateRight {} {
	global currentSpeed 
	set_speed r [expr {$currentSpeed * -1}]	
}


# Rotate Left
proc rotateLeft {} {
	global currentSpeed
	set_speed r $currentSpeed	
}


# Strafe Right
proc moveRight {} {
	global currentSpeed
	set_speed x $currentSpeed	
}


# Strafe Left
proc moveLeft {} {
	global currentSpeed
	set_speed x [expr {$currentSpeed * -1}]	
}