namespace KnobsterWrapper;

public enum KnobsterEvent
{
    NotConnected = 0,
    Initializing = 1,
    Channel = 2,
    ErrorNoResponse = 3,
    ErrorTransfer = 4,
    NoEvent = 5,
    DialMinorCw = 6,
    DialMinorCcw = 7,
    DialMajorCw = 8,
    DialMajorCcw = 9,
    ButtonPressed = 10,
    ButtonReleased = 11
}
