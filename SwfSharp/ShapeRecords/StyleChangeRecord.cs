using SwfSharp.Structs;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    public class StyleChangeRecord : ShapeRecord
    {
        public NonEdgeFlags Flags { get; set; }
        public int MoveDeltaX { get; set; }
        public int MoveDeltaY { get; set; }
        public uint FillStyle0 { get; set; }
        public uint FillStyle1 { get; set; }
        public uint LineStyle { get; set; }
        public FillStyleArray FillStyles { get; set; }
        public LineStyleArray LineStyles { get; set; }

        public StyleChangeRecord(NonEdgeFlags nonEdgeFlags) : base(ShapeRecordType.StyleChange)
        {
            Flags = nonEdgeFlags;
        }

        internal void FromStream(BitReader reader, byte numFillBits, byte numLineBits, TagType type)
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
                LineStyle = reader.ReadBits(numFillBits);
            }
            if (Flags.StateNewStyles)
            {
                FillStyles = FillStyleArray.CreateFromStream(reader, type);
                LineStyles = LineStyleArray.CreateFromStream(reader, type);
                var newNumFillBits = reader.ReadBits(4);
                var newNumLineBits = reader.ReadBits(4);
            }
        }

        internal static StyleChangeRecord CreateFromStream(NonEdgeFlags flags, byte numFillBits, byte numLineBits, TagType type, BitReader reader)
        {
            var result = new StyleChangeRecord(flags);

            result.FromStream(reader, numFillBits, numLineBits, type);

            return result;
        }
    }
}