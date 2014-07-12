using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class LineStyle2Struct : LineStyleStruct
    {
        public CapStyleType StartCapStyle { get; set; }
        public JoinStyleType JoinStyle { get; set; }
        public bool HasFillFlag { get; set; }
        public bool NoHScaleFlag { get; set; }
        public bool NoVScaleFlag { get; set; }
        public bool PixelHintingFlag { get; set; }
        public bool NoClose { get; set; }
        public CapStyleType EndCapStyle { get; set; }
        public float MiterLimitFactor { get; set; }
        public FillStyleStruct FillType { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            Width = reader.ReadUI16();
            StartCapStyle = (CapStyleType) reader.ReadBits(2);
            JoinStyle = (JoinStyleType) reader.ReadBits(2);
            HasFillFlag = reader.ReadBoolBit();
            NoHScaleFlag = reader.ReadBoolBit();
            NoVScaleFlag = reader.ReadBoolBit();
            PixelHintingFlag = reader.ReadBoolBit();
            reader.ReadBits(5);
            NoClose = reader.ReadBoolBit();
            EndCapStyle = (CapStyleType) reader.ReadBits(2);
            if (JoinStyle == JoinStyleType.MiterJoin)
            {
                MiterLimitFactor = reader.ReadFixed8();
            }
            if (!HasFillFlag)
            {
                Color = RgbaStruct.CreateFromStream(reader);
            }
            else
            {
                FillType = FillStyleStruct.CreateFromStream(reader, type);
            }
        }

        internal new static LineStyle2Struct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new LineStyle2Struct();

            result.FromStream(reader, type);

            return result;
        }

        public enum CapStyleType
        {
            RoundCap = 0,
            NoCap = 1,
            SquareCap = 2
        }

        public enum JoinStyleType
        {
            RoundJoin = 0,
            BevelJoin = 1,
            MiterJoin = 2
        }
    }
}
