using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp
{
    public class SwfHeader
    {
        public byte Version { get; set; }
        public SwfFileCompression Compression { get; set; }
        public uint FileSize { get; set; }
        public SwfRectStruct Rect { get; set; }
        public float FrameRate { get; set; }
        public ushort FrameCount { get; set; }

        internal void FromCompressedStream(BitReader reader)
        {
            Compression = ReadSignature(reader);
            Version = reader.ReadUI8();
            FileSize = reader.ReadUI32();
        }

        internal void FromStream(BitReader reader)
        {
            Rect = SwfRectStruct.CreateFromStream(reader);
            FrameRate = reader.ReadFixed8();
            FrameCount = reader.ReadUI16();
        }

        private static SwfFileCompression ReadSignature(BitReader reader)
        {
            var compressionMode = (SwfFileCompression)reader.ReadUI8();
            var magic = reader.ReadUI16();

            if (magic != 0x5357)
            {
                throw new FormatException("Data is not an Adobe SWF file");
            }
            return compressionMode;
        }
    }
}
