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
        private byte[] _origBytes;

        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }

        public static SwfRectStruct FromStream(BinaryReader reader)
        {
            var result = new SwfRectStruct();

            var firstByte = reader.PeekChar()|0xFF;

            var bitsPerField = firstByte >> 3;

            int bitLen = bitsPerField * 4 + 5;

            var byteCount = BitUtil.ByteCount(bitLen);

            result._origBytes = reader.ReadBytes(byteCount);

            var pos = 5;

            return result;
        }
    }
}
