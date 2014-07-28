using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [Serializable]
    public class StraightEdgeRecord : ShapeRecord
    {
        [XmlAttribute]
        public int DeltaX { get; set; }
        [XmlAttribute]
        public int DeltaY { get; set; }

        public StraightEdgeRecord() : base(ShapeRecordType.StraightEdge)
        {
        }

        private void FromStream(BitReader reader)
        {
            var numBits = reader.ReadBits(4) + 2;
            var generalLineFlag = reader.ReadBoolBit();
            bool vertLineFlag = false;
            if (!generalLineFlag)
            {
                vertLineFlag = reader.ReadBoolBit();
            }

            if (generalLineFlag || !vertLineFlag)
            {
                DeltaX = reader.ReadBitsSigned(numBits);
            }
            if (generalLineFlag || vertLineFlag)
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

            var generalLineFlag = (DeltaX != 0) && (DeltaY != 0);
            var vertLineFlag = (DeltaX == 0) && (DeltaY != 0);

            int[] data;
            if (generalLineFlag)
            {
                data = new[] {DeltaX, DeltaY};
            }
            else if (vertLineFlag)
            {
                data = new[] { DeltaY };
            }
            else
            {
                data = new[] { DeltaX };
            }
            var numBits = (uint)Math.Max((int)BitWriter.MinBitsPerField(data) - 2, 0);
            writer.WriteBits(4, numBits);
            writer.WriteBoolBit(generalLineFlag);
            if (!generalLineFlag)
            {
                writer.WriteBoolBit(vertLineFlag);
            }

            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteBitsSigned(numBits + 2, data[i]);
            }
        }
    }
}