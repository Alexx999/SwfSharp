using System;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class CXformWithAlphaStruct : CXformStruct
    {
        public int AlphaMultTerm { get; set; }
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

        internal new static CXformWithAlphaStruct CreateFromStream(BitReader reader)
        {
            var result = new CXformWithAlphaStruct();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            writer.Align();

            writer.WriteBoolBit(HasAddTerms);
            writer.WriteBoolBit(HasMultTerms);

            uint nbits = 0;

            if (HasMultTerms)
            {
                nbits = BitWriter.MinBitsPerField(new[] {RedMultTerm, GreenMultTerm, BlueMultTerm, AlphaMultTerm});
            }
            if (HasAddTerms)
            {
                nbits = Math.Max(BitWriter.MinBitsPerField(new[] { RedAddTerm, GreenAddTerm, BlueAddTerm, AlphaAddTerm }), nbits);
            }

            writer.WriteBits(4, nbits);

            if (HasMultTerms)
            {
                writer.WriteBitsSigned(nbits, RedMultTerm);
                writer.WriteBitsSigned(nbits, GreenMultTerm);
                writer.WriteBitsSigned(nbits, BlueMultTerm);
                writer.WriteBitsSigned(nbits, AlphaMultTerm);
            }
            if (HasAddTerms)
            {
                writer.WriteBitsSigned(nbits, RedAddTerm);
                writer.WriteBitsSigned(nbits, GreenAddTerm);
                writer.WriteBitsSigned(nbits, BlueAddTerm);
                writer.WriteBitsSigned(nbits, AlphaAddTerm);
            }
        }
    }
}