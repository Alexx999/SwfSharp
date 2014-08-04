using System;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class FillStyleStruct
    {
        private ushort? _bitmapId;

        [XmlAttribute]
        public FillStyle FillStyleType { get; set; }
        [XmlElement]
        public RgbaStruct Color { get; set; }
        [XmlElement]
        public MatrixStruct GradientMatrix { get; set; }
        [XmlElement]
        public GradientStruct Gradient { get; set; }
        [XmlElement]
        public FocalGradientStruct FocalGradient { get; set; }

        [XmlAttribute]
        public ushort BitmapId
        {
            get { return _bitmapId.GetValueOrDefault(); }
            set { _bitmapId = value; }
        }

        [XmlIgnore]
        public bool BitmapIdSpecified
        {
            get { return _bitmapId.HasValue; }
        }

        [XmlElement]
        public MatrixStruct BitmapMatrix { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            FillStyleType = (FillStyle)reader.ReadUI8();

            switch (FillStyleType)
            {
                case FillStyle.Solid:
                {
                    ReadSolid(reader, type);
                    break;
                }
                case FillStyle.LinearGradient:
                case FillStyle.RadialGradient:
                case FillStyle.FocalRadialGradient:
                {
                    ReadGradient(reader, type);
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
            _bitmapId = reader.ReadUI16();
            BitmapMatrix = MatrixStruct.CreateFromStream(reader);
        }

        private void ReadGradient(BitReader reader, TagType type)
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

        private void ReadSolid(BitReader reader, TagType type)
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

        internal static FillStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new FillStyleStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal void ToStream(BitWriter writer, TagType type)
        {
            writer.WriteUI8((byte) FillStyleType);

            switch (FillStyleType)
            {
                case FillStyle.Solid:
                {
                    WriteSolid(writer, type);
                    break;
                }
                case FillStyle.LinearGradient:
                case FillStyle.RadialGradient:
                case FillStyle.FocalRadialGradient:
                {
                    WriteGradient(writer, type);
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
            writer.WriteUI16(_bitmapId.GetValueOrDefault());
            BitmapMatrix.ToStream(writer);
        }

        private void WriteGradient(BitWriter writer, TagType type)
        {
            GradientMatrix.ToStream(writer);
            if (FillStyleType == FillStyle.FocalRadialGradient)
            {
                FocalGradient.ToStream(writer, type);
            }
            else
            {
                Gradient.ToStream(writer, type);
            }
        }

        private void WriteSolid(BitWriter writer, TagType type)
        {
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
