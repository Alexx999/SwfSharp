using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineShape4Tag : DefineShape3Tag
    {
        public RectStruct EdgeBounds { get; set; }
        public bool UsesFillWindingRule { get; set; }
        public bool UsesNonScalingStrokes { get; set; }
        public bool UsesScalingStrokes { get; set; }

        public DefineShape4Tag() : this(0)
        {
        }

        public DefineShape4Tag(int size)
            : base(TagType.DefineShape4, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ShapeId = reader.ReadUI16();
            ShapeBounds = RectStruct.CreateFromStream(reader);
            EdgeBounds = RectStruct.CreateFromStream(reader);
            reader.ReadBits(5);
            UsesFillWindingRule = reader.ReadBoolBit();
            UsesNonScalingStrokes = reader.ReadBoolBit();
            UsesScalingStrokes = reader.ReadBoolBit();
            Shapes = ShapeWithStyleStruct.CreateFromStream(reader, TagType);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ShapeId);
            ShapeBounds.ToStream(writer);
            EdgeBounds.ToStream(writer);
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(UsesFillWindingRule);
            writer.WriteBoolBit(UsesNonScalingStrokes);
            writer.WriteBoolBit(UsesScalingStrokes);
            Shapes.ToStream(writer, TagType);
        }
    }
}
