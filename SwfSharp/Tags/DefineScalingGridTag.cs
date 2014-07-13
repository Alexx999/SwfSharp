using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineScalingGridTag : SwfTag
    {
        public ushort CharacterId { get; set; }
        public RectStruct Splitter { get; set; }

        public DefineScalingGridTag(int size)
            : base(TagType.DefineScalingGrid, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            Splitter = RectStruct.CreateFromStream(reader);
        }
    }
}
