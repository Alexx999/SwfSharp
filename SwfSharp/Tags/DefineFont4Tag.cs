using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineFont4Tag : SwfTag
    {
        public ushort FontID { get; set; }
        public bool FontFlagsHasFontData { get; set; }
        public bool FontFlagsItalic { get; set; }
        public bool FontFlagsBold { get; set; }
        public string FontName { get; set; }
        public byte[] FontData { get; set; }

        public DefineFont4Tag() : this(0)
        {
        }

        public DefineFont4Tag(int size)
            : base(TagType.DefineFont4, size)
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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(FontID);
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(FontFlagsHasFontData);
            writer.WriteBoolBit(FontFlagsItalic);
            writer.WriteBoolBit(FontFlagsBold);
            writer.WriteString(FontName, swfVersion);
            writer.WriteBytes(FontData);
        }
    }
}
