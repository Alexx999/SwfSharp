﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class SetTabIndexTab : SwfTag
    {
        public ushort Depth { get; set; }
        public ushort TabIndex { get; set; }

        public SetTabIndexTab(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Depth = reader.ReadUI16();
            TabIndex = reader.ReadUI16();
        }
    }
}