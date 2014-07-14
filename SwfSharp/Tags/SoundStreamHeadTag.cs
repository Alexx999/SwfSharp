using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Sounds;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class SoundStreamHeadTag : SwfTag
    {
        public SampleRate PlaybackSoundRate { get; set; }
        public SampleSize PlaybackSoundSize { get; set; }
        public SoundType PlaybackSoundType { get; set; }
        public SoundFormat StreamSoundCompression { get; set; }
        public SampleRate StreamSoundRate { get; set; }
        public SampleSize StreamSoundSize { get; set; }
        public SoundType StreamSoundType { get; set; }
        public ushort StreamSoundSampleCount { get; set; }
        public short LatencySeek { get; set; }

        public SoundStreamHeadTag(int size)
            : base(TagType.SoundStreamHead, size)
        {
        }

        protected SoundStreamHeadTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadBits(4);
            PlaybackSoundRate = (SampleRate) reader.ReadBits(2);
            PlaybackSoundSize = (SampleSize) reader.ReadBits(1);
            PlaybackSoundType = (SoundType) reader.ReadBits(1);
            StreamSoundCompression = (SoundFormat) reader.ReadBits(4);
            StreamSoundRate = (SampleRate) reader.ReadBits(2);
            StreamSoundSize = (SampleSize)reader.ReadBits(1);
            StreamSoundType = (SoundType)reader.ReadBits(1);
            StreamSoundSampleCount = reader.ReadUI16();
            if (StreamSoundCompression == SoundFormat.MP3)
            {
                LatencySeek = reader.ReadSI16();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }
    }
}
