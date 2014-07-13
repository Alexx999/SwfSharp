namespace SwfSharp.Structs
{
    public enum FillStyle : byte
    {
        Solid = 0x00,
        LinearGradient = 0x10,
        RadialGradient = 0x12,
        FocalRadialGradient = 0x13,
        RepeatingBitmap = 0x40,
        ClippedBitmap = 0x41,
        NonSmoothedRepeatingBitmap = 0x42,
        NonSmoothedClippedBitmap = 0x43,
    }
}