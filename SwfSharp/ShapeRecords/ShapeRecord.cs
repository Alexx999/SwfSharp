using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    public abstract class ShapeRecord
    {
        public ShapeRecordType RecordType { get; set; }

        public ShapeRecord(ShapeRecordType recordType)
        {
            RecordType = recordType;
        }

        internal abstract void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType);
    }
}
