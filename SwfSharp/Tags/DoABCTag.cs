using System;
using System.Xml.Serialization;
using SwfSharp.ABC;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable, Obsolete("Use DoABC2 instead")]
    public class DoABCTag : SwfTag
    {
        [XmlElement]
        public ABCFile ABCData { get; set; }

        public DoABCTag() : this(0)
        {
        }

        public DoABCTag(int size)
            : base(TagType.DoABC, size)
        {
        }
        protected DoABCTag(TagType type, int size)
            : base(type, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ABCData = ABCFile.CreateFromStream(reader, (int) reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            ABCData.ToStream(writer);
        }
    }
}
