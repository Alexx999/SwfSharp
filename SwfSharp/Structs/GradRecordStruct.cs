﻿using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class GradRecordStruct
    {
        public byte Ratio { get; set; }
        public RgbaStruct Color { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            Ratio = reader.ReadUI8();
            if (type < TagType.DefineShape3)
            {
                Color = RgbaStruct.CreateFromRgbStream(reader);
            }
            else
            {
                Color = RgbaStruct.CreateFromStream(reader);
            }
        }

        internal static GradRecordStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new GradRecordStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}