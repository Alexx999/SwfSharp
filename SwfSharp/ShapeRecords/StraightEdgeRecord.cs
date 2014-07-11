using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    public class StraightEdgeRecord : ShapeRecord
    {
        public bool GeneralLineFlag { get; set; }
        public bool VertLineFlag { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public StraightEdgeRecord() : base(ShapeRecordType.StraightEdge)
        {
        }

        private void FromStream(BitReader reader)
        {
            var numBits = reader.ReadBits(4);
            GeneralLineFlag = reader.ReadBoolBit();
            if (!GeneralLineFlag)
            {
                VertLineFlag = reader.ReadBoolBit();
            }

            if(GeneralLineFlag || !VertLineFlag)
            {
                DeltaX = reader.ReadBitsSigned(numBits + 2);
            }
            if (GeneralLineFlag || VertLineFlag)
            {
                DeltaY = reader.ReadBitsSigned(numBits + 2);
            }
        }

        internal static StraightEdgeRecord CreateFromStream(BitReader reader)
        {
            var result = new StraightEdgeRecord();

            result.FromStream(reader);

            return result;
        }
    }
}