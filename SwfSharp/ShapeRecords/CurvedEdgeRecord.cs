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
            var numBits = reader.ReadBits(4);
            ControlDeltaX = reader.ReadBitsSigned(numBits + 2);
            ControlDeltaY = reader.ReadBitsSigned(numBits + 2);
            AnchorDeltaX = reader.ReadBitsSigned(numBits + 2);
            AnchorDeltaY = reader.ReadBitsSigned(numBits + 2);
        }

        internal static CurvedEdgeRecord CreateFromStream(BitReader reader)
        {
            var result = new CurvedEdgeRecord();

            result.FromStream(reader);

            return result;
        }
    }
}