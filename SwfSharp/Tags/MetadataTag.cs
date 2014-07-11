﻿using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class MetadataTag : SwfTag
    {
        public string Metadata { get; set; }

        public MetadataTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Metadata = reader.ReadString(Size);
        }
    }
}
