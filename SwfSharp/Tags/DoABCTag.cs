using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DoABCTag : SwfTag
    {
        [XmlAttribute]
        public uint Flags { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public byte[] ABCData { get; set; }

        public DoABCTag() : this(0)
        {
        }

        public DoABCTag(int size)
            : base(TagType.DoABC, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Flags = reader.ReadUI32();
            Name = reader.ReadString();
            ABCData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI32(Flags);
            writer.WriteString(Name, swfVersion);
            writer.WriteBytes(ABCData);
        }
    }
}
