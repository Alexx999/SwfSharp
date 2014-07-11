using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class UnknownTag : SwfTag
    {
        private byte[] _origBytes;

        public UnknownTag(TagType type, int size) : base(type, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            _origBytes = reader.ReadBytes(Size);
        }
    }
}
