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

        internal override void FromStream(BitReader reader, int size)
        {
            _origBytes = reader.ReadBytes(size);
        }
    }
}
