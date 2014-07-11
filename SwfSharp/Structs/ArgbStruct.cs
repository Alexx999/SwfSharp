using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ArgbStruct
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        private void FromStream(BitReader reader)
        {
            A = reader.ReadUI8();
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
        }

        internal static ArgbStruct CreateFromStream(BitReader reader)
        {
            var result = new ArgbStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
