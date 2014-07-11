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
}
