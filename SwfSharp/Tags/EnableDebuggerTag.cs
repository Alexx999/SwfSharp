using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class EnableDebuggerTag : SwfTag
    {
        public string Password { get; set; }

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
