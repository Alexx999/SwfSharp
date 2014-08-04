using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphFocalGradientStruct : MorphGradientStruct
    {
        [XmlAttribute]
        public float StartFocalPoint { get; set; }
        [XmlAttribute]
        public float EndFocalPoint { get; set; }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            StartFocalPoint = reader.ReadFixed8();
            EndFocalPoint = reader.ReadFixed8();
        }

        internal new static MorphFocalGradientStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphFocalGradientStruct();

            result.FromStream(reader);

            return result;
        }

        internal override void ToStream(BitWriter writer)
        {
            base.ToStream(writer);
            writer.WriteFixed8(StartFocalPoint);
            writer.WriteFixed8(EndFocalPoint);
        }
    }
}
