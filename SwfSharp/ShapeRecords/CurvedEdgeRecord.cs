using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    public class CurvedEdgeRecord : ShapeRecord
    {
        public int ControlDeltaX { get; set; }
        public int ControlDeltaY { get; set; }
        public int AnchorDeltaX { get; set; }
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