using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [Serializable]
    public class StraightEdgeRecord : ShapeRecord
    {
        private int? _deltaX;
        private int? _deltaY;

        [XmlAttribute]
        public int DeltaX
        {
            get { return _deltaX.GetValueOrDefault(); }
            set { _deltaX = value; }
        }

        [XmlIgnore]
        public bool DeltaXSpecified
        {
            get { return _deltaX.HasValue; }
        }

        [XmlAttribute]
        public int DeltaY
        {
            get { return _deltaY.GetValueOrDefault(); }
            set { _deltaY = value; }
        }

        [XmlIgnore]
        public bool DeltaYSpecified
        {
            get { return _deltaY.HasValue; }
        }

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
                _deltaX = reader.ReadBitsSigned(numBits);
            }
            if (generalLineFlag || vertLineFlag)
            {
                _deltaY = reader.ReadBitsSigned(numBits);
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

            var generalLineFlag = _deltaX.HasValue && _deltaY.HasValue;
            var vertLineFlag = !_deltaX.HasValue && _deltaY.HasValue;

            int[] data;
            if (generalLineFlag)
            {
                data = new[] { _deltaX.Value, _deltaY.Value };
            }
            else if (vertLineFlag)
            {
                data = new[] { _deltaY.Value };
            }
            else
            {
                data = new[] { _deltaX.Value };
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