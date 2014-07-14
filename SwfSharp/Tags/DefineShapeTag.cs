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

        public DefineShapeTag(int size)
            : base(TagType.DefineShape, size)
        {
        }

        protected DefineShapeTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ShapeId = reader.ReadUI16();
            ShapeBounds = RectStruct.CreateFromStream(reader);
            Shapes = ShapeWithStyleStruct.CreateFromStream(reader, TagType);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ShapeId);
            ShapeBounds.ToStream(writer);
            Shapes.ToStream(writer, TagType);
        }
    }
}
