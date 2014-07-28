using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public abstract class SwfTag
    {
        protected SwfTag(TagType tagType, int size)
        {
            TagType = tagType;
            Size = size;
        }

        [XmlIgnore]
        public TagType TagType { get; set; }

        protected int Size { get; set; }
        internal abstract void FromStream(BitReader reader, byte swfVersion);
        internal abstract void ToStream(BitWriter writer, byte swfVersion);
    }
}
