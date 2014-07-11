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
        private byte _numFillBits;
        private byte _numLineBits;

        public byte NumFillBits
        {
            get { return _numFillBits; }
            set { _numFillBits = value; }
        }

        public byte NumLineBits
        {
            get { return _numLineBits; }
            set { _numLineBits = value; }
        }

        public IList<ShapeRecord> ShapeRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            NumFillBits = (byte)reader.ReadBits(4);
            NumLineBits = (byte)reader.ReadBits(4);
            ShapeRecords = new List<ShapeRecord>();
            ShapeRecord record;
            do
            {
                record = ShapeRecordFactory.ReadTag(ref _numFillBits, ref _numLineBits, type, reader);
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
