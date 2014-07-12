using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public SoundStreamHeadTag(TagType tagType, int size) : base(tagType, size)
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

        public enum SampleSize
        {
            Size8Bits = 0,
            Size16Bits = 1
        }

        public enum SampleRate
        {
            Rate5KHz = 0,
            Rate11KHz = 1,
            Rate22KHz = 2,
            Rate44KHz = 3,
        }

        public enum SoundType
        {
            Mono = 0,
            Stereo = 1
        }

        public enum SoundFormat
        {
            ADPCM = 1,
            MP3 = 2
        }
    }
}
