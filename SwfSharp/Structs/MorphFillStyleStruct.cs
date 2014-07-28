using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphFillStyleStruct
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

            switch (FillStyleType)
            {
                case FillStyle.Solid:
                {
                    ReadSolid(reader);
                    break;
                }
                case FillStyle.LinearGradient:
                case FillStyle.RadialGradient:
                {
                    ReadGradient(reader);
                    break;
                }
                case FillStyle.ClippedBitmap:
                case FillStyle.RepeatingBitmap:
                case FillStyle.NonSmoothedClippedBitmap:
                case FillStyle.NonSmoothedRepeatingBitmap:
                {
                    ReadBitmap(reader);
                    break;
                }
                default:
                {
                    throw new InvalidDataException("Bad fill style");
                }
            }
        }

        private void ReadBitmap(BitReader reader)
        {
            BitmapId = reader.ReadUI16();
            StartBitmapMatrix = MatrixStruct.CreateFromStream(reader);
            EndBitmapMatrix = MatrixStruct.CreateFromStream(reader);
        }

        private void ReadGradient(BitReader reader)
        {
            StartGradientMatrix = MatrixStruct.CreateFromStream(reader);
            EndGradientMatrix = MatrixStruct.CreateFromStream(reader);
            Gradient = MorphGradientStruct.CreateFromStream(reader);
        }

        private void ReadSolid(BitReader reader)
        {
            StartColor = RgbaStruct.CreateFromStream(reader);
            EndColor = RgbaStruct.CreateFromStream(reader);
        }

        internal static MorphFillStyleStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphFillStyleStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte) FillStyleType);

            switch (FillStyleType)
            {
                case FillStyle.Solid:
                {
                    WriteSolid(writer);
                    break;
                }
                case FillStyle.LinearGradient:
                case FillStyle.RadialGradient:
                {
                    WriteGradient(writer);
                    break;
                }
                case FillStyle.ClippedBitmap:
                case FillStyle.RepeatingBitmap:
                case FillStyle.NonSmoothedClippedBitmap:
                case FillStyle.NonSmoothedRepeatingBitmap:
                {
                    WriteBitmap(writer);
                    break;
                }
                default:
                {
                    throw new InvalidDataException("Bad fill style");
                }
            }
        }

        private void WriteBitmap(BitWriter writer)
        {
            writer.WriteUI16(BitmapId);
            StartBitmapMatrix.ToStream(writer);
            EndBitmapMatrix.ToStream(writer);
        }

        private void WriteGradient(BitWriter writer)
        {
            StartGradientMatrix.ToStream(writer);
            EndGradientMatrix.ToStream(writer);
            Gradient.ToStream(writer);
        }

        private void WriteSolid(BitWriter writer)
        {
            StartColor.ToStream(writer);
            EndColor.ToStream(writer);
        }
    }
}
