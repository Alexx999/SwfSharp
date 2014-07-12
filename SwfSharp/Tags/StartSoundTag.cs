using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class StartSoundTag : SwfTag
    {
        public ushort SoundId { get; set; }
        public SoundInfoStruct SoundInfo { get; set; }

        public StartSoundTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SoundId = reader.ReadUI16();
            SoundInfo = SoundInfoStruct.CreateFromStream(reader);
        }
    }
}
