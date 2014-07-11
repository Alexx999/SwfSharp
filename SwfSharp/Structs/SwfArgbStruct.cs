using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class SwfArgbStruct
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }


        internal static SwfArgbStruct CreateFromStream(BitReader reader)
        {
            var result = new SwfArgbStruct();

            result.FromStream(reader);

            return result;
        }

        private void FromStream(BitReader reader)
        {
            A = reader.ReadUI8();
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
        }
    }
}
