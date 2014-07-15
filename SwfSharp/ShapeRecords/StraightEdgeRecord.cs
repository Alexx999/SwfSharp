using System;
using SwfSharp.Tags;
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
            var numBits = reader.ReadBits(4) + 2;
            GeneralLineFlag = reader.ReadBoolBit();
            if (!GeneralLineFlag)
            {
                VertLineFlag = reader.ReadBoolBit();
            }

            if(GeneralLineFlag || !VertLineFlag)
            {
                DeltaX = reader.ReadBitsSigned(numBits);
            }
            if (GeneralLineFlag || VertLineFlag)
            {
                DeltaY = reader.ReadBitsSigned(numBits);
            }
        }

        internal static StraightEdgeRecord CreateFromStream(BitReader reader)
        {
            var result = new StraightEdgeRecord();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType)
        {
            //Type flags
            writer.WriteBits(1, 1);
            writer.WriteBits(1, 1);

            int[] data;
            if (GeneralLineFlag)
            {
                data = new[] {DeltaX, DeltaY};
            }
            else if (VertLineFlag)
            {
                data = new[] { DeltaY };
            }
            else
            {
                data = new[] { DeltaX };
            }
            var numBits = (uint)Math.Max((int)BitWriter.MinBitsPerField(data) - 2, 0);
            writer.WriteBits(4, numBits);
            writer.WriteBoolBit(GeneralLineFlag);
            if (!GeneralLineFlag)
            {
                writer.WriteBoolBit(VertLineFlag);
            }

            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteBitsSigned(numBits + 2, data[i]);
            }
        }
    }
}