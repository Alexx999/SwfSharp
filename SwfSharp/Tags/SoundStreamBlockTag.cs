using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class SoundStreamBlockTag : SwfTag
    {
        [XmlElement]
        public byte[] StreamSoundData { get; set; }

        public SoundStreamBlockTag() : this(0)
        {
        }

        public SoundStreamBlockTag(int size)
            : base(TagType.SoundStreamBlock, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            StreamSoundData = reader.ReadBytes(Size);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(StreamSoundData);
        }
    }
}
