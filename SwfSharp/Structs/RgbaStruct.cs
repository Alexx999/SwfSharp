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

        public override string ToString()
        {
            return string.Format("#{0:X}{1:X}{2:X}{3:X}", R, G, B, A);
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

        internal override void ToStream(BitWriter writer)
        {
            base.ToStream(writer);
            writer.WriteUI8(A);
        }

        internal void ToRgbStream(BitWriter writer)
        {
            base.ToStream(writer);
        }
    }
}
