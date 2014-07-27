﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DebugIDTag : SwfTag
    {
        public byte[] Uuid { get; set; }

        public DebugIDTag() : this(0)
        {
        }

        public DebugIDTag(int size)
            : base(TagType.DebugID, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Uuid = reader.ReadBytes(16);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(Uuid);
        }
    }
}
