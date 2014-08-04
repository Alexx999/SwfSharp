using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFontNameTag : SwfTag
    {
        [XmlAttribute]
        public ushort FontID { get; set; }
        [XmlAttribute]
        public string FontName { get; set; }
        [XmlAttribute]
        public string FontCopyright { get; set; }

        public DefineFontNameTag() : this(0)
        {
        }

        public DefineFontNameTag(int size)
            : base(TagType.DefineFontName, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            FontName = reader.ReadString();
            FontCopyright = reader.ReadString();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(FontID);
            writer.WriteString(FontName, swfVersion);
            writer.WriteString(FontCopyright, swfVersion);
        }
    }
}
