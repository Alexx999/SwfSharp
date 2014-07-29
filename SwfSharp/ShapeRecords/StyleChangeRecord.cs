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
        private int? _moveDeltaX;
        private int? _moveDeltaY;
        private uint? _fillStyle0;
        private uint? _fillStyle1;
        private uint? _lineStyle;
        private NonEdgeFlags Flags { get; set; }

        [XmlAttribute]
        public int MoveDeltaX
        {
            get { return _moveDeltaX.GetValueOrDefault(); }
            set { _moveDeltaX = value; }
        }

        [XmlIgnore]
        public bool MoveDeltaXSpecified
        {
            get { return _moveDeltaX.HasValue; }
        }

        [XmlAttribute]
        public int MoveDeltaY
        {
            get { return _moveDeltaY.GetValueOrDefault(); }
            set { _moveDeltaY = value; }
        }

        [XmlIgnore]
        public bool MoveDeltaYSpecified
        {
            get { return _moveDeltaY.HasValue; }
        }

        [XmlAttribute]
        public uint FillStyle0
        {
            get { return _fillStyle0.GetValueOrDefault(); }
            set { _fillStyle0 = value; }
        }

        [XmlIgnore]
        public bool FillStyle0Specified
        {
            get { return _fillStyle0.HasValue; }
        }

        [XmlAttribute]
        public uint FillStyle1
        {
            get { return _fillStyle1.GetValueOrDefault(); }
            set { _fillStyle1 = value; }
        }

        [XmlIgnore]
        public bool FillStyle1Specified
        {
            get { return _fillStyle1.HasValue; }
        }

        [XmlAttribute]
        public uint LineStyle
        {
            get { return _lineStyle.GetValueOrDefault(); }
            set { _lineStyle = value; }
        }

        [XmlIgnore]
        public bool LineStyleSpecified
        {
            get { return _lineStyle.HasValue; }
        }

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
                _moveDeltaX = reader.ReadBitsSigned(moveBits);
                _moveDeltaY = reader.ReadBitsSigned(moveBits);
            }
            if (Flags.StateFillStyle0)
            {
                _fillStyle0 = reader.ReadBits(numFillBits);
            }
            if (Flags.StateFillStyle1)
            {
                _fillStyle1 = reader.ReadBits(numFillBits);
            }
            if (Flags.StateLineStyle)
            {
                _lineStyle = reader.ReadBits(numLineBits);
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

            Flags.StateMoveTo = (_moveDeltaX.HasValue) || (_moveDeltaY.HasValue);
            Flags.StateFillStyle0 = _fillStyle0.HasValue;
            Flags.StateFillStyle1 = _fillStyle1.HasValue;
            Flags.StateLineStyle = _lineStyle.HasValue;
            Flags.StateNewStyles = canHaveNewStyles && LineStyles != null && FillStyles != null &&
                                   ((LineStyles.LineStyles.Count > 0) || (FillStyles.FillStyles.Count > 0));

            writer.WriteBits(1, 0);
            Flags.ToStream(writer);

            if (Flags.StateMoveTo)
            {
                writer.WriteBitSizeAndData(5, new[] { _moveDeltaX.GetValueOrDefault(), _moveDeltaY.GetValueOrDefault() });
            }
            if (Flags.StateFillStyle0)
            {
                writer.WriteBits(numFillBits, _fillStyle0.Value);
            }
            if (Flags.StateFillStyle1)
            {
                writer.WriteBits(numFillBits, _fillStyle1.Value);
            }
            if (Flags.StateLineStyle)
            {
                writer.WriteBits(numLineBits, _lineStyle.Value);
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