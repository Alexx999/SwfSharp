using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineMorphShapeTag : SwfTag
    {
        public ushort CharacterId { get; set; }
        public RectStruct StartBounds { get; set; }
        public RectStruct EndBounds { get; set; }
        public uint Offset { get; set; }
        public MorphFillStyleArrayStruct MorphFillStyles { get; set; }
        public MorphLineStyleArrayStruct MorphLineStyles { get; set; }
        public ShapeStruct StartEdges { get; set; }
        public ShapeStruct EndEdges { get; set; }

        public DefineMorphShapeTag(int size)
            : base(TagType.DefineMorphShape, size)
        {
        }

        protected DefineMorphShapeTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            StartBounds = RectStruct.CreateFromStream(reader);
            EndBounds = RectStruct.CreateFromStream(reader);
            Offset = reader.ReadUI32();
            MorphFillStyles = MorphFillStyleArrayStruct.CreateFromStream(reader);
            MorphLineStyles = MorphLineStyleArrayStruct.CreateFromStream(reader, TagType);
            StartEdges = ShapeStruct.CreateFromStream(reader, TagType);
            EndEdges = ShapeStruct.CreateFromStream(reader, TagType);
        }
    }
}
