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
            var charSize = FontFlagsWideCodes ? 2 : 1;
            ReadCodeTable(reader, charSize);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }

        internal virtual void ReadCodeTable(BitReader reader, int charSize)
        {
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
