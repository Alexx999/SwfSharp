﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class FilterListStruct
    {
        [XmlElement("Filter")]
        public List<FilterStruct> Filter { get; set; } 

        private void FromStream(BitReader reader)
        {
            var numberOfFilters = reader.ReadUI8();
            Filter = new List<FilterStruct>(numberOfFilters);
            for (int i = 0; i < numberOfFilters; i++)
            {
                Filter.Add(FilterStruct.CreateFromStream(reader));
            }
        }

        internal static FilterListStruct CreateFromStream(BitReader reader)
        {
            var result = new FilterListStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte) Filter.Count);
            foreach (var filterStruct in Filter)
            {
                filterStruct.ToStream(writer);
            }
        }
    }
}
