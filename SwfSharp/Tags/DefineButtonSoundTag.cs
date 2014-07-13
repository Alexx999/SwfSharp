using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineButtonSoundTag : SwfTag
    {
        public ushort ButtonId { get; set; }
        public ushort ButtonSoundChar0 { get; set; }
        public SoundInfoStruct ButtonSoundInfo0 { get; set; }
        public ushort ButtonSoundChar1 { get; set; }
        public SoundInfoStruct ButtonSoundInfo1 { get; set; }
        public ushort ButtonSoundChar2 { get; set; }
        public SoundInfoStruct ButtonSoundInfo2 { get; set; }
        public ushort ButtonSoundChar3 { get; set; }
        public SoundInfoStruct ButtonSoundInfo3 { get; set; }

        public DefineButtonSoundTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            ButtonSoundChar0 = reader.ReadUI16();
            if (ButtonSoundChar0 != 0)
            {
                ButtonSoundInfo0 = SoundInfoStruct.CreateFromStream(reader);
            }
            ButtonSoundChar1 = reader.ReadUI16();
            if (ButtonSoundChar1 != 0)
            {
                ButtonSoundInfo1 = SoundInfoStruct.CreateFromStream(reader);
            }
            ButtonSoundChar2 = reader.ReadUI16();
            if (ButtonSoundChar2 != 0)
            {
                ButtonSoundInfo2 = SoundInfoStruct.CreateFromStream(reader);
            }
            ButtonSoundChar3 = reader.ReadUI16();
            if (ButtonSoundChar3 != 0)
            {
                ButtonSoundInfo3 = SoundInfoStruct.CreateFromStream(reader);
            }
        }
    }
}
