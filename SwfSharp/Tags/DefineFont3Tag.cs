using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Tags
{
    public class DefineFont3Tag : DefineFont2Tag
    {
        public DefineFont3Tag() : this(0)
        {
        }

        public DefineFont3Tag(int size)
            : base(TagType.DefineFont3, size)
        {
        }
    }
}
