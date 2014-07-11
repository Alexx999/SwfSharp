﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBitsTag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public byte[] JPEGData { get; set; }

        public DefineBitsTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            JPEGData = reader.ReadBytes(Size - 2);
        }
    }
}
