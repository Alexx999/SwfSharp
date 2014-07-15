using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    public class GradientGlowFilter : GradientGlowAndBevelFilter
    {
        internal static GradientGlowFilter CreateFromStream(BitReader reader)
        {
            var result = new GradientGlowFilter();

            result.FromStream(reader);

            return result;
        }
    }
}
