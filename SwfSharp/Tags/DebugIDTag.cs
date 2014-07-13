﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DebugIDTag : SwfTag
    {
        public byte[] Uuid { get; set; }

        public DebugIDTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Uuid = reader.ReadBytes(16);
        }
    }
}
