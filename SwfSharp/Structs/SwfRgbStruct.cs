using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class SwfRgbStruct
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }


        internal static SwfRgbStruct CreateFromStream(BitReader reader)
        {
            var result = new SwfRgbStruct();

            result.FromStream(reader);

            return result;
        }

        private void FromStream(BitReader reader)
        {
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
        }
    }
}
