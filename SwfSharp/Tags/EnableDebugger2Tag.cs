using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class EnableDebugger2Tag : EnableDebuggerTag
    {
        public EnableDebugger2Tag(int size)
            : base(TagType.EnableDebugger2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(0);
            base.ToStream(writer, swfVersion);
        }
    }
}
