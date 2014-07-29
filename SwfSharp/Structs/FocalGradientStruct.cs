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
    public class FocalGradientStruct : GradientStruct
    {
        [XmlAttribute]
        public float FocalPoint { get; set; }

        internal override void FromStream(BitReader reader, TagType type)
        {
            base.FromStream(reader, type);
            FocalPoint = reader.ReadFixed8();
        }

        internal new static FocalGradientStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new FocalGradientStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal override void ToStream(BitWriter writer, TagType type)
        {
            base.ToStream(writer, type);
            writer.WriteFixed8(FocalPoint);
        }
    }
}
