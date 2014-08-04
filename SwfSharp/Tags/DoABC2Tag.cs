using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DoABC2Tag : DoABCTag
    {
        [XmlAttribute]
        public uint Flags { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        public DoABC2Tag() : this(0)
        {
        }

        public DoABC2Tag(int size)
            : base(TagType.DoABC2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Flags = reader.ReadUI32();
            Name = reader.ReadString();
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI32(Flags);
            writer.WriteString(Name, swfVersion);
            base.ToStream(writer, swfVersion);
        }
    }
}
