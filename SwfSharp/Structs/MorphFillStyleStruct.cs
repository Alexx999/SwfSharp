using System;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphFillStyleStruct
    {
        private ushort? _bitmapId;

        [XmlAttribute]
        public FillStyle FillStyleType { get; set; }
        [XmlElement]
        public RgbaStruct StartColor { get; set; }
        [XmlElement]
        public RgbaStruct EndColor { get; set; }
        [XmlElement]
        public MatrixStruct StartGradientMatrix { get; set; }
        [XmlElement]
        public MatrixStruct EndGradientMatrix { get; set; }
        [XmlElement]
        public MorphGradientStruct Gradient { get; set; }

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
        public MatrixStruct StartBitmapMatrix { get; set; }
        [XmlElement]
        public MatrixStruct EndBitmapMatrix { get; set; }

        private void FromStream(BitReader reader, TagType type)
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
            StartBitmapMatrix = MatrixStruct.CreateFromStream(reader);
            EndBitmapMatrix = MatrixStruct.CreateFromStream(reader);
        }

        private void ReadGradient(BitReader reader, TagType type)
        {
            StartGradientMatrix = MatrixStruct.CreateFromStream(reader);
            EndGradientMatrix = MatrixStruct.CreateFromStream(reader);

            if (FillStyleType == FillStyle.FocalRadialGradient && type == TagType.DefineMorphShape2)
            {
                Gradient = MorphFocalGradientStruct.CreateFromStream(reader);
            }
            else
            {

                Gradient = MorphGradientStruct.CreateFromStream(reader);
            }
        }

        private void ReadSolid(BitReader reader)
        {
            StartColor = RgbaStruct.CreateFromStream(reader);
            EndColor = RgbaStruct.CreateFromStream(reader);
        }

        internal static MorphFillStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new MorphFillStyleStruct();

            result.FromStream(reader, type);

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
                case FillStyle.FocalRadialGradient:
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
            writer.WriteUI16(_bitmapId.GetValueOrDefault());
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
