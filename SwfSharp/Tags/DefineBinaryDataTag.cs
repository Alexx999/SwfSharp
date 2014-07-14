using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBinaryDataTag : SwfTag
    {
        public ushort Tag { get; set; }
        public byte[] Data { get; set; }

        public DefineBinaryDataTag(int size)
            : base(TagType.DefineBinaryData, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Tag = reader.ReadUI16();
            reader.ReadUI32();
            Data = reader.ReadBytes(Size - 6);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }
    }
}
