using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class FillStyleStruct
    {
        public FillStyle FillStyleType { get; set; }
        public RgbaStruct Color { get; set; }
        public MatrixStruct GradientMatrix { get; set; }
        public GradientStruct Gradient { get; set; }
        public FocalGradientStruct FocalGradient { get; set; }
        public ushort BitmapId { get; set; }
        public MatrixStruct BitmapMatrix { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            FillStyleType = (FillStyle)reader.ReadUI8();

            if (FillStyleType == FillStyle.Solid)
            {
                if (type < TagType.DefineShape3)
                {
                    Color = RgbaStruct.CreateFromRgbStream(reader);
                }
                else
                {
                    Color = RgbaStruct.CreateFromStream(reader);
                }
            }
            if (FillStyleType == FillStyle.LinearGradient ||
                FillStyleType == FillStyle.RadialGradient ||
                FillStyleType == FillStyle.FocalRadialGradient)
            {
                GradientMatrix = MatrixStruct.CreateFromStream(reader);
                if (FillStyleType == FillStyle.FocalRadialGradient)
                {
                    FocalGradient = FocalGradientStruct.CreateFromStream(reader, type);
                }
                else
                {
                    Gradient = GradientStruct.CreateFromStream(reader, type);
                }
            }
            if (FillStyleType == FillStyle.ClippedBitmap ||
                FillStyleType == FillStyle.RepeatingBitmap ||
                FillStyleType == FillStyle.NonSmoothedClippedBitmap ||
                FillStyleType == FillStyle.NonSmoothedRepeatingBitmap)
            {
                BitmapId = reader.ReadUI16();
                BitmapMatrix = MatrixStruct.CreateFromStream(reader);
            }
        }

        internal static FillStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new FillStyleStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}
