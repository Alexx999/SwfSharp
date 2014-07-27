using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineTextTag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public RectStruct TextBounds { get; set; }
        public MatrixStruct TextMatrix { get; set; }
        public IList<TextRecordStruct> TextRecords { get; set; }

        public DefineTextTag() : this(0)
        {
        }

        public DefineTextTag(int size)
            : base(TagType.DefineText, size)
        {
        }

        protected DefineTextTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            TextBounds = RectStruct.CreateFromStream(reader);
            TextMatrix = MatrixStruct.CreateFromStream(reader);
            var glyphBits = reader.ReadUI8();
            var advanceBits = reader.ReadUI8();
            TextRecords = new List<TextRecordStruct>();
            TextRecordStruct nextTextRecord = TextRecordStruct.CreateFromStream(reader, TagType, glyphBits, advanceBits);
            while (nextTextRecord.TextRecordType == 1)
            {
                TextRecords.Add(nextTextRecord);
                nextTextRecord = TextRecordStruct.CreateFromStream(reader, TagType, glyphBits, advanceBits);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            TextBounds.ToStream(writer);
            TextMatrix.ToStream(writer);
            var glyphs = TextRecords.SelectMany(t => t.GlyphEntries).ToList();
            var glyphBits = (byte)(glyphs.Count == 0 ? 0 : glyphs.Max(g => BitWriter.GetBitsForValue(g.GlyphIndex)));
            var advanceBits = (byte)(glyphs.Count == 0 ? 0 : glyphs.Max(g => BitWriter.GetBitsForValue(g.GlyphAdvance)));
            writer.WriteUI8(glyphBits);
            writer.WriteUI8(advanceBits);
            foreach (var textRecord in TextRecords)
            {
                textRecord.ToStream(writer, TagType, glyphBits, advanceBits);
            }
            writer.WriteUI8(0);
        }
    }
}
