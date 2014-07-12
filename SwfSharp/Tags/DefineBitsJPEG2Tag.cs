﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBitsJPEG2Tag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public byte[] ImageData { get; set; }

        public DefineBitsJPEG2Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            ImageData = reader.ReadBytes(Size - 2);
        }
    }
}