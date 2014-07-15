using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SwfSharp.ShapeRecords;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ShapeStruct
    {
        public IList<ShapeRecord> ShapeRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            reader.Align();
            var numFillBits = (byte)reader.ReadBits(4);
            var numLineBits = (byte)reader.ReadBits(4);
            ShapeRecords = new List<ShapeRecord>();
            ShapeRecord record;
            do
            {
                record = ShapeRecordFactory.ReadTag(ref numFillBits, ref numLineBits, type, reader);
                ShapeRecords.Add(record);
            } while (record.RecordType != ShapeRecordType.EndShape);
        }

        internal static ShapeStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new ShapeStruct();

            result.FromStream(reader, type);

            return result;
        }
        internal virtual void ToStream(BitWriter writer, TagType tagType)
        {
            Debug.Assert(tagType == TagType.DefineFont || tagType == TagType.DefineFont2 ||
                         tagType == TagType.DefineFont3, "Shape struct must be used only in fonts and morph shapes");

            writer.Align();
            byte numLineBits = 0;
            byte numFillBits = 1;
            ToStream(writer, ref numFillBits, ref numLineBits, tagType);
        }

        internal void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType)
        {
            writer.Align();

            writer.WriteBits(4, numFillBits);
            writer.WriteBits(4, numLineBits);

            foreach (var record in ShapeRecords)
            {
                record.ToStream(writer, ref numFillBits, ref numLineBits, tagType);
            }
        }
    }
}
