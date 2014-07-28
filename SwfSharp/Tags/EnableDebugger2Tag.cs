using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class EnableDebugger2Tag : EnableDebuggerTag
    {
        [XmlAttribute]
        public ushort Reserved { get; set; }

        public EnableDebugger2Tag() : this(0)
        {
        }

        public EnableDebugger2Tag(int size)
            : base(TagType.EnableDebugger2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Reserved = reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(Reserved);
            base.ToStream(writer, swfVersion);
        }
    }
}
