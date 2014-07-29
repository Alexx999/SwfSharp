using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphLineStyle2Struct : MorphLineStyleStruct
    {
        private float? _miterLimitFactor;

        [XmlAttribute]
        public CapStyle StartCapStyle { get; set; }
        [XmlAttribute]
        public JoinStyle JoinStyle { get; set; }
        [XmlAttribute]
        public bool NoHScaleFlag { get; set; }
        [XmlAttribute]
        public bool NoVScaleFlag { get; set; }
        [XmlAttribute]
        public bool PixelHintingFlag { get; set; }
        [XmlAttribute]
        public bool NoClose { get; set; }
        [XmlAttribute]
        public CapStyle EndCapStyle { get; set; }

        [XmlAttribute]
        public float MiterLimitFactor
        {
            get { return _miterLimitFactor.GetValueOrDefault(); }
            set { _miterLimitFactor = value; }
        }

        [XmlIgnore]
        public bool MiterLimitFactorSpecified
        {
            get { return _miterLimitFactor.HasValue; }
        }

        public MorphFillStyleStruct FillType { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            StartWidth = reader.ReadUI16();
            EndWidth = reader.ReadUI16();

            StartCapStyle = (CapStyle) reader.ReadBits(2);
            JoinStyle = (JoinStyle)reader.ReadBits(2);
            var hasFillFlag = reader.ReadBoolBit();
            NoHScaleFlag = reader.ReadBoolBit();
            NoVScaleFlag = reader.ReadBoolBit();
            PixelHintingFlag = reader.ReadBoolBit();
            reader.ReadBits(5);
            NoClose = reader.ReadBoolBit();
            EndCapStyle = (CapStyle)reader.ReadBits(2);
            if (JoinStyle == JoinStyle.MiterJoin)
            {
                _miterLimitFactor = reader.ReadFixed8();
            }

            if (!hasFillFlag)
            {
                StartColor = RgbaStruct.CreateFromStream(reader);
                EndColor = RgbaStruct.CreateFromStream(reader);
            }
            else
            {
                FillType = MorphFillStyleStruct.CreateFromStream(reader, type);
            }
        }

        internal static MorphLineStyle2Struct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new MorphLineStyle2Struct();

            result.FromStream(reader, type);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            var hasFillFlag = FillType != null;

            writer.WriteUI16(StartWidth);
            writer.WriteUI16(EndWidth);

            writer.WriteBits(2, (uint) StartCapStyle);
            writer.WriteBits(2, (uint) JoinStyle);
            writer.WriteBoolBit(hasFillFlag);
            writer.WriteBoolBit(NoHScaleFlag);
            writer.WriteBoolBit(NoVScaleFlag);
            writer.WriteBoolBit(PixelHintingFlag);
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(NoClose);
            writer.WriteBits(2, (uint) EndCapStyle);
            if (JoinStyle == JoinStyle.MiterJoin)
            {
                writer.WriteFixed8(_miterLimitFactor.GetValueOrDefault());
            }

            if (!hasFillFlag)
            {
                StartColor.ToStream(writer);
                EndColor.ToStream(writer);
            }
            else
            {
                FillType.ToStream(writer);
            }
        }
    }
}
