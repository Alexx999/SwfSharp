using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBitsJPEG4Tag : DefineBitsJPEG3Tag
    {
        public float DeblockParam { get; set; }

        public DefineBitsJPEG4Tag(int size)
            : base(TagType.DefineBitsJPEG4, size)
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
