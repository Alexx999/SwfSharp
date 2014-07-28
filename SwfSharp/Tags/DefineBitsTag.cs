using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineBitsTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterID { get; set; }
        public byte[] JPEGData { get; set; }

        public DefineBitsTag() : this(0)
        {
        }

        public DefineBitsTag(int size)
            : base(TagType.DefineBits, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            JPEGData = reader.ReadBytes(Size - 2);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteBytes(JPEGData);
        }
    }
}
