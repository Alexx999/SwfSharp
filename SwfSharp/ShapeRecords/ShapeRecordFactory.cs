﻿using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    internal static class ShapeRecordFactory
    {
        public static ShapeRecord ReadTag(ref byte numFillBits, ref byte numLineBits, TagType type, BitReader reader)
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
                    return StyleChangeRecord.CreateFromStream(nonEdgeFlags, ref numFillBits, ref numLineBits, type, reader);
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
