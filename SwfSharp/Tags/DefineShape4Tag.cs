﻿using System;
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

        public DefineShape4Tag(TagType tagType, int size) : base(tagType, size)
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
    }
}