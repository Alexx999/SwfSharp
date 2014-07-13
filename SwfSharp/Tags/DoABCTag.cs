using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DoABCTag : SwfTag
    {
        public uint Flags { get; set; }
        public string Name { get; set; }
        public byte[] ABCData { get; set; }

        public DoABCTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Flags = reader.ReadUI32();
            Name = reader.ReadString();
            ABCData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }
    }
}
