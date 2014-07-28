using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class LineStyle2Struct : LineStyleStruct
    {
        public CapStyle StartCapStyle { get; set; }
        public JoinStyle JoinStyle { get; set; }
        public bool HasFillFlag { get; set; }
        public bool NoHScaleFlag { get; set; }
        public bool NoVScaleFlag { get; set; }
        public bool PixelHintingFlag { get; set; }
        public bool NoClose { get; set; }
        public CapStyle EndCapStyle { get; set; }
        public float MiterLimitFactor { get; set; }
        public FillStyleStruct FillType { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            Width = reader.ReadUI16();
            StartCapStyle = (CapStyle) reader.ReadBits(2);
            JoinStyle = (JoinStyle) reader.ReadBits(2);
            HasFillFlag = reader.ReadBoolBit();
            NoHScaleFlag = reader.ReadBoolBit();
            NoVScaleFlag = reader.ReadBoolBit();
            PixelHintingFlag = reader.ReadBoolBit();
            reader.ReadBits(5);
            NoClose = reader.ReadBoolBit();
            EndCapStyle = (CapStyle) reader.ReadBits(2);
            if (JoinStyle == JoinStyle.MiterJoin)
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

        internal override void ToStream(BitWriter writer, TagType type)
        {
            writer.WriteUI16(Width);
            writer.WriteBits(2, (uint) StartCapStyle);
            writer.WriteBits(2, (uint) JoinStyle);
            writer.WriteBoolBit(HasFillFlag);
            writer.WriteBoolBit(NoHScaleFlag);
            writer.WriteBoolBit(NoVScaleFlag);
            writer.WriteBoolBit(PixelHintingFlag);
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(NoClose);
            writer.WriteBits(2, (uint) EndCapStyle);
            if (JoinStyle == JoinStyle.MiterJoin)
            {
                writer.WriteFixed8(MiterLimitFactor);
            }
            if (!HasFillFlag)
            {
                Color.ToStream(writer);
            }
            else
            {
                FillType.ToStream(writer, type);
            }
        }
    }
}
