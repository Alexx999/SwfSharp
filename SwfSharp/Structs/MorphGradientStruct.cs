﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphGradientStruct
    {
        [XmlElement("MorphGradRecord")]
        public List<MorphGradRecordStruct> GradientRecords { get; set; }

        internal virtual void FromStream(BitReader reader)
        {
            var numGradients = reader.ReadUI8();
            GradientRecords = new List<MorphGradRecordStruct>(numGradients);
            for (int i = 0; i < numGradients; i++)
            {
                GradientRecords.Add(MorphGradRecordStruct.CreateFromStream(reader));
            }
        }

        internal static MorphGradientStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphGradientStruct();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte) GradientRecords.Count);
            foreach (var record in GradientRecords)
            {
                record.ToStream(writer);
            }
        }
    }
}