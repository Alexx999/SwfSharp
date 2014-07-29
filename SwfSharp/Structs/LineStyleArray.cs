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
    public class LineStyleArray : List<LineStyleStruct>
    {
        public LineStyleArray()
        {
        }

        public LineStyleArray(int capacity)
            : base(capacity)
        {
        }

        private void FromStream(BitReader reader, TagType type, int len)
        {

            for (int i = 0; i < len; i++)
            {
                var style = type == TagType.DefineShape4 
                    ? LineStyle2Struct.CreateFromStream(reader, type) 
                    : LineStyleStruct.CreateFromStream(reader, type);
                Add(style);
            }
        }

        internal static LineStyleArray CreateFromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();

            var result = new LineStyleArray(len);

            result.FromStream(reader, type, len);

            return result;
        }

        internal byte ToStream(BitWriter writer, TagType type)
        {
            writer.WriteExtendableCount(Count);
            foreach (var lineStyle in this)
            {
                lineStyle.ToStream(writer, type);
            }
            return (byte) BitWriter.GetBitsForValue((uint)Count);
        }
    }
}
