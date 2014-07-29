using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineBinaryDataTag : SwfTag
    {
        [XmlAttribute]
        public ushort Tag { get; set; }
        [XmlElement]
        public byte[] Data { get; set; }

        public DefineBinaryDataTag() : this(0)
        {
        }

        public DefineBinaryDataTag(int size)
            : base(TagType.DefineBinaryData, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Tag = reader.ReadUI16();
            reader.ReadUI32();
            Data = reader.ReadBytes(Size - 6);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(Tag);
            writer.WriteUI32(0);
            writer.WriteBytes(Data);
        }
    }
}
