using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineFont4Tag : SwfTag
    {
        public ushort FontID { get; set; }
        public bool FontFlagsHasFontData { get; set; }
        public bool FontFlagsItalic { get; set; }
        public bool FontFlagsBold { get; set; }
        public string FontName { get; set; }
        public byte[] FontData { get; set; }

        public DefineFont4Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            reader.ReadBits(5);
            FontFlagsHasFontData = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            FontName = reader.ReadString();
            FontData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }
    }
}
