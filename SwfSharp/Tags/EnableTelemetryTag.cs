using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class EnableTelemetryTag : SwfTag
    {
        public byte[] PasswordHash { get; set; }

        public EnableTelemetryTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadUI16();
            if (Size > 2)
            {
                PasswordHash = reader.ReadBytes(32);
            }
        }
    }
}
