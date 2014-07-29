using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class StartSound2Tag : SwfTag
    {
        [XmlAttribute]
        public string SoundClassName { get; set; }
        [XmlElement]
        public SoundInfoStruct SoundInfo { get; set; }

        public StartSound2Tag() : this(0)
        {
        }

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
            writer.WriteString(SoundClassName, swfVersion);
            SoundInfo.ToStream(writer);
        }
    }
}
