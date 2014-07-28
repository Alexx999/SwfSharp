using System;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [Serializable]
    public class EndShapeRecord : ShapeRecord
    {
        public EndShapeRecord() : base(ShapeRecordType.EndShape)
        {
        }

        internal override void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType)
        {
            writer.WriteBits(6, 0);
        }
    }
}