using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class RgbaStruct : RgbStruct
    {
        [XmlAttribute]
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
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", R, G, B, A);
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
