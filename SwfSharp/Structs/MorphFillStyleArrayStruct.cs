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
    public class MorphFillStyleArrayStruct
    {
        [XmlElement("MorphFillStyle")]
        public List<MorphFillStyleStruct> FillStyles { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();
            FillStyles = new List<MorphFillStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                FillStyles.Add(MorphFillStyleStruct.CreateFromStream(reader, type));
            }
        }

        internal static MorphFillStyleArrayStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new MorphFillStyleArrayStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal byte ToStream(BitWriter writer)
        {
            writer.WriteExtendableCount(FillStyles.Count);
            foreach (var fillStyle in FillStyles)
            {
                fillStyle.ToStream(writer);
            }
            return (byte)BitWriter.GetBitsForValue((uint)FillStyles.Count);
        }
    }
}
