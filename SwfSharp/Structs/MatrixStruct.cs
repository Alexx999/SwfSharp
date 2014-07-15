using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MatrixStruct
    {
        public bool HasScale { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public bool HasRotate { get; set; }
        public double RotateSkew0 { get; set; }
        public double RotateSkew1 { get; set; }
        public int TranslateX { get; set; }
        public int TranslateY { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();
            HasScale = reader.ReadBoolBit();
            if (HasScale)
            {
                var scaleBits = reader.ReadBits(5);
                ScaleX = reader.ReadFBits(scaleBits);
                ScaleY = reader.ReadFBits(scaleBits);
            }
            HasRotate = reader.ReadBoolBit();
            if (HasRotate)
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
            writer.WriteBoolBit(HasScale);
            if (HasScale)
            {
                writer.WriteBitSizeAndData(5, new[] { ScaleX, ScaleY });
            }
            writer.WriteBoolBit(HasRotate);
            if (HasRotate)
            {
                writer.WriteBitSizeAndData(5, new[] { RotateSkew0, RotateSkew1 });
            }
            writer.WriteBitSizeAndData(5, new[] { TranslateX, TranslateY });
        }
    }
}
