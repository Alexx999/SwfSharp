﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class RemoveObject2Tag : SwfTag
    {
        [XmlAttribute]
        public ushort Depth { get; set; }

        public RemoveObject2Tag() : this(0)
        {
        }

        public RemoveObject2Tag(int size)
            : base(TagType.RemoveObject2, size)
        {
        }

        protected RemoveObject2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Depth = reader.ReadUI16();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(Depth);
        }
    }
}
