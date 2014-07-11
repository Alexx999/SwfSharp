using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class CXformWithAlphaStruct
    {
        public bool HasAddTerms { get; set; }
        public bool HasMultTerms { get; set; }
        public int RedMultTerm { get; set; }
        public int GreenMultTerm { get; set; }
        public int BlueMultTerm { get; set; }
        public int AlphaMultTerm { get; set; }
        public int RedAddTerm { get; set; }
        public int GreenAddTerm { get; set; }
        public int BlueAddTerm { get; set; }
        public int AlphaAddTerm { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();

            HasAddTerms = reader.ReadBoolBit();
            HasMultTerms = reader.ReadBoolBit();

            var nbits = reader.ReadBits(4);

            if (HasMultTerms)
            {
                RedMultTerm = reader.ReadBitsSigned(nbits);
                GreenMultTerm = reader.ReadBitsSigned(nbits);
                BlueMultTerm = reader.ReadBitsSigned(nbits);
                AlphaMultTerm = reader.ReadBitsSigned(nbits);
            }
            if (HasAddTerms)
            {
                RedAddTerm = reader.ReadBitsSigned(nbits);
                GreenAddTerm = reader.ReadBitsSigned(nbits);
                BlueAddTerm = reader.ReadBitsSigned(nbits);
                AlphaAddTerm = reader.ReadBitsSigned(nbits);
            }
        }

        internal static CXformWithAlphaStruct CreateFromStream(BitReader reader)
        {
            var result = new CXformWithAlphaStruct();

            result.FromStream(reader);

            return result;
        }
    }
}