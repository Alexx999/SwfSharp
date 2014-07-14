using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class SoundStreamBlockTag : SwfTag
    {
        public byte[] StreamSoundData { get; set; }

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
            throw new NotImplementedException();
        }
    }
}
