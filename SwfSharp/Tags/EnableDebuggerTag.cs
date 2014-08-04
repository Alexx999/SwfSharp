using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class EnableDebuggerTag : SwfTag
    {
        [XmlAttribute]
        public string Password { get; set; }

        public EnableDebuggerTag() : this(0)
        {
        }

        public EnableDebuggerTag(int size)
            : this(TagType.EnableDebugger, size)
        {
        }

        protected EnableDebuggerTag(TagType tagType, int size)
            : base(tagType, size)
        {
            Password = "";
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Password = reader.ReadString();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteString(Password, swfVersion);
        }
    }
}
