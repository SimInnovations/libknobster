#include <stdio.h> // For printf
#include <Windows.h> // For sleep

#include "libknobster.h"

int main(int argc, const char* argv[]) {

	printf("Searching for knobsters...\n");

	// We will search for a max. of 8 Knobsters
	struct Knobster* knobsters[8];
	int nr_knobster = libknobster_scan(knobsters, 8);

	printf("Found %i knobsters\n", nr_knobster);

	// Check if we have found at least one
	if (nr_knobster >= 1) {
		// We will connect to the first knobster we find
		struct Knobster* knobster = knobsters[0];

		printf("Connecting to first knobster we found\n");

		if (libknobster_connect(knobster) == 0) {
			printf("Connected to Knobster, waiting for events...\n");

			// Now that we are connected, we can ask for new events
			while (knobster != NULL) {

				// Ask for a new event
				enum KnobsterEvent event;
				while ((knobster != NULL) && ((event = libknobster_poll(knobster)) != KNOBSTER_EVENT_NO_EVENT)) {
					switch (event) {

					case KNOBSTER_EVENT_ERROR_NO_RESPONSE:
					case KNOBSTER_EVENT_ERROR_TRANSFER:
						// Connection problem with the Knobster
						printf("Lost connection with the Knobster\n");

						// Clean up knobster object
						libknobster_close(knobster);
						knobster = NULL;
						break;

					case KNOBSTER_EVENT_CHANNEL:
						printf("Got channel: %c\n", 'A' + libknobster_get_channel(knobster));
						break;

					case KNOBSTER_EVENT_BUTTON_PRESSED:
						printf("Button is pressed\n");
						break;

					case KNOBSTER_EVENT_BUTTON_RELEASED:
						printf("Button is released\n");
						break;

					case KNOBSTER_EVENT_DIAL_MINOR_CW:
						printf("Minor dial is turned clockwise\n");
						break;

					case KNOBSTER_EVENT_DIAL_MINOR_CCW:
						printf("Minor dial is turned counterclockwise\n");
						break;

					case KNOBSTER_EVENT_DIAL_MAJOR_CW:
						printf("Major dial is turned clockwise\n");
						break;

					case KNOBSTER_EVENT_DIAL_MAJOR_CCW:
						printf("Major dial is turned counterclockwise\n");
						break;
					}
				}

				Sleep(1);
			}
		}
		else {
			printf("Failed to connect to Knobster\n");
		}
	}
	else {
		printf("No knobsters where found to connect to\n");
	}
}
