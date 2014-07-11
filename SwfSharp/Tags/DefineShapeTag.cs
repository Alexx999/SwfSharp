using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineShapeTag : SwfTag
    {
        public ushort ShapeId { get; set; }
        public RectStruct ShapeBounds { get; set; }
        public ShapeWithStyleStruct Shapes { get; set; }

        public DefineShapeTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            ShapeId = reader.ReadUI16();
            ShapeBounds = RectStruct.CreateFromStream(reader);
            Shapes = ShapeWithStyleStruct.CreateFromStream(reader, TagType);
        }
    }
}
