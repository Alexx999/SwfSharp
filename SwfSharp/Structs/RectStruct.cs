using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class RectStruct
    {
        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();

            var bitsPerField = reader.ReadBits(5);

            Xmin = reader.ReadBitsSigned(bitsPerField);
            Xmax = reader.ReadBitsSigned(bitsPerField);
            Ymin = reader.ReadBitsSigned(bitsPerField);
            Ymax = reader.ReadBitsSigned(bitsPerField);
        }

        internal static RectStruct CreateFromStream(BitReader reader)
        {
            var result = new RectStruct();

            result.FromStream(reader);

            return result;
        }

        internal void WriteTo(BitWriter writer)
        {
            var bitsPerField = BitWriter.MinBitsPerField(new [] {Xmin, Xmax, Ymin, Ymax});
            writer.WriteBits(5, bitsPerField);
            writer.WriteBitsSigned(bitsPerField, Xmin);
            writer.WriteBitsSigned(bitsPerField, Xmax);
            writer.WriteBitsSigned(bitsPerField, Ymin);
            writer.WriteBitsSigned(bitsPerField, Ymax);
        }
    }
}
