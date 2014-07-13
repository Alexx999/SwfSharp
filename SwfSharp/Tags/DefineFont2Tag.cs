using System;
using System.Collections.Generic;
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
        public bool FontFlagsWideOffsets { get; set; }
        public bool FontFlagsWideCodes { get; set; }
        public bool FontFlagsItalic { get; set; }
        public bool FontFlagsBold { get; set; }
        public byte LanguageCode { get; set; }
        public string FontName { get; set; }
        public IList<uint> OffsetTable { get; set; }
        public IList<ShapeStruct> GlyphShapeTable { get; set; }
        public IList<ushort> CodeTable { get; set; }
        public ushort FontAscent { get; set; }
        public ushort FontDescent { get; set; }
        public short FontLeading { get; set; }
        public IList<short> FontAdvanceTable { get; set; }
        public IList<RectStruct> FontBoundsTable { get; set; }
        public ushort KerningCount { get; set; }
        public IList<KerningRecordStruct> FontKerningTable { get; set; } 

        public DefineFont2Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            FontFlagsHasLayout = reader.ReadBoolBit();
            FontFlagsShiftJIS = reader.ReadBoolBit();
            FontFlagsSmallText = reader.ReadBoolBit();
            FontFlagsANSI = reader.ReadBoolBit();
            FontFlagsWideOffsets = reader.ReadBoolBit();
            FontFlagsWideCodes = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            LanguageCode = reader.ReadUI8();
            FontName = reader.ReadSizeString();
            var numGlyphs = reader.ReadUI16();
            OffsetTable = new List<uint>(numGlyphs);
            for (int i = 0; i < numGlyphs; i++)
            {
                OffsetTable.Add(FontFlagsWideOffsets ? reader.ReadUI32() : reader.ReadUI16());
            }
            if (numGlyphs == 0 && TagType == TagType.DefineFont3) return;
            // ReSharper disable once UnusedVariable
            uint codeTableOffset = FontFlagsWideOffsets ? reader.ReadUI32() : reader.ReadUI16();
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
    }
}
