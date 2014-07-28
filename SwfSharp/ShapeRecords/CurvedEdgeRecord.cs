using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [Serializable]
    public class CurvedEdgeRecord : ShapeRecord
    {
        [XmlAttribute]
        public int ControlDeltaX { get; set; }
        [XmlAttribute]
        public int ControlDeltaY { get; set; }
        [XmlAttribute]
        public int AnchorDeltaX { get; set; }
        [XmlAttribute]
        public int AnchorDeltaY { get; set; }

        public CurvedEdgeRecord() : base(ShapeRecordType.CurvedEdge)
        {
        }

        private void FromStream(BitReader reader)
        {
            var numBits = reader.ReadBits(4) + 2;
            ControlDeltaX = reader.ReadBitsSigned(numBits);
            ControlDeltaY = reader.ReadBitsSigned(numBits);
            AnchorDeltaX = reader.ReadBitsSigned(numBits);
            AnchorDeltaY = reader.ReadBitsSigned(numBits);
        }

        internal static CurvedEdgeRecord CreateFromStream(BitReader reader)
        {
            var result = new CurvedEdgeRecord();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType)
        {
            //Type flags
            writer.WriteBits(1, 1);
            writer.WriteBits(1, 0);
            writer.WriteBitSizeAndDataWithOffset(4, 2, new[] { ControlDeltaX, ControlDeltaY, AnchorDeltaX, AnchorDeltaY });
        }
    }
}