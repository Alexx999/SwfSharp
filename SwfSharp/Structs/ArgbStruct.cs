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

        public override string ToString()
        {
            return string.Format("#{0:X}{1:X}{2:X}{3:X}", A, R, G, B);
        }

        internal static ArgbStruct CreateFromStream(BitReader reader)
        {
            var result = new ArgbStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
