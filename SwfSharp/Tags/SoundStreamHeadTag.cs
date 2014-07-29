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
    public class SoundStreamHeadTag : SwfTag
    {
        private short? _latencySeek;

        [XmlAttribute]
        public SampleRate PlaybackSoundRate { get; set; }
        [XmlAttribute]
        public SampleSize PlaybackSoundSize { get; set; }
        [XmlAttribute]
        public SoundType PlaybackSoundType { get; set; }
        [XmlAttribute]
        public SoundFormat StreamSoundCompression { get; set; }
        [XmlAttribute]
        public SampleRate StreamSoundRate { get; set; }
        [XmlAttribute]
        public SampleSize StreamSoundSize { get; set; }
        [XmlAttribute]
        public SoundType StreamSoundType { get; set; }
        [XmlAttribute]
        public ushort StreamSoundSampleCount { get; set; }

        [XmlAttribute]
        public short LatencySeek
        {
            get { return _latencySeek.GetValueOrDefault(); }
            set { _latencySeek = value; }
        }

        [XmlIgnore]
        public bool LatencySeekSpecified
        {
            get { return _latencySeek.HasValue; }
        }

        public SoundStreamHeadTag() : this(0)
        {
        }

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
                _latencySeek = reader.ReadSI16();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBits(4, 0);
            writer.WriteBits(2, (uint) PlaybackSoundRate);
            writer.WriteBits(1, (uint) PlaybackSoundSize);
            writer.WriteBits(1, (uint) PlaybackSoundType);
            writer.WriteBits(4, (uint) StreamSoundCompression);
            writer.WriteBits(2, (uint) StreamSoundRate);
            writer.WriteBits(1, (uint) StreamSoundSize);
            writer.WriteBits(1, (uint) StreamSoundType);
            writer.WriteUI16(StreamSoundSampleCount);
            if (StreamSoundCompression == SoundFormat.MP3)
            {
                writer.WriteSI16(_latencySeek.GetValueOrDefault());
            }
        }
    }
}
