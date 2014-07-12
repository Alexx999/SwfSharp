﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class LineStyleArray
    {
        public IList<LineStyleStruct> LineStyles { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadUI8();
            if (len == byte.MaxValue && type > TagType.DefineShape)
            {
                len = reader.ReadUI16();
            }
            LineStyles = new List<LineStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                var style = type == TagType.DefineShape4 
                    ? LineStyle2Struct.CreateFromStream(reader, type) 
                    : LineStyleStruct.CreateFromStream(reader, type);
                LineStyles.Add(style);
            }
        }

        internal static LineStyleArray CreateFromStream(BitReader reader, TagType type)
        {
            var result = new LineStyleArray();

            result.FromStream(reader, type);

            return result;
        }
    }
}
