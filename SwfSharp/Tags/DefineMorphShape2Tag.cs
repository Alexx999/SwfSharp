using System;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineMorphShape2Tag : DefineMorphShapeTag
    {
        [XmlElement]
        public RectStruct StartEdgeBounds { get; set; }
        [XmlElement]
        public RectStruct EndEdgeBounds { get; set; }
        [XmlAttribute]
        public bool UsesNonScalingStrokes { get; set; }
        [XmlAttribute]
        public bool UsesScalingStrokes { get; set; }

        public DefineMorphShape2Tag() : this(0)
        {
        }

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
            reader.ReadUI32();
            MorphFillStyles = MorphFillStyleArrayStruct.CreateFromStream(reader, TagType);
            MorphLineStyles = MorphLineStyleArrayStruct.CreateFromStream(reader, TagType);
            StartEdges = ShapeStruct.CreateFromStream(reader, TagType);
            EndEdges = ShapeStruct.CreateFromStream(reader, TagType);
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
            StartEdgeBounds.ToStream(writer);
            EndEdgeBounds.ToStream(writer);
            writer.WriteBits(6, 0);
            writer.WriteBoolBit(UsesNonScalingStrokes);
            writer.WriteBoolBit(UsesScalingStrokes);
            writer.WriteUI32(offset);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)offset);
            EndEdges.ToStream(writer, ref fillbits, ref lineBits, TagType);
        }
    }
}
