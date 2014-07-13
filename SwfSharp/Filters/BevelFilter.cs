﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    public class BevelFilter
    {
        public RgbaStruct ShadowColor { get; set; }
        public RgbaStruct HighlightColor { get; set; }
        public float BlurX { get; set; }
        public float BlurY { get; set; }
        public float Angle { get; set; }
        public float Distance { get; set; }
        public float Strength { get; set; }
        public bool InnerShadow { get; set; }
        public bool Knockout { get; set; }
        public bool CompositeSource { get; set; }
        public bool OnTop { get; set; }
        public byte Passes { get; set; }

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

        internal static BevelFilter CreateFromStream(BitReader reader)
        {
            var result = new BevelFilter();

            result.FromStream(reader);

            return result;
        }
    }
}