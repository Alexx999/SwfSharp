using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ProtectTag : SwfTag
    {
        public byte[] Data { get; set; }

        public ProtectTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Data = reader.ReadBytes(Size);
        }
    }
}
