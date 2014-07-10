using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Utils
{
    internal static class BitUtil
    {
        public static int ByteCount(int bits)
        {
            var fullBytes = bits/8;
            var remainder = bits%8;
            return fullBytes + (remainder == 0 ? 0 : 1);
        }
    }
}
