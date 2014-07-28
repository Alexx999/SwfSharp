using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    public class ConvolutionFilter
    {
        public byte MatrixX { get; set; }
        public byte MatrixY { get; set; }
        public float Divisor { get; set; }
        public float Bias { get; set; }
        public List<float> Matrix { get; set; }
        public RgbaStruct DefaultColor { get; set; }
        public bool Clamp { get; set; }
        public bool PreserveAlpha { get; set; }

        private void FromStream(BitReader reader)
        {
            MatrixX = reader.ReadUI8();
            MatrixY = reader.ReadUI8();
            var matrixSize = MatrixX*MatrixY;
            Divisor = reader.ReadFloat();
            Bias = reader.ReadFloat();
            Matrix = new List<float>(matrixSize);
            for (int i = 0; i < matrixSize; i++)
            {
                Matrix.Add(reader.ReadFloat());
            }
            DefaultColor = RgbaStruct.CreateFromStream(reader);
            reader.ReadBits(6);
            Clamp = reader.ReadBoolBit();
            PreserveAlpha = reader.ReadBoolBit();
        }

        internal static ConvolutionFilter CreateFromStream(BitReader reader)
        {
            var result = new ConvolutionFilter();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8(MatrixX);
            writer.WriteUI8(MatrixY);
            writer.WriteFloat(Divisor);
            writer.WriteFloat(Bias);
            var matrixSize = MatrixX * MatrixY;
            for (int i = 0; i < matrixSize; i++)
            {
                writer.WriteFloat(Matrix[i]);
            }
            DefaultColor.ToStream(writer);
            writer.WriteBits(6, 0);
            writer.WriteBoolBit(Clamp);
            writer.WriteBoolBit(PreserveAlpha);
        }
    }
}
