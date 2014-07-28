using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MatrixStruct
    {
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double RotateSkew0 { get; set; }
        public double RotateSkew1 { get; set; }
        public int TranslateX { get; set; }
        public int TranslateY { get; set; }

        public MatrixStruct()
        {
            ScaleX = 1;
            ScaleY = 1;
        }

        private void FromStream(BitReader reader)
        {
            reader.Align();
            var hasScale = reader.ReadBoolBit();
            if (hasScale)
            {
                var scaleBits = reader.ReadBits(5);
                ScaleX = reader.ReadFBits(scaleBits);
                ScaleY = reader.ReadFBits(scaleBits);
            }
            var hasRotate = reader.ReadBoolBit();
            if (hasRotate)
            {
                var rotateBits = reader.ReadBits(5);
                RotateSkew0 = reader.ReadFBits(rotateBits);
                RotateSkew1 = reader.ReadFBits(rotateBits);
            }
            var translateBits = reader.ReadBits(5);
            TranslateX = reader.ReadBitsSigned(translateBits);
            TranslateY = reader.ReadBitsSigned(translateBits);
        }

        internal static MatrixStruct CreateFromStream(BitReader reader)
        {
            var result = new MatrixStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.Align();
            var hasScale = (ScaleX != 1) || (ScaleY != 1);
            writer.WriteBoolBit(hasScale);
            if (hasScale)
            {
                writer.WriteBitSizeAndData(5, new[] { ScaleX, ScaleY });
            }
            var hasRotate = (RotateSkew0 != 0) || (RotateSkew1 != 0);
            writer.WriteBoolBit(hasRotate);
            if (hasRotate)
            {
                writer.WriteBitSizeAndData(5, new[] { RotateSkew0, RotateSkew1 });
            }
            writer.WriteBitSizeAndData(5, new[] { TranslateX, TranslateY });
        }
    }
}
