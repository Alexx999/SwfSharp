using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class FrameLabelTag : SwfTag
    {
        public string Name { get; set; }
        public bool IsNamedAnchor { get; set; }

        public FrameLabelTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            string name;
            var len = reader.ReadString(out name);
            Name = name;

            if (Size > len)
            {
                IsNamedAnchor = reader.ReadUI8() != 0;
            }
        }
    }
}
