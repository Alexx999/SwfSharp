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
        public List<GlyphEntryStruct> GlyphEntries { get; set; }

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

        internal void ToStream(BitWriter writer, TagType type, byte glyphBits, byte advanceBits)
        {
            writer.Align();
            writer.WriteBits(1, 1);
            writer.WriteBits(3, 0);
            writer.WriteBoolBit(StyleFlagsHasFont);
            writer.WriteBoolBit(StyleFlagsHasColor);
            writer.WriteBoolBit(StyleFlagsHasYOffset);
            writer.WriteBoolBit(StyleFlagsHasXOffset);

            if (StyleFlagsHasFont)
            {
                writer.WriteUI16(FontID);
            }
            if (StyleFlagsHasColor)
            {
                if (type == TagType.DefineText2)
                {
                    TextColor.ToStream(writer);
                }
                else
                {
                    TextColor.ToRgbStream(writer);
                }
            }
            if (StyleFlagsHasXOffset)
            {
                writer.WriteSI16(XOffset);
            }
            if (StyleFlagsHasYOffset)
            {
                writer.WriteSI16(YOffset);
            }
            if (StyleFlagsHasFont)
            {
                writer.WriteUI16(TextHeight);
            }
            writer.WriteUI8((byte) GlyphEntries.Count);

            foreach (var glyphEntry in GlyphEntries)
            {
                glyphEntry.ToStream(writer, glyphBits, advanceBits);
            }
        }
    }
}
