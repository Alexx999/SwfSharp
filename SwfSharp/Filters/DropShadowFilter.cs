using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    public class DropShadowFilter
    {
        public RgbaStruct DropShadowColor { get; set; }
        public float BlurX { get; set; }
        public float BlurY { get; set; }
        public float Angle { get; set; }
        public float Distance { get; set; }
        public float Strength { get; set; }
        public bool InnerShadow { get; set; }
        public bool Knockout { get; set; }
        public bool CompositeSource { get; set; }
        public byte Passes { get; set; }

        private void FromStream(BitReader reader)
        {
            DropShadowColor = RgbaStruct.CreateFromStream(reader);
            BlurX = reader.ReadFixed();
            BlurY = reader.ReadFixed();
            Angle = reader.ReadFixed();
            Distance = reader.ReadFixed();
            Strength = reader.ReadFixed8();
            InnerShadow = reader.ReadBoolBit();
            Knockout = reader.ReadBoolBit();
            CompositeSource = reader.ReadBoolBit();
            Passes = (byte) reader.ReadBits(5);
        }

        internal static DropShadowFilter CreateFromStream(BitReader reader)
        {
            var result = new DropShadowFilter();

            result.FromStream(reader);

            return result;
        }
    }
}
