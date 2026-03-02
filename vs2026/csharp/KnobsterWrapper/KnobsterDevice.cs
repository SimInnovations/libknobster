namespace KnobsterWrapper;

public sealed class KnobsterDevice : IDisposable
{
    private IntPtr _handle;

    internal KnobsterDevice(IntPtr handle)
    {
        _handle = handle;
    }

    public bool Connect()
    {
        EnsureNotClosed();
        return NativeMethods.libknobster_connect(_handle) == 0;
    }

    public void Disconnect()
    {
        EnsureNotClosed();
        NativeMethods.libknobster_disconnect(_handle);
    }

    public KnobsterEvent Poll()
    {
        EnsureNotClosed();
        return NativeMethods.libknobster_poll(_handle);
    }

    public sbyte GetChannel()
    {
        EnsureNotClosed();
        return NativeMethods.libknobster_get_channel(_handle);
    }

    public void SetChannel(sbyte channel)
    {
        EnsureNotClosed();
        NativeMethods.libknobster_set_channel(_handle, channel);
    }

    public void Dispose()
    {
        if (_handle != IntPtr.Zero)
        {
            NativeMethods.libknobster_close(_handle);
            _handle = IntPtr.Zero;
        }

        GC.SuppressFinalize(this);
    }

    private void EnsureNotClosed()
    {
        if (_handle == IntPtr.Zero)
        {
            throw new ObjectDisposedException(nameof(KnobsterDevice));
        }
    }
}
