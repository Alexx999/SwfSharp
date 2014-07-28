using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class FrameLabelTag : SwfTag
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public bool IsNamedAnchor { get; set; }

        public FrameLabelTag() : this(0)
        {
        }

        public FrameLabelTag(int size)
            : base(TagType.FrameLabel, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Name = reader.ReadString();

            if (swfVersion >= 6 && reader.TagBytesRemaining > 0)
            {
                IsNamedAnchor = reader.ReadUI8() != 0;
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteString(Name, swfVersion);
            if (swfVersion >= 6 && IsNamedAnchor)
            {
                writer.WriteUI8(1);
            }
        }
    }
}
