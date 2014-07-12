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
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public bool HasRotate { get; set; }
        public float RotateSkew0 { get; set; }
        public float RotateSkew1 { get; set; }
        public float TranslateX { get; set; }
        public float TranslateY { get; set; }

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
            reader.Align();
        }

        internal static MatrixStruct CreateFromStream(BitReader reader)
        {
            var result = new MatrixStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
