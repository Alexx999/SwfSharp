using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public class BlurFilter
    {
        [XmlAttribute]
        public double BlurX { get; set; }
        [XmlAttribute]
        public double BlurY { get; set; }
        [XmlAttribute]
        public byte Passes { get; set; }

        private void FromStream(BitReader reader)
        {
            BlurX = reader.ReadFixed();
            BlurY = reader.ReadFixed();
            Passes = (byte)reader.ReadBits(5);
            reader.ReadBits(3);
        }

        internal static BlurFilter CreateFromStream(BitReader reader)
        {
            var result = new BlurFilter();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteFixed(BlurX);
            writer.WriteFixed(BlurY);
            writer.WriteBits(5, Passes);
            writer.WriteBits(3, 0);
        }
    }
}
