using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineFont2Tag : SwfTag
    {
        public ushort FontID { get; set; }
        public bool FontFlagsHasLayout { get; set; }
        public bool FontFlagsShiftJIS { get; set; }
        public bool FontFlagsSmallText { get; set; }
        public bool FontFlagsANSI { get; set; }
        public bool FontFlagsWideCodes { get; set; }
        public bool FontFlagsItalic { get; set; }
        public bool FontFlagsBold { get; set; }
        public byte LanguageCode { get; set; }
        public string FontName { get; set; }
        public IList<ShapeStruct> GlyphShapeTable { get; set; }
        public IList<ushort> CodeTable { get; set; }
        public ushort FontAscent { get; set; }
        public ushort FontDescent { get; set; }
        public short FontLeading { get; set; }
        public IList<short> FontAdvanceTable { get; set; }
        public IList<RectStruct> FontBoundsTable { get; set; }
        public ushort KerningCount { get; set; }
        public IList<KerningRecordStruct> FontKerningTable { get; set; }

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
            FontFlagsHasLayout = reader.ReadBoolBit();
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
            //var offsetTableSize = numGlyphs*(fontFlagsWideOffsets ? 4 : 2);
            //reader.ReadBytes(offsetTableSize);
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
            if (!FontFlagsHasLayout) return;

            FontAscent = reader.ReadUI16();
            FontDescent = reader.ReadUI16();
            FontLeading = reader.ReadSI16();
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
            KerningCount = reader.ReadUI16();
            FontKerningTable = new List<KerningRecordStruct>(KerningCount);
            for (int i = 0; i < KerningCount; i++)
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
            var numGlyphs = (ushort)(GlyphShapeTable == null ? 0 : GlyphShapeTable.Count);
            var ms = new MemoryStream();
            uint totalSize;
            uint[] offsets;
            using (var glyphWriter = new BitWriter(ms, true))
            {
                offsets = WriteGlyphData(glyphWriter, numGlyphs, out totalSize);
            }
            bool useWideOffsets = (numGlyphs + 1)*2 + totalSize > ushort.MaxValue;

            writer.WriteUI16(FontID);
            writer.WriteBoolBit(FontFlagsHasLayout);
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
            if (!FontFlagsHasLayout) return;
            writer.WriteUI16(FontAscent);
            writer.WriteUI16(FontDescent);
            writer.WriteSI16(FontLeading);
            for (int i = 0; i < numGlyphs; i++)
            {
                writer.WriteSI16(FontAdvanceTable[i]);
            }
            for (int i = 0; i < numGlyphs; i++)
            {
                FontBoundsTable[i].ToStream(writer);
            }
            writer.WriteUI16(KerningCount);
            for (int i = 0; i < KerningCount; i++)
            {
                FontKerningTable[i].ToStream(writer, FontFlagsWideCodes);
            }
        }
    }
}
