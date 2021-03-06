﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public abstract class GradientGlowAndBevelFilter
    {
        public List<RgbaStruct> GradientColors { get; set; }
        public List<byte> GradientRatio { get; set; }
        [XmlAttribute]
        public double BlurX { get; set; }
        [XmlAttribute]
        public double BlurY { get; set; }
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
        [XmlAttribute]
        public byte Passes { get; set; }

        internal void FromStream(BitReader reader)
        {
            var numColors = reader.ReadUI8();
            GradientColors = new List<RgbaStruct>(numColors);
            for (int i = 0; i < numColors; i++)
            {
                GradientColors.Add(RgbaStruct.CreateFromStream(reader));
            }
            GradientRatio = new List<byte>(numColors);
            for (int i = 0; i < numColors; i++)
            {
                GradientRatio.Add(reader.ReadUI8());
            }
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

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte)GradientColors.Count);
            foreach (var color in GradientColors)
            {
                color.ToStream(writer);
            }
            foreach (var ratio in GradientRatio)
            {
                writer.WriteUI8(ratio);
            }
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
