using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineScalingGridTag : SwfTag
    {
        public ushort CharacterId { get; set; }
        public RectStruct Splitter { get; set; }

        public DefineScalingGridTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            Splitter = RectStruct.CreateFromStream(reader);
        }
    }
}
