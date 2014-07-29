using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFont2Tag : SwfTag
    {
        private ushort? _fontAscent;
        private ushort? _fontDescent;
        private short? _fontLeading;

        [XmlAttribute]
        public ushort FontID { get; set; }
        [XmlAttribute]
        public bool FontFlagsShiftJIS { get; set; }
        [XmlAttribute]
        public bool FontFlagsSmallText { get; set; }
        [XmlAttribute]
        public bool FontFlagsANSI { get; set; }
        [XmlAttribute]
        public bool FontFlagsWideCodes { get; set; }
        [XmlAttribute]
        public bool FontFlagsItalic { get; set; }
        [XmlAttribute]
        public bool FontFlagsBold { get; set; }
        [XmlAttribute]
        public byte LanguageCode { get; set; }
        [XmlAttribute]
        public string FontName { get; set; }
        [XmlArrayItem("Shape")]
        public List<ShapeStruct> GlyphShapeTable { get; set; }
        [XmlArrayItem("Code")]
        public List<ushort> CodeTable { get; set; }

        [XmlAttribute]
        public ushort FontAscent
        {
            get { return _fontAscent.GetValueOrDefault(); }
            set { _fontAscent = value; }
        }

        [XmlIgnore]
        public bool FontAscentSpecified
        {
            get { return _fontAscent.HasValue; }
        }

        [XmlAttribute]
        public ushort FontDescent
        {
            get { return _fontDescent.GetValueOrDefault(); }
            set { _fontDescent = value; }
        }

        [XmlIgnore]
        public bool FontDescentSpecified
        {
            get { return _fontDescent.HasValue; }
        }

        [XmlAttribute]
        public short FontLeading
        {
            get { return _fontLeading.GetValueOrDefault(); }
            set { _fontLeading = value; }
        }

        [XmlIgnore]
        public bool FontLeadingSpecified
        {
            get { return _fontLeading.HasValue; }
        }

        [XmlArrayItem("Advance")]
        public List<short> FontAdvanceTable { get; set; }
        [XmlArrayItem("Bounds")]
        public List<RectStruct> FontBoundsTable { get; set; }
        [XmlArrayItem("KerningRecord")]
        public List<KerningRecordStruct> FontKerningTable { get; set; }

        public DefineFont2Tag() : this(0)
        {
        }

        public DefineFont2Tag(int size)
            : base(TagType.DefineFont2, size)
        {
        }

        protected DefineFont2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            var fontFlagsHasLayout = reader.ReadBoolBit();
            FontFlagsShiftJIS = reader.ReadBoolBit();
            FontFlagsSmallText = reader.ReadBoolBit();
            FontFlagsANSI = reader.ReadBoolBit();
            var fontFlagsWideOffsets = reader.ReadBoolBit();
            FontFlagsWideCodes = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            LanguageCode = reader.ReadUI8();
            FontName = reader.ReadSizeString();
            var numGlyphs = reader.ReadUI16();
            if (numGlyphs == 0 && reader.AtTagEnd()) return;
            var offsets = new uint[numGlyphs];
            for (int i = 0; i < numGlyphs; i++)
            {
                offsets[i] = (fontFlagsWideOffsets ? reader.ReadUI32() : reader.ReadUI16()) - 232;
            }
            // ReSharper disable once UnusedVariable
            uint codeTableOffset = fontFlagsWideOffsets ? reader.ReadUI32() : reader.ReadUI16();
            GlyphShapeTable = new List<ShapeStruct>(numGlyphs);
            for (int i = 0; i < numGlyphs; i++)
            {
                GlyphShapeTable.Add(ShapeStruct.CreateFromStream(reader, TagType));
            }
            CodeTable = new List<ushort>(numGlyphs);
            for (int i = 0; i < numGlyphs; i++)
            {
                CodeTable.Add(FontFlagsWideCodes ? reader.ReadUI16() : reader.ReadUI8());
            }
            if (!fontFlagsHasLayout) return;

            _fontAscent = reader.ReadUI16();
            _fontDescent = reader.ReadUI16();
            _fontLeading = reader.ReadSI16();
            FontAdvanceTable = new List<short>(numGlyphs);
            for (int i = 0; i < numGlyphs; i++)
            {
                FontAdvanceTable.Add(reader.ReadSI16());
            }
            FontBoundsTable = new List<RectStruct>(numGlyphs);
            for (int i = 0; i < numGlyphs; i++)
            {
                FontBoundsTable.Add(RectStruct.CreateFromStream(reader));
            }
            var kerningCount = reader.ReadUI16();
            FontKerningTable = new List<KerningRecordStruct>(kerningCount);
            for (int i = 0; i < kerningCount; i++)
            {
                FontKerningTable.Add(KerningRecordStruct.CreateFromStream(reader, FontFlagsWideCodes));
            }
        }

        private uint[] WriteGlyphData(BitWriter writer, int numGlyphs, out uint totalSize)
        {
            var result = new uint[numGlyphs];
            var startPos = writer.Position;
            for (int i = 0; i < numGlyphs; i++)
            {
                result[i] = (uint) (writer.Position - startPos);
                GlyphShapeTable[i].ToStream(writer, TagType);
                writer.Align();
            }
            totalSize = (uint)(writer.Position - startPos);
            return result;
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var fontFlagsHasLayout = _fontAscent.HasValue && _fontDescent.HasValue && _fontLeading.HasValue &&
                                     FontAdvanceTable != null && FontBoundsTable != null && FontKerningTable != null;

            var numGlyphs = (ushort)(GlyphShapeTable == null ? 0 : GlyphShapeTable.Count);
            uint totalSize;
            uint[] offsets;
            var ms = new MemoryStream();
            using (var glyphWriter = new BitWriter(ms, true))
            {
                offsets = WriteGlyphData(glyphWriter, numGlyphs, out totalSize);
            }
            bool useWideOffsets = (numGlyphs + 1)*2 + totalSize > ushort.MaxValue;

            writer.WriteUI16(FontID);
            writer.WriteBoolBit(fontFlagsHasLayout);
            writer.WriteBoolBit(FontFlagsShiftJIS);
            writer.WriteBoolBit(FontFlagsSmallText);
            writer.WriteBoolBit(FontFlagsANSI);
            writer.WriteBoolBit(useWideOffsets);
            writer.WriteBoolBit(FontFlagsWideCodes);
            writer.WriteBoolBit(FontFlagsItalic);
            writer.WriteBoolBit(FontFlagsBold);
            writer.WriteUI8(LanguageCode);
            writer.WriteSizeString(FontName, swfVersion);
            writer.WriteUI16(numGlyphs);
#if !MAKE_SWFINVESTIGATOR_HAPPY
            if (numGlyphs == 0) return;
#endif
            var offsetSize = (numGlyphs + 1)*(useWideOffsets ? 4 : 2);
            for (int i = 0; i < numGlyphs; i++)
            {
                if (useWideOffsets)
                {
                    writer.WriteUI32((uint) (offsets[i] + offsetSize));
                }
                else
                {
                    writer.WriteUI16((ushort)(offsets[i] + offsetSize));
                }
            }
            var totalOffset = (uint) (totalSize + offsetSize);
            if (useWideOffsets)
            {
                writer.WriteUI32(totalOffset);
            }
            else
            {
                writer.WriteUI16((ushort)totalOffset);
            }
            writer.WriteBytes(ms.GetBuffer(), 0, (int) totalSize);

            for (int i = 0; i < numGlyphs; i++)
            {
                if (FontFlagsWideCodes)
                {
                    writer.WriteUI16(CodeTable[i]);
                }
                else
                {
                    writer.WriteUI8((byte) CodeTable[i]);
                }
            }
            if (!fontFlagsHasLayout) return;
            writer.WriteUI16(_fontAscent.Value);
            writer.WriteUI16(_fontDescent.Value);
            writer.WriteSI16(_fontLeading.Value);
            for (int i = 0; i < numGlyphs; i++)
            {
                writer.WriteSI16(FontAdvanceTable[i]);
            }
            for (int i = 0; i < numGlyphs; i++)
            {
                FontBoundsTable[i].ToStream(writer);
            }
            writer.WriteUI16((ushort) FontKerningTable.Count);
            for (int i = 0; i < FontKerningTable.Count; i++)
            {
                FontKerningTable[i].ToStream(writer, FontFlagsWideCodes);
            }
        }
    }
}
