using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MatrixStruct
    {
        private double? _scaleX;
        private double? _scaleY;
        private double? _rotateSkew0;
        private double? _rotateSkew1;

        [XmlAttribute]
        public double ScaleX
        {
            get { return _scaleX.GetValueOrDefault(); }
            set { _scaleX = value; }
        }

        [XmlIgnore]
        public bool ScaleXSpecified
        {
            get { return _scaleX.HasValue; }
        }

        [XmlAttribute]
        public double ScaleY
        {
            get { return _scaleY.GetValueOrDefault(); }
            set { _scaleY = value; }
        }

        [XmlIgnore]
        public bool ScaleYSpecified
        {
            get { return _scaleY.HasValue; }
        }

        [XmlAttribute]
        public double RotateSkew0
        {
            get { return _rotateSkew0.GetValueOrDefault(); }
            set { _rotateSkew0 = value; }
        }

        [XmlIgnore]
        public bool RotateSkew0Specified
        {
            get { return _rotateSkew0.HasValue; }
        }

        [XmlAttribute]
        public double RotateSkew1
        {
            get { return _rotateSkew1.GetValueOrDefault(); }
            set { _rotateSkew1 = value; }
        }

        [XmlIgnore]
        public bool RotateSkew1Specified
        {
            get { return _rotateSkew1.HasValue; }
        }

        [XmlAttribute]
        public int TranslateX { get; set; }
        [XmlAttribute]
        public int TranslateY { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();
            var hasScale = reader.ReadBoolBit();
            if (hasScale)
            {
                var scaleBits = reader.ReadBits(5);
                _scaleX = reader.ReadFBits(scaleBits);
                _scaleY = reader.ReadFBits(scaleBits);
            }
            var hasRotate = reader.ReadBoolBit();
            if (hasRotate)
            {
                var rotateBits = reader.ReadBits(5);
                _rotateSkew0 = reader.ReadFBits(rotateBits);
                _rotateSkew1 = reader.ReadFBits(rotateBits);
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
            var hasScale = _scaleX.HasValue && _scaleY.HasValue;
            writer.WriteBoolBit(hasScale);
            if (hasScale)
            {
                writer.WriteBitSizeAndData(5, new[] { _scaleX.Value, _scaleY.Value });
            }
            var hasRotate = _rotateSkew0.HasValue && _rotateSkew1.HasValue;
            writer.WriteBoolBit(hasRotate);
            if (hasRotate)
            {
                writer.WriteBitSizeAndData(5, new[] { _rotateSkew0.Value, _rotateSkew1.Value });
            }
            writer.WriteBitSizeAndData(5, new[] { TranslateX, TranslateY });
        }
    }
}
