using KnobsterWrapper;

Console.WriteLine("Searching for knobsters...");

IReadOnlyList<KnobsterDevice> devices = Array.Empty<KnobsterDevice>();
var scanEndAt = DateTime.UtcNow + TimeSpan.FromSeconds(10);

while (DateTime.UtcNow < scanEndAt)
{
    devices = Knobster.Scan();
    if (devices.Count > 0)
    {
        break;
    }

    Thread.Sleep(100);
}

if (devices.Count == 0)
{
    Console.WriteLine("No knobsters where found to connect to");
    return;
}

Console.WriteLine($"Found {devices.Count} knobsters");

Console.WriteLine("Connecting to first knobster we found");

using var knobster = devices[0];

if (!knobster.Connect())
{
    Console.WriteLine("Failed to connect to Knobster");
    return;
}

Console.WriteLine("Connected to Knobster, waiting for events...");

var endAt = DateTime.UtcNow + TimeSpan.FromSeconds(60);

while (knobster != null)
{
    var ev = knobster.Poll();

    if (ev == KnobsterEvent.NoEvent)
    {
        Thread.Sleep(1);
        continue;
    }

    switch (ev)
    {
        case KnobsterEvent.Channel:
            Console.WriteLine($"Got channel: {(char)('A' + knobster.GetChannel())}");
            break;
        case KnobsterEvent.ButtonPressed:
            Console.WriteLine("Button is pressed");
            break;
        case KnobsterEvent.ButtonReleased:
            Console.WriteLine("Button is released");
            break;
        case KnobsterEvent.DialMinorCw:
            Console.WriteLine("Minor dial is turned clockwise");
            break;
        case KnobsterEvent.DialMinorCcw:
            Console.WriteLine("Minor dial is turned counterclockwise");
            break;
        case KnobsterEvent.DialMajorCw:
            Console.WriteLine("Major dial is turned clockwise");
            break;
        case KnobsterEvent.DialMajorCcw:
            Console.WriteLine("Major dial is turned counterclockwise");
            break;
        case KnobsterEvent.ErrorNoResponse:
        case KnobsterEvent.ErrorTransfer:
            Console.WriteLine("Lost connection with the Knobster");
            return;
    }
}

