using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class FrameLabelTag : SwfTag
    {
        public string Name { get; set; }
        public bool IsNamedAnchor { get; set; }

        public FrameLabelTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Name = reader.ReadString();

            if (reader.TagBytesRemaining > 0)
            {
                IsNamedAnchor = reader.ReadUI8() != 0;
            }
        }
    }
}
