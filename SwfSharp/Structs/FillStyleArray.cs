using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class FillStyleArray
    {
        public IList<FillStyleStruct> FillStyles { get; set; } 

        private void FromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadUI8();
            if (len == byte.MaxValue && type > TagType.DefineShape)
            {
                len = reader.ReadUI16();
            }
            FillStyles = new List<FillStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                FillStyles.Add(FillStyleStruct.CreateFromStream(reader, type));
            }
        }

        internal static FillStyleArray CreateFromStream(BitReader reader, TagType type)
        {
            var result = new FillStyleArray();

            result.FromStream(reader, type);

            return result;
        }
    }
}
