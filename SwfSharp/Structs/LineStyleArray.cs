using System;
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
            reader.Align();
            int len = reader.ReadUI8();
            if (len == byte.MaxValue)
            {
                len = reader.ReadUI16();
            }
            LineStyles = new List<LineStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                LineStyles.Add(LineStyleStruct.CreateFromStream(reader, type));
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
