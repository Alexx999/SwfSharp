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


        public DefineFontInfoTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            var fontNameLen = reader.ReadUI8();
            FontName = reader.ReadString(fontNameLen);
            reader.ReadBits(2);
            FontFlagsSmallText = reader.ReadBoolBit();
            FontFlagsShiftJIS = reader.ReadBoolBit();
            FontFlagsANSI = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            FontFlagsWideCodes = reader.ReadBoolBit();
            var charSize = FontFlagsWideCodes ? 2 : 1;
            ReadCodeTable(reader, charSize);
        }

        internal virtual void ReadCodeTable(BitReader reader, int charSize)
        {
            var nGlyphs = (int)reader.GetTagRemaining() / charSize;
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
