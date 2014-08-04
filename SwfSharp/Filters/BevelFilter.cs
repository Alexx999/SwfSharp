using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public class BevelFilter : BlurFilter
    {
        public RgbaStruct ShadowColor { get; set; }
        public RgbaStruct HighlightColor { get; set; }
        [XmlAttribute]
        public double Angle { get; set; }
        [XmlAttribute]
        public double Distance { get; set; }
        [XmlAttribute]
        public float Strength { get; set; }
        [XmlAttribute]
        public bool InnerShadow { get; set; }
        [XmlAttribute]
        public bool Knockout { get; set; }
        [XmlAttribute]
        public bool CompositeSource { get; set; }
        [XmlAttribute]
        public bool OnTop { get; set; }

        private void FromStream(BitReader reader)
        {
            ShadowColor = RgbaStruct.CreateFromStream(reader);
            HighlightColor = RgbaStruct.CreateFromStream(reader);
            BlurX = reader.ReadFixed();
            BlurY = reader.ReadFixed();
            Angle = reader.ReadFixed();
            Distance = reader.ReadFixed();
            Strength = reader.ReadFixed8();
            InnerShadow = reader.ReadBoolBit();
            Knockout = reader.ReadBoolBit();
            CompositeSource = reader.ReadBoolBit();
            OnTop = reader.ReadBoolBit();
            Passes = (byte)reader.ReadBits(4);
        }

        internal new static BevelFilter CreateFromStream(BitReader reader)
        {
            var result = new BevelFilter();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            ShadowColor.ToStream(writer);
            HighlightColor.ToStream(writer);
            writer.WriteFixed(BlurX);
            writer.WriteFixed(BlurY);
            writer.WriteFixed(Angle);
            writer.WriteFixed(Distance);
            writer.WriteFixed8(Strength);
            writer.WriteBoolBit(InnerShadow);
            writer.WriteBoolBit(Knockout);
            writer.WriteBoolBit(CompositeSource);
            writer.WriteBoolBit(OnTop);
            writer.WriteBits(4, Passes);
        }
    }
}
