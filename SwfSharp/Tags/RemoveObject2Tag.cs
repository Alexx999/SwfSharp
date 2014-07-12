﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class RemoveObject2Tag : SwfTag
    {
        public ushort Depth { get; set; }

        public RemoveObject2Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Depth = reader.ReadUI16();
        }
    }
}