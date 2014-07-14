using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Sounds;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineSoundTag : SwfTag
    {
        public ushort SoundId { get; set; }
        public SoundFormat SoundFormat { get; set; }
        public SampleRate SoundRate { get; set; }
        public SampleSize SoundSize { get; set; }
        public SoundType SoundType { get; set; }
        public uint SoundSampleCount { get; set; }
        public byte[] SoundData { get; set; }

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
            throw new NotImplementedException();
        }
    }
}
