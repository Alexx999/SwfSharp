using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable, Obsolete]
    public class NameCharacterTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterID { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        public NameCharacterTag() : this(0)
        {
        }

        public NameCharacterTag(int size)
            : base(TagType.NameCharacter, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            Name = reader.ReadString();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteString(Name, swfVersion);
        }
    }
}
