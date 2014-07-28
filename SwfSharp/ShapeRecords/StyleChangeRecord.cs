using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [Serializable]
    public class StyleChangeRecord : ShapeRecord
    {
        private NonEdgeFlags Flags { get; set; }
        [XmlAttribute]
        public int MoveDeltaX { get; set; }
        [XmlAttribute]
        public int MoveDeltaY { get; set; }
        [XmlAttribute]
        public uint FillStyle0 { get; set; }
        [XmlAttribute]
        public uint FillStyle1 { get; set; }
        [XmlAttribute]
        public uint LineStyle { get; set; }
        public FillStyleArray FillStyles { get; set; }
        public LineStyleArray LineStyles { get; set; }

        public StyleChangeRecord() : this(new NonEdgeFlags())
        {}

        private StyleChangeRecord(NonEdgeFlags nonEdgeFlags) : base(ShapeRecordType.StyleChange)
        {
            Flags = nonEdgeFlags;
        }

        internal void FromStream(BitReader reader, ref byte numFillBits, ref byte numLineBits, TagType type)
        {
            if (Flags.StateMoveTo)
            {
                var moveBits = reader.ReadBits(5);
                MoveDeltaX = reader.ReadBitsSigned(moveBits);
                MoveDeltaY = reader.ReadBitsSigned(moveBits);
            }
            if (Flags.StateFillStyle0)
            {
                FillStyle0 = reader.ReadBits(numFillBits);
            }
            if (Flags.StateFillStyle1)
            {
                FillStyle1 = reader.ReadBits(numFillBits);
            }
            if (Flags.StateLineStyle)
            {
                LineStyle = reader.ReadBits(numLineBits);
            }
            var canHaveNewStyles = type == TagType.DefineShape2 || type == TagType.DefineShape3 ||
                                type == TagType.DefineShape4;
            if (canHaveNewStyles && Flags.StateNewStyles)
            {
                FillStyles = FillStyleArray.CreateFromStream(reader, type);
                LineStyles = LineStyleArray.CreateFromStream(reader, type);
                reader.Align();
                numFillBits = (byte) reader.ReadBits(4);
                numLineBits = (byte) reader.ReadBits(4);
            }
        }

        internal static StyleChangeRecord CreateFromStream(NonEdgeFlags flags, ref byte numFillBits, ref byte numLineBits, TagType type, BitReader reader)
        {
            var result = new StyleChangeRecord(flags);

            result.FromStream(reader, ref numFillBits, ref numLineBits, type);

            return result;
        }

        internal override void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType type)
        {
            var canHaveNewStyles = type == TagType.DefineShape2 || type == TagType.DefineShape3 ||
                                type == TagType.DefineShape4;

            Flags.StateMoveTo = (MoveDeltaX != 0) || (MoveDeltaY != 0);
            Flags.StateFillStyle0 = FillStyle0 != 0;
            Flags.StateFillStyle1 = FillStyle1 != 0;
            Flags.StateLineStyle = LineStyle != 0;
            Flags.StateNewStyles = canHaveNewStyles && LineStyles != null && FillStyles != null &&
                                   ((LineStyles.LineStyles.Count > 0) || (FillStyles.FillStyles.Count > 0));

            writer.WriteBits(1, 0);
            Flags.ToStream(writer);

            if (Flags.StateMoveTo)
            {
                writer.WriteBitSizeAndData(5, new[] { MoveDeltaX, MoveDeltaY });
            }
            if (Flags.StateFillStyle0)
            {
                writer.WriteBits(numFillBits, FillStyle0);
            }
            if (Flags.StateFillStyle1)
            {
                writer.WriteBits(numFillBits, FillStyle1);
            }
            if (Flags.StateLineStyle)
            {
                writer.WriteBits(numLineBits, LineStyle);
            }
            if (Flags.StateNewStyles)
            {
                numFillBits = FillStyles.ToStream(writer, type);
                numLineBits = LineStyles.ToStream(writer, type);
                writer.Align();
                writer.WriteBits(4, numFillBits);
                writer.WriteBits(4, numLineBits);
            }
        }
    }
}