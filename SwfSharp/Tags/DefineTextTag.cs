using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineTextTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterID { get; set; }
        [XmlElement]
        public RectStruct TextBounds { get; set; }
        [XmlElement]
        public MatrixStruct TextMatrix { get; set; }
        [XmlArrayItem("TextRecord")]
        public List<TextRecordStruct> TextRecords { get; set; }

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
            var glyphs = GetAllGlyphs();
            var glyphBits = GetMaxGlyphIndexBits(glyphs);
            var advanceBits = GetMaxAdvanceBits(glyphs);
            writer.WriteUI8(glyphBits);
            writer.WriteUI8(advanceBits);
            foreach (var textRecord in TextRecords)
            {
                textRecord.ToStream(writer, TagType, glyphBits, advanceBits);
            }
            writer.WriteUI8(0);
        }

        private byte GetMaxGlyphIndexBits(List<GlyphEntryStruct> glyphs)
        {
            if (glyphs == null || glyphs.Count == 0) return 0;
            byte currMaxBits = 0;
            foreach (var glyph in glyphs)
            {
                var bitsRequired = BitWriter.GetBitsForValue(glyph.GlyphIndex);
                if (bitsRequired > currMaxBits)
                {
                    currMaxBits = bitsRequired;
                }
            }
            return currMaxBits;
        }

        private byte GetMaxAdvanceBits(List<GlyphEntryStruct> glyphs)
        {
            if (glyphs == null || glyphs.Count == 0) return 0;
            byte currMaxBits = 0;
            foreach (var glyph in glyphs)
            {
                var bitsRequired = BitWriter.GetBitsForValue(glyph.GlyphAdvance);
                if (bitsRequired > currMaxBits)
                {
                    currMaxBits = bitsRequired;
                }
            }
            return currMaxBits;
        }

        private List<GlyphEntryStruct> GetAllGlyphs()
        {
            var result = new List<GlyphEntryStruct>();
            foreach (var textRecord in TextRecords)
            {
                result.AddRange(textRecord.GlyphEntries);
            }
            return result;
        }
    }
}
