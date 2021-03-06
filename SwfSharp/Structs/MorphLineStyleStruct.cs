﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphLineStyleStruct
    {
        [XmlAttribute]
        public ushort StartWidth { get; set; }
        [XmlAttribute]
        public ushort EndWidth { get; set; }
        [XmlElement]
        public RgbaStruct StartColor { get; set; }
        [XmlElement]
        public RgbaStruct EndColor { get; set; }

        private void FromStream(BitReader reader)
        {
            StartWidth = reader.ReadUI16();
            EndWidth = reader.ReadUI16();
            StartColor = RgbaStruct.CreateFromStream(reader);
            EndColor = RgbaStruct.CreateFromStream(reader);
        }

        internal static MorphLineStyleStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphLineStyleStruct();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteUI16(StartWidth);
            writer.WriteUI16(EndWidth);
            StartColor.ToStream(writer);
            EndColor.ToStream(writer);
        }
    }
}
