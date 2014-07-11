using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Annotations;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    class ShapeRecordFactory
    {

        public static ShapeRecord ReadTag(byte numFillBits, byte numLineBits, TagType type, BitReader reader)
        {
            var isEdgeRecord = reader.ReadBoolBit();
            if (!isEdgeRecord)
            {
                var nonEdgeFlags = new NonEdgeFlags();
                nonEdgeFlags.FromStream(reader);

                if (nonEdgeFlags.AllZero())
                {
                    return new EndShapeRecord();
                }
                else
                {
                    return StyleChangeRecord.CreateFromStream(nonEdgeFlags, numFillBits, numLineBits, type, reader);
                }
            }
            else
            {
                var isStraightEdge = reader.ReadBoolBit();
                if (isStraightEdge)
                {
                    return StraightEdgeRecord.CreateFromStream(reader);
                }
                else
                {
                    return CurvedEdgeRecord.CreateFromStream(reader);
                }
            }
        }
    }

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
