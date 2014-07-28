using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineBitsJPEG4Tag : DefineBitsJPEG3Tag
    {
        [XmlAttribute]
        public float DeblockParam { get; set; }

        public DefineBitsJPEG4Tag() : this(0)
        {
        }

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
            BitmapAlphaData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteUI32((uint) ImageData.Length);
            writer.WriteFixed8(DeblockParam);
            writer.WriteBytes(ImageData);
            writer.WriteBytes(BitmapAlphaData);
        }
    }
}
