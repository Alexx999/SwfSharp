using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class RemoveObjectTag : RemoveObject2Tag
    {
        [XmlAttribute]
        public ushort CharacterId { get; set; }

        public RemoveObjectTag() : this(0)
        {
        }

        public RemoveObjectTag(int size)
            : base(TagType.RemoveObject, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterId);
            base.ToStream(writer, swfVersion);
        }
    }
}
