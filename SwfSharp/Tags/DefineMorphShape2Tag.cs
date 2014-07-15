using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineMorphShape2Tag : DefineMorphShapeTag
    {
        public RectStruct StartEdgeBounds { get; set; }
        public RectStruct EndEdgeBounds { get; set; }
        public bool UsesNonScalingStrokes { get; set; }
        public bool UsesScalingStrokes { get; set; }

        public DefineMorphShape2Tag(int size)
            : base(TagType.DefineMorphShape2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            StartBounds = RectStruct.CreateFromStream(reader);
            EndBounds = RectStruct.CreateFromStream(reader);
            StartEdgeBounds = RectStruct.CreateFromStream(reader);
            EndEdgeBounds = RectStruct.CreateFromStream(reader);
            reader.ReadBits(6);
            UsesNonScalingStrokes = reader.ReadBoolBit();
            UsesScalingStrokes = reader.ReadBoolBit();
            Offset = reader.ReadUI32();
            MorphFillStyles = MorphFillStyleArrayStruct.CreateFromStream(reader);
            MorphLineStyles = MorphLineStyleArrayStruct.CreateFromStream(reader, TagType);
            StartEdges = ShapeStruct.CreateFromStream(reader, TagType);
            EndEdges = ShapeStruct.CreateFromStream(reader, TagType);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            throw new NotImplementedException();
        }
    }
}
