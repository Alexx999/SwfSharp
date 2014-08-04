using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class UnknownTag : SwfTag
    {
        [XmlElement]
        public byte[] Bytes { get; set; }

        //Mostly for XML serialization
        public new int TagType
        {
            get { return (int)base.TagType; }
            set { base.TagType = (TagType)value; }
        }

        public UnknownTag() : base((TagType) 255, 0)
        {
        }

        public UnknownTag(TagType type, int size) : base(type, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Bytes = reader.ReadBytes(Size);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(Bytes);
        }
    }
}
