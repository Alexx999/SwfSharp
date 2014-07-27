﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfSharp.Tags
{
    public class DefineShape2Tag : DefineShapeTag
    {
        public DefineShape2Tag() : this(0)
        {
        }

        public DefineShape2Tag(int size)
            : base(TagType.DefineShape2, size)
        {
        }

        protected DefineShape2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }
    }
}
