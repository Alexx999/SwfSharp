using System;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
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
