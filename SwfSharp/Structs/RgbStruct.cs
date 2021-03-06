﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class RgbStruct
    {
        [XmlAttribute]
        public byte R { get; set; }
        [XmlAttribute]
        public byte G { get; set; }
        [XmlAttribute]
        public byte B { get; set; }

        internal virtual void FromStream(BitReader reader)
        {
            R = reader.ReadUI8();
            G = reader.ReadUI8();
            B = reader.ReadUI8();
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteUI8(R);
            writer.WriteUI8(G);
            writer.WriteUI8(B);
        }

        internal void FromRGB15Stream(BitReader reader)
        {
            reader.ReadBits(1);
            R = (byte) reader.ReadBits(5);
            G = (byte) reader.ReadBits(5);
            B = (byte) reader.ReadBits(5);
        }

        internal void FromRGB24Stream(BitReader reader)
        {
            reader.ReadUI8();
            FromStream(reader);
        }

        public override string ToString()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
        }

        internal static RgbStruct CreateFromStream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromStream(reader);

            return result;
        }

        internal static RgbStruct CreateFromRGB15Stream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromRGB15Stream(reader);

            return result;
        }

        internal static RgbStruct CreateFromRGB24Stream(BitReader reader)
        {
            var result = new RgbStruct();

            result.FromRGB24Stream(reader);

            return result;
        }
    }
}
