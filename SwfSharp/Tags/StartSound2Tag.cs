using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class StartSound2Tag : SwfTag
    {
        public string SoundClassName { get; set; }
        public SoundInfoStruct SoundInfo { get; set; }

        public StartSound2Tag(int size)
            : base(TagType.StartSound2, size)
        {
        }
        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SoundClassName = reader.ReadString();
            SoundInfo = SoundInfoStruct.CreateFromStream(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }
    }
}
