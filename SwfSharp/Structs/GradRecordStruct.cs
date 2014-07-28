using System;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
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

        internal void ToStream(BitWriter writer, TagType type)
        {
            writer.WriteUI8(Ratio);
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