using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineMorphShapeTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterId { get; set; }
        public RectStruct StartBounds { get; set; }
        public RectStruct EndBounds { get; set; }
        public MorphFillStyleArrayStruct MorphFillStyles { get; set; }
        public MorphLineStyleArrayStruct MorphLineStyles { get; set; }
        public ShapeStruct StartEdges { get; set; }
        public ShapeStruct EndEdges { get; set; }

        public DefineMorphShapeTag() : this(0)
        {
        }

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
            reader.ReadUI32();
            MorphFillStyles = MorphFillStyleArrayStruct.CreateFromStream(reader);
            MorphLineStyles = MorphLineStyleArrayStruct.CreateFromStream(reader, TagType);
            StartEdges = ShapeStruct.CreateFromStream(reader, TagType);
            EndEdges = ShapeStruct.CreateFromStream(reader, TagType);
        }

        internal uint WriteData(BitWriter writer, out byte fillbits, out byte lineBits)
        {
            var startPos = writer.Position;

            fillbits = MorphFillStyles.ToStream(writer);
            lineBits = MorphLineStyles.ToStream(writer);
            StartEdges.ToStream(writer, ref fillbits, ref lineBits, TagType);
            writer.Align();
            return (uint)(writer.Position - startPos);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            uint offset;
            byte fillbits;
            byte lineBits;
            var ms = new MemoryStream();
            using (var bitWriter = new BitWriter(ms, true))
            {
                offset = WriteData(bitWriter, out fillbits, out lineBits);
            }
            writer.WriteUI16(CharacterId);
            StartBounds.ToStream(writer);
            EndBounds.ToStream(writer);
            writer.WriteUI32(offset);
            writer.WriteBytes(ms.GetBuffer(), 0, (int) offset);
            EndEdges.ToStream(writer, ref fillbits, ref lineBits, TagType);
        }
    }
}
