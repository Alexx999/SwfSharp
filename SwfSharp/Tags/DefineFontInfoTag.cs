using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineFontInfoTag : SwfTag
    {
        public ushort FontID { get; set; }
        public string FontName { get; set; }
        public bool FontFlagsSmallText { get; set; }
        public bool FontFlagsShiftJIS { get; set; }
        public bool FontFlagsANSI { get; set; }
        public bool FontFlagsItalic { get; set; }
        public bool FontFlagsBold { get; set; }
        public bool FontFlagsWideCodes { get; set; }
        public IList<ushort> CodeTable { get; set; }


        public DefineFontInfoTag(int size)
            : base(TagType.DefineFontInfo, size)
        {
        }

        protected DefineFontInfoTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            FontName = reader.ReadSizeString();
            reader.ReadBits(2);
            FontFlagsSmallText = reader.ReadBoolBit();
            FontFlagsShiftJIS = reader.ReadBoolBit();
            FontFlagsANSI = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            FontFlagsWideCodes = reader.ReadBoolBit();
            ReadCodeTable(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(FontID);
            writer.WriteSizeString(FontName, swfVersion);
            writer.WriteBits(2, 0);
            writer.WriteBoolBit(FontFlagsSmallText);
            writer.WriteBoolBit(FontFlagsShiftJIS);
            writer.WriteBoolBit(FontFlagsANSI);
            writer.WriteBoolBit(FontFlagsItalic);
            writer.WriteBoolBit(FontFlagsBold);
            writer.WriteBoolBit(FontFlagsWideCodes);
            WriteCodeTable(writer);
        }

        internal virtual void WriteCodeTable(BitWriter writer)
        {
            foreach (var code in CodeTable)
            {
                if (FontFlagsWideCodes)
                {
                    writer.WriteUI16(code);
                }
                else
                {
                    writer.WriteUI8((byte) code);
                }
            }
        }

        internal virtual void ReadCodeTable(BitReader reader)
        {
            var charSize = FontFlagsWideCodes ? 2 : 1;
            var nGlyphs = (int)reader.TagBytesRemaining / charSize;
            CodeTable = new List<ushort>(nGlyphs);
            if (FontFlagsWideCodes)
            {
                for (int i = 0; i < nGlyphs; i++)
                {
                    CodeTable.Add(reader.ReadUI16());
                }
            }
            else
            {
                for (int i = 0; i < nGlyphs; i++)
                {
                    CodeTable.Add(reader.ReadUI8());
                }
            }
        }
    }
}
