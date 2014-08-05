using System;

namespace SwfSharp.ABC
{
    [Flags]
    public enum InstanceFlags : byte
    {
        Sealed = 0x01,
        Final = 0x02,
        Interface = 0x04,
        ProtectedNs = 0x08
    }
}