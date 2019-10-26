/*
	libknobster:

	This library will help connect the USB Knobster to any third party application.
	https://www.siminnovations.com/hardware/product/57-knobster

	Functions are NOT thread safe, make sure to call all functions from the same thread.

	
	Example:

	void main(int argc, const char* argv[]) {
			
	}
*/

#pragma once

#ifdef __cplusplus
#ifdef WIN32
#define DLL_EXPORT extern "C" __declspec(dllexport)
#endif
#ifdef __APPLE__
#define DLL_EXPORT extern "C" __attribute__((visibility("default")))
#endif
#ifdef __linux__
#define DLL_EXPORT extern "C" __attribute__((visibility("default")))
#endif
extern "C" {
#else
#ifdef WIN32
#define DLL_EXPORT __declspec(dllexport)
#endif
#ifdef __APPLE__
#define DLL_EXPORT __attribute__((visibility("default")))
#endif
#ifdef __linux__
#define DLL_EXPORT __attribute__((visibility("default")))
#endif
#endif

struct Knobster;

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

// Scan for Knobsters on USB ports. len is the max number of Knobsters you want to search for.
DLL_EXPORT int libknobster_scan(struct Knobster** knobster_list, int len);

// Connect to the Knobster
// Returns 0 on Success, otherwise failed to connect
DLL_EXPORT int libknobster_connect(struct Knobster* knobster);

// Disconnect the Knobster
DLL_EXPORT void libknobster_disconnect(struct Knobster* knobster);

// Poll Knobster for new events
DLL_EXPORT enum KnobsterEvent libknobster_poll(struct Knobster* knobster);

// Close a Knobster
DLL_EXPORT void libknobster_close(struct Knobster* knobster);

#ifdef __cplusplus
}
#endif
