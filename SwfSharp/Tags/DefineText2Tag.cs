using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Tags
{
    public class DefineText2Tag : DefineTextTag
    {
        public DefineText2Tag() : this(0)
        {
        }

        public DefineText2Tag(int size)
            : base(TagType.DefineText2, size)
        {
        }
    }
}
