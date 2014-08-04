using System;
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
