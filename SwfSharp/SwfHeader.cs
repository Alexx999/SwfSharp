using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwfSharp
{
    public class SwfHeader
    {
        public byte Version { get; set; }
        public SwfFileCompression Compression { get; set; }
        public uint FileSize { get; set; }
        public SwfRectStruct Rect { get; set; }

        public void FromCompressedStream(BinaryReader reader)
        {
            Compression = ReadSignature(reader);
            Version = reader.ReadByte();
            FileSize = reader.ReadUInt32();
        }

        public void FromStream(BinaryReader reader)
        {
            Rect = SwfRectStruct.FromStream(reader);
        }

        private static SwfFileCompression ReadSignature(BinaryReader reader)
        {
            var compressionMode = (SwfFileCompression)reader.ReadByte();
            var magic = reader.ReadUInt16();

            if (magic != 0x5357)
            {
                throw new FormatException("Data is not an Adobe SWF file");
            }
            return compressionMode;
        }
    }
}
