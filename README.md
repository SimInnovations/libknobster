LibKnobster
===========

Library used to connect to a Knobster.

The Knobster is a single knob that allows realistic operation of every dial / rotary switch / knob on your Air Manager or Air Player cockpit panel. It works in touch or mouse control mode and emulates single and dual rotary encoders including an integral push button. This low-cost hardware makes a touchscreen panel almost as realistic as a hardware sim.

## Screenshot

![Screenshot of the Knobster](/knobster.jpg?raw=true)

Usage
=====

Right now only the Windows platform is supported.
Open up the Visual Studio 2019 project in the 'vs2019' folder. There are no external dependencies.

API
=====

Main API functions:
```C
// Scan for Knobsters on USB ports. len is the max number of Knobsters you want to search for.
int libknobster_scan(struct Knobster** knobster_list, int len);

// Connect to the Knobster
// Returns 0 on Success, otherwise failed to connect
int libknobster_connect(struct Knobster* knobster);

// Disconnect the Knobster
void libknobster_disconnect(struct Knobster* knobster);

// Poll Knobster for new events
enum KnobsterEvent libknobster_poll(struct Knobster* knobster);

// Close a Knobster
void libknobster_close(struct Knobster* knobster);
```

Events returned by 'libknobster_poll'
```C
enum KnobsterEvent {
	KNOBSTER_EVENT_NOT_CONNECTED,      // Knobster is still in non connected state. 'libknobster_connect' has not been called.	

	KNOBSTER_EVENT_INITIALIZING,       // Connection with the Knobster is being initialized

	KNOBSTER_EVENT_ERROR_NO_RESPONSE,  // Connection error, Knobster did not respond with correct internal message
	KNOBSTER_EVENT_ERROR_TRANSFER,     // Connection error, something went from with the USB communication

	KNOBSTER_EVENT_NO_EVENT,           // Connected, but nothing is happening with the knobster
	KNOBSTER_EVENT_DIAL_MINOR_CW,      // The minor dial is turned clockwise
	KNOBSTER_EVENT_DIAL_MINOR_CCW,     // The minor dial is turned counterclockwise
	KNOBSTER_EVENT_DIAL_MAJOR_CW,      // The major dial is turned clockwise
	KNOBSTER_EVENT_DIAL_MAJOR_CCW,     // The major dial is turned counterclockwise
	KNOBSTER_EVENT_BUTTON_PRESSED,     // The button is pressed
	KNOBSTER_EVENT_BUTTON_RELEASED     // The button is released
};
```

Example
=======



License
=======

There is no license in place. You can use this software as you please.
