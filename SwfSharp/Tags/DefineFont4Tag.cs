using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFont4Tag : SwfTag
    {
        [XmlAttribute]
        public ushort FontID { get; set; }
        [XmlAttribute]
        public bool FontFlagsItalic { get; set; }
        [XmlAttribute]
        public bool FontFlagsBold { get; set; }
        [XmlAttribute]
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
            var fontFlagsHasFontData = reader.ReadBoolBit();
            FontFlagsItalic = reader.ReadBoolBit();
            FontFlagsBold = reader.ReadBoolBit();
            FontName = reader.ReadString();
            if (fontFlagsHasFontData)
            {
                FontData = reader.ReadBytes((int) reader.TagBytesRemaining);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var fontFlagsHasFontData = FontData != null && FontData.Length > 0;
            writer.WriteUI16(FontID);
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(fontFlagsHasFontData);
            writer.WriteBoolBit(FontFlagsItalic);
            writer.WriteBoolBit(FontFlagsBold);
            writer.WriteString(FontName, swfVersion);
            if (fontFlagsHasFontData)
            {
                writer.WriteBytes(FontData);
            }
        }
    }
}
