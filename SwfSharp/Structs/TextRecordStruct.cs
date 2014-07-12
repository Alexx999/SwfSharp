using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class TextRecordStruct
    {
        public byte TextRecordType { get; set; }
        public bool StyleFlagsHasFont { get; set; }
        public bool StyleFlagsHasColor { get; set; }
        public bool StyleFlagsHasYOffset { get; set; }
        public bool StyleFlagsHasXOffset { get; set; }
        public ushort FontID { get; set; }
        public RgbaStruct TextColor { get; set; }
        public short XOffset { get; set; }
        public short YOffset { get; set; }
        public ushort TextHeight { get; set; }
        public byte GlyphCount { get; set; }
        public IList<GlyphEntryStruct> GlyphEntries { get; set; }

        private void FromStream(BitReader reader, TagType type, byte glyphBits, byte advanceBits)
        {
            reader.Align();
            TextRecordType = (byte) reader.ReadBits(1);
            reader.ReadBits(3);
            StyleFlagsHasFont = reader.ReadBoolBit();
            StyleFlagsHasColor = reader.ReadBoolBit();
            StyleFlagsHasYOffset = reader.ReadBoolBit();
            StyleFlagsHasXOffset = reader.ReadBoolBit();
            if (TextRecordType == 0)
            {
                return;
            }

            if (StyleFlagsHasFont)
            {
                FontID = reader.ReadUI16();
            }
            if (StyleFlagsHasColor)
            {
                TextColor = type == TagType.DefineText2 
                    ? RgbaStruct.CreateFromStream(reader) 
                    : RgbaStruct.CreateFromRgbStream(reader);
            }
            if (StyleFlagsHasXOffset)
            {
                XOffset = reader.ReadSI16();
            }
            if (StyleFlagsHasYOffset)
            {
                YOffset = reader.ReadSI16();
            }
            if (StyleFlagsHasFont)
            {
                TextHeight = reader.ReadUI16();
            }
            var glyphCount = reader.ReadUI8();
            GlyphEntries = new List<GlyphEntryStruct>(glyphCount);
            for (int i = 0; i < glyphCount; i++)
            {
                GlyphEntries.Add(GlyphEntryStruct.CreateFromStream(reader, glyphBits, advanceBits));
            }
        }

        internal static TextRecordStruct CreateFromStream(BitReader reader, TagType type, byte glyphBits, byte advanceBits)
        {
            var result = new TextRecordStruct();

            result.FromStream(reader, type, glyphBits, advanceBits);

            return result;
        }
    }
}
