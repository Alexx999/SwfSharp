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

        internal static RgbStruct CreateFromStream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
