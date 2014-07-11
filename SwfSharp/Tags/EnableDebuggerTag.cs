﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class EnableDebuggerTag : SwfTag
    {
        public string Password { get; set; }

        public EnableDebuggerTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Password = reader.ReadString();
        }
    }
}