﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class RectStruct
    {
        [XmlAttribute]
        public int Xmin { get; set; }
        [XmlAttribute]
        public int Xmax { get; set; }
        [XmlAttribute]
        public int Ymin { get; set; }
        [XmlAttribute]
        public int Ymax { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();

            var bitsPerField = reader.ReadBits(5);

            Xmin = reader.ReadBitsSigned(bitsPerField);
            Xmax = reader.ReadBitsSigned(bitsPerField);
            Ymin = reader.ReadBitsSigned(bitsPerField);
            Ymax = reader.ReadBitsSigned(bitsPerField);
        }

        internal static RectStruct CreateFromStream(BitReader reader)
        {
            var result = new RectStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.Align();
            writer.WriteBitSizeAndData(5, new[] { Xmin, Xmax, Ymin, Ymax });
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Xmin, Xmax, Ymin, Ymax);
        }
    }
}
