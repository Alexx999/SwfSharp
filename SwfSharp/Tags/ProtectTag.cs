using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class ProtectTag : SwfTag
    {
        [XmlElement]
        public byte[] Data { get; set; }

        public ProtectTag() : this(0)
        {
        }

        public ProtectTag(int size)
            : base(TagType.Protect, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Data = reader.ReadBytes(Size);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(Data);
        }
    }
}
