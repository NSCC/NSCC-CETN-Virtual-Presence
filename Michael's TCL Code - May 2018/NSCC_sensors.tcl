#### This file contains procedures for checking the IR sensors ####

# sets sensor sensitivity
# Accepts values from 0 to 8191
set sensitivity 3000

# check to see if any front sensors detect an obstacle
proc checkFrontBlocked {} {
	global adc sensitivity
	set blocked false
	# check all front base sensors (0,1,27,28)
	if { $adc(ir.0) > $sensitivity || $adc(ir.1) > $sensitivity || $adc(ir.27) > $sensitivity || $adc(ir.28) > $sensitivity} {
		set blocked true
	}
	# check all front waist sensors (0,1,6,7)
	if {$adc(ir.29) > $sensitivity || $adc(ir.30) > $sensitivity || $adc(ir.35) > $sensitivity || $adc(ir.36) > $sensitivity} {
		set blocked true
	}	
	return $blocked
}


# check to see if any rear base sensors detect an obstacle
proc checkRearBlocked {} {
	global adc sensitivity
	set blocked false
	# check all rear base sensors (10 - 18)
	for {set i 10} {$i < 19} {incr i} {
		if {$adc(ir.$i) > $sensitivity} {
			set blocked true
		}
	}	
	return $blocked
}


# check to see if any left side sensors detect an obstacle
proc checkLeftBlocked {} {
	global adc sensitivity
	set blocked false
	# check all left side base sensors (18 - 27)
	for {set i 18} {$i < 28} {incr i} {
		if {$adc(ir.$i) > $sensitivity} {
			set blocked true
		}
	}
	# check all left side waist sensors (4,5)
	if {$adc(ir.33) > $sensitivity || $adc(ir.34) > $sensitivity} {
		set blocked true
	}
	return $blocked
}


# check to see if any right side sensors detect an obstacle 
proc checkRightBlocked {} {
	global adc sensitivity
	set blocked false
	# check all right side base sensors (1-10)
	for {set i 1} {$i < 11} {incr i} {
		if {$adc(ir.$i) > $sensitivity} {
			set blocked true
		}	
	}
	# check all right side waist sensors (2,3)
	if {$adc(ir.31) > $sensitivity || $adc(ir.32) > $sensitivity} {
		set blocked true
	}
	return $blocked
}


# check to see if clear to rotate left
proc checkRotateLeft {} {
	global adc sensitivity
	set blocked false
	# check left sensors
	if {[checkLeftBlocked]} {
		set blocked true
	}
	# check rear left sensors
	for {set i 16} {$i < 19} {incr i} {
		if {$adc(ir.$i) > $sensitivity} {
			set blocked true
		}
	}
	return $blocked
}


# check to see if clear to rotate right
proc checkRotateRight {} {
	global adc sensitivity
	set blocked false
	# check right side sensors
	if {[checkRightBlocked]} {
		set blocked true
	}
	# check rear right sensors
	for {set i 10} {$i < 13} {incr i} {
		if {$adc(ir.$i) > $sensitivity} {
			set blocked true
		}
	}
	return $blocked
}