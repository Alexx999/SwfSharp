using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class RgbStruct
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        internal virtual void FromStream(BitReader reader)
        {
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteUI8(R);
            writer.WriteUI8(G);
            writer.WriteUI8(B);
        }

        internal void FromRGB15Stream(BitReader reader)
        {
            reader.ReadBits(1);
            R = (byte) reader.ReadBits(5);
            G = (byte) reader.ReadBits(5);
            B = (byte) reader.ReadBits(5);
        }

        internal void FromRGB24Stream(BitReader reader)
        {
            reader.ReadUI8();
            FromStream(reader);
        }

        public override string ToString()
        {
            return string.Format("#{0:X}{1:X}{2:X}", R, G, B);
        }

        internal static RgbStruct CreateFromStream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromStream(reader);

            return result;
        }

        internal static RgbStruct CreateFromRGB15Stream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromRGB15Stream(reader);

            return result;
        }

        internal static RgbStruct CreateFromRGB24Stream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromRGB24Stream(reader);

            return result;
        }
    }
}
