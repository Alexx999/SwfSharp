using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Tags
{
    public class DefineShape3Tag : DefineShape2Tag
    {
        public DefineShape3Tag(int size)
            : base(TagType.DefineShape3, size)
        {
        }

        protected DefineShape3Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }
    }
}
