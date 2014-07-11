using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class SwfRgbaStruct
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }


        internal static SwfRgbaStruct CreateFromStream(BitReader reader)
        {
            var result = new SwfRgbaStruct();

            result.FromStream(reader);

            return result;
        }

        private void FromStream(BitReader reader)
        {
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
            A = reader.ReadUI8();
        }
    }
}
