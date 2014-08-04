using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ShapeWithStyleStruct : ShapeStruct
    {
        [XmlArrayItem("FillStyle")]
        public FillStyleArray FillStyles { get; set; }

        [XmlIgnore]
        public bool FillStylesSpecified
        {
            get { return FillStyles != null && FillStyles.Count > 0; }
        }

        [XmlArrayItem("LineStyle", typeof(LineStyleStruct))]
        [XmlArrayItem("LineStyle2", typeof(LineStyle2Struct))]
        public LineStyleArray LineStyles { get; set; }

        [XmlIgnore]
        public bool LineStylesSpecified
        {
            get { return LineStyles != null && LineStyles.Count > 0; }
        }

        public ShapeWithStyleStruct()
        {
            FillStyles = new FillStyleArray();
            LineStyles = new LineStyleArray();
        }

        internal override void FromStream(BitReader reader, TagType type)
        {
            FillStyles = FillStyleArray.CreateFromStream(reader, type);
            LineStyles = LineStyleArray.CreateFromStream(reader, type);
            base.FromStream(reader, type);
        }

        internal new static ShapeWithStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new ShapeWithStyleStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal override void ToStream(BitWriter writer, TagType tagType)
        {
            var fillbits = FillStyles.ToStream(writer, tagType);
            var lineBits = LineStyles.ToStream(writer, tagType);

            ToStream(writer, ref fillbits, ref lineBits, tagType);
        }
    }
}
