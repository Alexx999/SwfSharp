using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public class GlowFilter : BlurFilter
    {
        public RgbaStruct GlowColor { get; set; }
        public float Strength { get; set; }
        public bool InnerShadow { get; set; }
        public bool Knockout { get; set; }
        public bool CompositeSource { get; set; }

        private void FromStream(BitReader reader)
        {
            GlowColor = RgbaStruct.CreateFromStream(reader);
            BlurX = reader.ReadFixed();
            BlurY = reader.ReadFixed();
            Strength = reader.ReadFixed8();
            InnerShadow = reader.ReadBoolBit();
            Knockout = reader.ReadBoolBit();
            CompositeSource = reader.ReadBoolBit();
            Passes = (byte)reader.ReadBits(5);
        }

        internal new static GlowFilter CreateFromStream(BitReader reader)
        {
            var result = new GlowFilter();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            GlowColor.ToStream(writer);
            writer.WriteFixed(BlurX);
            writer.WriteFixed(BlurY);
            writer.WriteFixed8(Strength);
            writer.WriteBoolBit(InnerShadow);
            writer.WriteBoolBit(Knockout);
            writer.WriteBoolBit(CompositeSource);
            writer.WriteBits(5, Passes);
        }
    }
}
