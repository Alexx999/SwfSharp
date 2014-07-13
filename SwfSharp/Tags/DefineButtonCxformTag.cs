using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineButtonCxformTag : SwfTag
    {
        public ushort ButtonId { get; set; }
        public CXformStruct ButtonColorTransform { get; set; }

        public DefineButtonCxformTag(int size)
            : base(TagType.DefineButtonCxform, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            ButtonColorTransform = CXformStruct.CreateFromStream(reader);
        }
    }
}
