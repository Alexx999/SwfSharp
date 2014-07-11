using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DoABCTag : SwfTag
    {
        public uint Flags { get; set; }
        public string Name { get; set; }
        public byte[] ABCData { get; set; }

        public DoABCTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            Flags = reader.ReadUI32();
            string name;
            var len = reader.ReadString(out name);
            Name = name;
            ABCData = reader.ReadBytes(Size - (len + 4));
        }
    }
}
