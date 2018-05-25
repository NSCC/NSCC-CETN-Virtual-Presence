#### This file handles playing sound files ####
#### Currently setup to use mp3, but other options are available

# This package is for playing sounds
package require snack 2.2

# Set directories where sound collections are located
set soundDir0 ".\\NSCC_sounds\\C3PO_sounds\\"
set soundDir1 ".\\NSCC_sounds\\LostInSpace_sounds\\"
set soundDir2 ".\\NSCC_sounds\\Dalek_sounds\\"

# C3PO's sounds (file names without extension)
set c3po {{All systems} {chances} {Desolate place} {Im C3PO} {Made to suffer} {Madmen} {Overweight} {Regret} {Watch your language} {Where are you}}

# Lost In Space sounds (file names without extensions)
set lostInSpace {{Affirmative} {Danger danger 2} {Great deal in common} {Harm human beings} {I am sorry} {I do not sleep} {I have only you} {Programmed for more important work} {That does not compute} {Wasting our time}}

# Dalek sounds (file names without extensions)
set dalek {{Destroy} {exterminate} {Go stronger} {groan} {gun} {I am dalek} {Message} {Must Survive} {Rant} {Stay}}

proc playSound {data} {
	global soundDir0 soundDir1 soundDir2 c3po dalek lostInSpace soundBank
	
	# select the sound file based on the currently selected bank and the data received from the client
	if {$soundBank == 0} {
		set sounds $soundDir0
		set soundList $c3po
	} elseif {$soundBank == 1} {
		set sounds $soundDir1
		set soundList $lostInSpace
	} elseif {$soundBank == 2} {
		set sounds $soundDir2
		set soundList $dalek
	}
	set soundNum [string index $data 1]
	
	#create the sound objetc and play it
	snack::sound s -file "$sounds[lindex $soundList $soundNum].mp3"
	s play -blocking 0
	
	
}

proc setSoundBank {bank} {
	global soundBank
	set soundBank $bank	
}
