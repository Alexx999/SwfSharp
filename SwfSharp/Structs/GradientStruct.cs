using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class GradientStruct
    {
        [XmlAttribute]
        public SpreadMode SpreadMode { get; set; }
        [XmlAttribute]
        public InterpolationMode InterpolationMode { get; set; }
        public List<GradRecordStruct> GradientRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            reader.Align();
            SpreadMode = (SpreadMode)reader.ReadBits(2);
            InterpolationMode = (InterpolationMode)reader.ReadBits(2);
            var numGradients = reader.ReadBits(4);
            GradientRecords = new List<GradRecordStruct>((int) numGradients);
            for (int i = 0; i < numGradients; i++)
            {
                GradientRecords.Add(GradRecordStruct.CreateFromStream(reader, type));
            }
        }

        internal static GradientStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new GradientStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal virtual void ToStream(BitWriter writer, TagType type)
        {
            writer.Align();
            writer.WriteBits(2, (uint) SpreadMode);
            writer.WriteBits(2, (uint) InterpolationMode);
            writer.WriteBits(4, (uint) GradientRecords.Count);

            foreach (var record in GradientRecords)
            {
                record.ToStream(writer, type);
            }
        }
    }
}