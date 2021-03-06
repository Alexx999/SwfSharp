﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineBitsJPEG3Tag : DefineBitsJPEG2Tag
    {
        [XmlElement]
        public byte[] BitmapAlphaData { get; set; }
        
        public DefineBitsJPEG3Tag() : this(0)
        {
        }

        public DefineBitsJPEG3Tag(int size)
            : base(TagType.DefineBitsJPEG3, size)
        {
        }

        protected DefineBitsJPEG3Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            var dataSize = (int)reader.ReadUI32();
            ImageData = reader.ReadBytes(dataSize);
            BitmapAlphaData = reader.ReadBytes((int)reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteUI32((uint)ImageData.Length);
            writer.WriteBytes(ImageData);
            writer.WriteBytes(BitmapAlphaData);
        }
    }
}
