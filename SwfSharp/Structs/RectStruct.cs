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

            reader.Align();
        }

        internal static RectStruct CreateFromStream(BitReader reader)
        {
            var result = new RectStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
