using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class ShowFrameTag : SwfTag
    {
        public ShowFrameTag() : this(0)
        {
        }

        public ShowFrameTag(int size)
            : base(TagType.ShowFrame, size)
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
