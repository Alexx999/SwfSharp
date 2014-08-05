using System;

namespace SwfSharp.ABC
{
    [Flags]
    public enum TraitAttributes
    {
        Final = 0x1,
        Override = 0x2,
        Metadata = 0x4
    }
}