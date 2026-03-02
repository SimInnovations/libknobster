using System.Runtime.InteropServices;

namespace KnobsterWrapper;

internal static partial class NativeMethods
{
    private const string NativeLibraryName = "libknobster";

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_scan")]
    internal static partial int libknobster_scan([Out] IntPtr[] knobsterList, int len);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_connect")]
    internal static partial int libknobster_connect(IntPtr knobster);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_disconnect")]
    internal static partial void libknobster_disconnect(IntPtr knobster);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_poll")]
    internal static partial KnobsterEvent libknobster_poll(IntPtr knobster);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_get_channel")]
    internal static partial sbyte libknobster_get_channel(IntPtr knobster);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_set_channel")]
    internal static partial void libknobster_set_channel(IntPtr knobster, sbyte channel);

    [LibraryImport(NativeLibraryName, EntryPoint = "libknobster_close")]
    internal static partial void libknobster_close(IntPtr knobster);
}
