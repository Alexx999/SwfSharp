using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineBinaryDataTag : SwfTag
    {
        public ushort Tag { get; set; }
        public byte[] Data { get; set; }

        public DefineBinaryDataTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Tag = reader.ReadUI16();
            reader.ReadUI32();
            Data = reader.ReadBytes(Size - 6);
        }
    }
}
