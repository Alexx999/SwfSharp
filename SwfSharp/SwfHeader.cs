using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp
{
    [Serializable]
    public class SwfHeader
    {
        [XmlAttribute]
        public byte Version { get; set; }
        [XmlAttribute]
        public SwfFileCompression Compression { get; set; }
        [XmlAttribute]
        public uint FileSize { get; set; }
        [XmlElement]
        public RectStruct Rect { get; set; }
        [XmlAttribute]
        public float FrameRate { get; set; }
        [XmlAttribute]
        public ushort FrameCount { get; set; }

        internal void FromCompressedStream(BitReader reader)
        {
            Compression = ReadSignature(reader);
            Version = reader.ReadUI8();
            FileSize = reader.ReadUI32();
        }

        internal void FromStream(BitReader reader)
        {
            Rect = RectStruct.CreateFromStream(reader);
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

        internal void ToUncompressedStream(BitWriter writer)
        {
            writer.WriteUI8((byte)SwfFileCompression.None);
            writer.WriteUI16(0x5357);
            writer.WriteUI8(Version);
            writer.WriteUI32(FileSize);
        }

        internal void ToStream(BitWriter writer)
        {
            Rect.ToStream(writer);
            writer.WriteFixed8(FrameRate);
            writer.WriteUI16(FrameCount);
        }
    }
}
