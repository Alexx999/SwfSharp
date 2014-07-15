using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class GlyphEntryStruct
    {
        public uint GlyphIndex { get; set; }
        public int GlyphAdvance { get; set; }

        private void FromStream(BitReader reader, byte glyphBits, byte advanceBits)
        {
            GlyphIndex = reader.ReadBits(glyphBits);
            GlyphAdvance = reader.ReadBitsSigned(advanceBits);
        }

        internal static GlyphEntryStruct CreateFromStream(BitReader reader, byte glyphBits, byte advanceBits)
        {
            var result = new GlyphEntryStruct();

            result.FromStream(reader, glyphBits, advanceBits);

            return result;
        }

        internal void ToStream(BitWriter writer, byte glyphBits, byte advanceBits)
        {
            writer.WriteBits(glyphBits, GlyphIndex);
            writer.WriteBitsSigned(advanceBits, GlyphAdvance);
        }
    }
}