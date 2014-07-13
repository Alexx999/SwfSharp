﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineBitsJPEG4Tag : DefineBitsJPEG3Tag
    {
        public float DeblockParam { get; set; }

        public DefineBitsJPEG4Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            var dataSize = (int)reader.ReadUI32();
            DeblockParam = reader.ReadFixed8();
            ImageData = reader.ReadBytes(dataSize);
            BitmapAlphaData = reader.ReadBytes(Size - (dataSize + 6));
        }
    }
}