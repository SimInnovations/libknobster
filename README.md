LibKnobster
===========

Library used to connect to a Knobster.

The Knobster is a single knob that allows realistic operation of every dial / rotary switch / knob on your Air Manager or Air Player cockpit panel. It works in touch or mouse control mode and emulates single and dual rotary encoders including an integral push button. This low-cost hardware makes a touchscreen panel almost as realistic as a hardware sim.

## Screenshot

![Screenshot of the Knobster](/knobster.jpg?raw=true)

Usage
=====

Right now only the Windows platform is supported.

Open up the Visual Studio 2019 project in the 'vs2019' folder.

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

// Get knobster channel (0x00='A' through 0x0F='P')
// Returns -1 when channel is not known, channel is available when 'KNOBSTER_EVENT_CONNECTED' event is fired
int8_t libknobster_get_channel(struct Knobster* knobster);

// Set knobster channel (0x00='A' through 0x0F='P')
// 'KNOBSTER_EVENT_CHANNEL' event is fired when set successfully
DLL_EXPORT void libknobster_set_channel(struct Knobster* knobster, int8_t channel);

// Close a Knobster
void libknobster_close(struct Knobster* knobster);
```

Events returned by 'libknobster_poll':
```C
enum KnobsterEvent {
	KNOBSTER_EVENT_NOT_CONNECTED,      // Knobster is still in non connected state. 'libknobster_connect' has not been called.	

	KNOBSTER_EVENT_INITIALIZING,       // Connection with the Knobster is being initialized

	KNOBSTER_EVENT_CHANNEL,            // We got the channel form the knobster
	
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

Example (C)
=======

Simple example trying to connect to the first Knobster it finds and polling for its events (rotary encoder or button changes)

```C
// We will search for a max. of 8 Knobsters
struct Knobster* knobsters[8];
int nr_knobster = libknobster_scan(knobsters, 8);

// Check if we have found at least one
if (nr_knobster >= 1) {
  // We will connect to the first knobster we find
  struct Knobster* knobster = knobsters[0];
  if (libknobster_connect(knobster) == 0) {
	
    // Now that we are connected, we can ask for new events
    while (knobster != NULL) {
      // Ask for a new event
      enum KnobsterEvent event;
      while ( (knobster != NULL) && ( (event = libknobster_poll(knobster)) != KNOBSTER_EVENT_NO_EVENT ) ) {
			
        // See what kind of event we got
        switch (event) {
        case KNOBSTER_EVENT_ERROR_NO_RESPONSE:
        case KNOBSTER_EVENT_ERROR_TRANSFER:
          // Connection problem with the Knobster
          // Clean up knobster object
          libknobster_close(knobster);
          knobster = NULL;
          break;
		  
        case KNOBSTER_EVENT_CHANNEL:
          printf("Channel is %c\n", 'A' + libknobster_get_channel(knobster));
          break;

        case KNOBSTER_EVENT_BUTTON_PRESSED:
        case KNOBSTER_EVENT_BUTTON_RELEASED:
        case KNOBSTER_EVENT_DIAL_MINOR_CW:
        case KNOBSTER_EVENT_DIAL_MINOR_CCW:
        case KNOBSTER_EVENT_DIAL_MAJOR_CW:
        case KNOBSTER_EVENT_DIAL_MAJOR_CCW:
          // Handle rotary encoder and button events here
          break;
        }
      }
	  
      Sleep(1);
    }
  }
}
```

License
=======

There is no license in place. You can use this software as you please.
