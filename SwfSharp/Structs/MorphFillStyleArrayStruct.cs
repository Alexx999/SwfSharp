using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MorphFillStyleArrayStruct
    {
        public IList<MorphFillStyleStruct> FillStyles { get; set; }

        private void FromStream(BitReader reader)
        {
            int len = reader.ReadUI8();
            if (len == byte.MaxValue)
            {
                len = reader.ReadUI16();
            }
            FillStyles = new List<MorphFillStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                FillStyles.Add(MorphFillStyleStruct.CreateFromStream(reader));
            }
        }

        internal static MorphFillStyleArrayStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphFillStyleArrayStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
