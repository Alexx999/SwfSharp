using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.ShapeRecords;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ShapeStruct
    {
        public byte NumFillBits { get; set; }
        public byte NumLineBits { get; set; }
        public IList<ShapeRecord> ShapeRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            NumFillBits = (byte)reader.ReadBits(4);
            NumLineBits = (byte)reader.ReadBits(4);
            ShapeRecords = new List<ShapeRecord>();
            ShapeRecord record;
            do
            {
                record = ShapeRecordFactory.ReadTag(NumFillBits, NumLineBits, type, reader);
                ShapeRecords.Add(record);
            } while (record.RecordType != ShapeRecordType.EndShape);
        }

        internal static ShapeStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new ShapeStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}
