using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DoInitActionTag : DoActionTag
    {
        [XmlAttribute]
        public ushort SpriteID { get; set; }

        public DoInitActionTag() : this(0)
        {
        }

        public DoInitActionTag(int size)
            : base(TagType.DoInitAction, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SpriteID = reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(SpriteID);
            base.ToStream(writer, swfVersion);
        }
    }
}
