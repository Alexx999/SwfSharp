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
            int len = reader.ReadExtendableCount();
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

        internal void ToStream(BitWriter writer)
        {
            writer.WriteExtendableCount(FillStyles.Count);
            foreach (var fillStyle in FillStyles)
            {
                fillStyle.ToStream(writer);
            }
        }
    }
}
