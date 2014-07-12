using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class SoundStreamBlockTag : SwfTag
    {
        public byte[] StreamSoundData { get; set; }

        public SoundStreamBlockTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            StreamSoundData = reader.ReadBytes(Size);
        }
    }
}
