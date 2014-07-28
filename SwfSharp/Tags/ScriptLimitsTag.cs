using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ScriptLimitsTag : SwfTag
    {
        [XmlAttribute]
        public ushort MaxRecursionDepth { get; set; }
        [XmlAttribute]
        public ushort ScriptTimeoutSeconds { get; set; }

        public ScriptLimitsTag() : this(0)
        {
        }

        public ScriptLimitsTag(int size)
            : base(TagType.ScriptLimits, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            MaxRecursionDepth = reader.ReadUI16();
            ScriptTimeoutSeconds = reader.ReadUI16();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(MaxRecursionDepth);
            writer.WriteUI16(ScriptTimeoutSeconds);
        }
    }
}
