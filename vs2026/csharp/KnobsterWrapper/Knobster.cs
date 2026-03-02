namespace KnobsterWrapper;

public static class Knobster
{
    public static IReadOnlyList<KnobsterDevice> Scan(int maxDevices = 8)
    {
        if (maxDevices <= 0)
        {
            return Array.Empty<KnobsterDevice>();
        }

        var handles = new IntPtr[maxDevices];
        var found = NativeMethods.libknobster_scan(handles, handles.Length);

        if (found <= 0)
        {
            return Array.Empty<KnobsterDevice>();
        }

        var devices = new List<KnobsterDevice>(found);

        for (var i = 0; i < found && i < handles.Length; i++)
        {
            if (handles[i] != IntPtr.Zero)
            {
                devices.Add(new KnobsterDevice(handles[i]));
            }
        }

        return devices;
    }
}
