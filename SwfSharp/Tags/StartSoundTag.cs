﻿using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class StartSoundTag : SwfTag
    {
        [XmlAttribute]
        public ushort SoundId { get; set; }
        [XmlElement]
        public SoundInfoStruct SoundInfo { get; set; }

        public StartSoundTag() : this(0)
        {
        }

        public StartSoundTag(int size)
            : base(TagType.StartSound, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SoundId = reader.ReadUI16();
            SoundInfo = SoundInfoStruct.CreateFromStream(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(SoundId);
            SoundInfo.ToStream(writer);
        }
    }
}
