using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Structs
{
    public enum BlendMode
    {
        Normal = 1,
        Layer = 2,
        Multiply = 3,
        Screen = 4,
        Lighten = 5,
        Darken = 6,
        Difference = 7,
        Add = 8,
        Subtract = 9,
        Invert = 10,
        Alpha = 11,
        Erase = 12,
        Overlay = 13,
        Hardlight = 14
    }

    public static class BlendModeHelper
    {
        public static BlendMode GetBlendMode(byte value)
        {
            if (value == 0) return BlendMode.Normal;

            return (BlendMode) value;
        }
    }
}
