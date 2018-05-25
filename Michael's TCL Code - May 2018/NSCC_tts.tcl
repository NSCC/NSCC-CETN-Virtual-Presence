#### This file deals with the text to speech functionality ####

package require tcom

# creates the text to speech object
set voice [::tcom::ref createobject Sapi.SpVoice]

# a list of available voices
set voiceList [$voice GetVoices]

# set a voice to use from the list
set person [$voiceList Item 1]

# set the voice to use
$voice Voice $person

# sets voice's speed
$voice Rate -4

proc speakMessage {msg} {
	global voice
	$voice Speak $msg
}

