using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Sounds;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineSoundTag : SwfTag
    {
        [XmlAttribute]
        public ushort SoundId { get; set; }
        [XmlAttribute]
        public SoundFormat SoundFormat { get; set; }
        [XmlAttribute]
        public SampleRate SoundRate { get; set; }
        [XmlAttribute]
        public SampleSize SoundSize { get; set; }
        [XmlAttribute]
        public SoundType SoundType { get; set; }
        [XmlAttribute]
        public uint SoundSampleCount { get; set; }
        public byte[] SoundData { get; set; }

        public DefineSoundTag() : this(0)
        {
        }

        public DefineSoundTag(int size)
            : base(TagType.DefineSound, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SoundId = reader.ReadUI16();
            SoundFormat = (SoundFormat) reader.ReadBits(4);
            SoundRate = (SampleRate) reader.ReadBits(2);
            SoundSize = (SampleSize) reader.ReadBits(1);
            SoundType = (SoundType) reader.ReadBits(1);
            SoundSampleCount = reader.ReadUI32();
            SoundData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(SoundId);
            writer.WriteBits(4, (uint) SoundFormat);
            writer.WriteBits(2, (uint) SoundRate);
            writer.WriteBits(1, (uint) SoundSize);
            writer.WriteBits(1, (uint) SoundType);
            writer.WriteUI32(SoundSampleCount);
            writer.WriteBytes(SoundData);
        }
    }
}
