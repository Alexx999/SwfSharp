using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class RgbaStruct : RgbStruct
    {
        public byte A { get; set; }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            A = reader.ReadUI8();
        }

        internal void FromRgbStream(BitReader reader)
        {
            base.FromStream(reader);
            A = byte.MaxValue;
        }

        internal new static RgbaStruct CreateFromStream(BitReader reader)
        {
            var result = new RgbaStruct();

            result.FromStream(reader);

            return result;
        }

        internal static RgbaStruct CreateFromRgbStream(BitReader reader)
        {
            var result = new RgbaStruct();

            result.FromRgbStream(reader);

            return result;
        }
    }
}
