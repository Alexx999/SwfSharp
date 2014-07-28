using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class EndTag : SwfTag
    {
        public EndTag() : this(0)
        {
        }

        public EndTag(int size)
            : base(TagType.End, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
        }
    }
}
