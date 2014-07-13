using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Tags
{
    class SoundStreamHead2Tag : SoundStreamHeadTag
    {
        public SoundStreamHead2Tag(int size)
            : base(TagType.SoundStreamHead2, size)
        {
        }
    }
}
