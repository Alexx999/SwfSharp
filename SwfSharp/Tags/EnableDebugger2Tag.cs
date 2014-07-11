using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class EnableDebugger2Tag : EnableDebuggerTag
    {
        public EnableDebugger2Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            reader.ReadUI16();
            base.FromStream(reader);
        }
    }
}
