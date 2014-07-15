using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineButtonSoundTag : SwfTag
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

        public DefineButtonSoundTag(int size)
            : base(TagType.DefineButtonSound, size)
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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ButtonId);
            writer.WriteUI16(ButtonSoundChar0);
            if (ButtonSoundChar0 != 0)
            {
                ButtonSoundInfo0.ToStream(writer);
            }
            writer.WriteUI16(ButtonSoundChar1);
            if (ButtonSoundChar1 != 0)
            {
                ButtonSoundInfo1.ToStream(writer);
            }
            writer.WriteUI16(ButtonSoundChar2);
            if (ButtonSoundChar2 != 0)
            {
                ButtonSoundInfo2.ToStream(writer);
            }
            writer.WriteUI16(ButtonSoundChar3);
            if (ButtonSoundChar3 != 0)
            {
                ButtonSoundInfo3.ToStream(writer);
            }
        }
    }
}
