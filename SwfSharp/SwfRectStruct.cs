using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp
{
    public class SwfRectStruct
    {
        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }

        internal static SwfRectStruct FromStream(BitReader reader)
        {
            var result = new SwfRectStruct();

            reader.Align();

            var bitsPerField = reader.ReadBits(5);

            result.Xmin = reader.ReadBitsSigned(bitsPerField);
            result.Xmax = reader.ReadBitsSigned(bitsPerField);
            result.Ymin = reader.ReadBitsSigned(bitsPerField);
            result.Ymax = reader.ReadBitsSigned(bitsPerField);

            reader.Align();

            return result;
        }
    }
}
