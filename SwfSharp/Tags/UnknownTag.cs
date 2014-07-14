using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class UnknownTag : SwfTag
    {
        private byte[] _origBytes;

        public UnknownTag(TagType type, int size) : base(type, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            _origBytes = reader.ReadBytes(Size);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(_origBytes);
        }
    }
}
