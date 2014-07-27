using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class UnknownTag : SwfTag
    {
        public byte[] Bytes { get; set; }

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
