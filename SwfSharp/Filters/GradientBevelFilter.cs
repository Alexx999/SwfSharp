using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public class GradientBevelFilter : GradientGlowAndBevelFilter
    {
        internal static GradientBevelFilter CreateFromStream(BitReader reader)
        {
            var result = new GradientBevelFilter();

            result.FromStream(reader);

            return result;
        }
    }
}
