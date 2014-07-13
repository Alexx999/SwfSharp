using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class MorphFillStyleStruct
    {
        public FillStyle FillStyleType { get; set; }
        public RgbaStruct StartColor { get; set; }
        public RgbaStruct EndColor { get; set; }
        public MatrixStruct StartGradientMatrix { get; set; }
        public MatrixStruct EndGradientMatrix { get; set; }
        public MorphGradientStruct Gradient { get; set; }
        public ushort BitmapId { get; set; }
        public MatrixStruct StartBitmapMatrix { get; set; }
        public MatrixStruct EndBitmapMatrix { get; set; }

        private void FromStream(BitReader reader)
        {
            FillStyleType = (FillStyle)reader.ReadUI8();

            if (FillStyleType == FillStyle.Solid)
            {
                StartColor = RgbaStruct.CreateFromStream(reader);
                EndColor = RgbaStruct.CreateFromStream(reader);
            }
            if (FillStyleType == FillStyle.LinearGradient ||
                FillStyleType == FillStyle.RadialGradient)
            {
                StartGradientMatrix = MatrixStruct.CreateFromStream(reader);
                EndGradientMatrix = MatrixStruct.CreateFromStream(reader);
                Gradient = MorphGradientStruct.CreateFromStream(reader);
            }
            if (FillStyleType == FillStyle.ClippedBitmap ||
                FillStyleType == FillStyle.RepeatingBitmap ||
                FillStyleType == FillStyle.NonSmoothedClippedBitmap ||
                FillStyleType == FillStyle.NonSmoothedRepeatingBitmap)
            {
                BitmapId = reader.ReadUI16();
                StartBitmapMatrix = MatrixStruct.CreateFromStream(reader);
                EndBitmapMatrix = MatrixStruct.CreateFromStream(reader);
            }
        }

        internal static MorphFillStyleStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphFillStyleStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
