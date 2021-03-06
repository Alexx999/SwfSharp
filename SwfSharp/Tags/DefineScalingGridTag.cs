﻿using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineScalingGridTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterId { get; set; }
        [XmlElement]
        public RectStruct Splitter { get; set; }

        public DefineScalingGridTag() : this(0)
        {
        }

        public DefineScalingGridTag(int size)
            : base(TagType.DefineScalingGrid, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            Splitter = RectStruct.CreateFromStream(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterId);
            Splitter.ToStream(writer);
        }
    }
}
