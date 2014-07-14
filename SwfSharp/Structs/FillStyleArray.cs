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
            int len = reader.ReadExtendableCount();
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

        internal byte ToStream(BitWriter writer, TagType type)
        {
            writer.WriteExtendableCount(FillStyles.Count());
            foreach (var fillStyle in FillStyles)
            {
                fillStyle.WriteTo(writer, type);
            }
            return (byte) BitWriter.GetBitsForValue((uint)FillStyles.Count());
        }
    }
}
