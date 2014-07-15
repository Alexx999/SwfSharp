using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBitsJPEG3Tag : DefineBitsJPEG2Tag
    {
        public byte[] BitmapAlphaData { get; set; }
        
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
            BitmapAlphaData = reader.ReadBytes(Size - (dataSize + 6));
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }
    }
}
