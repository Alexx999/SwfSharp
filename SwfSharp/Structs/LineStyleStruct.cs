﻿using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class LineStyleStruct
    {
        [XmlAttribute]
        public ushort Width { get; set; }
        [XmlElement]
        public RgbaStruct Color { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            Width = reader.ReadUI16();
            if (type < TagType.DefineShape3)
            {
                Color = RgbaStruct.CreateFromRgbStream(reader);
            }
            else
            {
                Color = RgbaStruct.CreateFromStream(reader);
            }
        }

        internal static LineStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new LineStyleStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal virtual void ToStream(BitWriter writer, TagType type)
        {
            writer.WriteUI16(Width);
            if (type < TagType.DefineShape3)
            {
                Color.ToRgbStream(writer);
            }
            else
            {
                Color.ToStream(writer);
            }
        }
    }
}
