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
    public class FillStyleArray : List<FillStyleStruct>
    {
        public FillStyleArray()
        {
        }

        public FillStyleArray(int capacity)
            : base(capacity)
        {
        }

        private void FromStream(BitReader reader, TagType type, int len)
        {
            for (int i = 0; i < len; i++)
            {
                Add(FillStyleStruct.CreateFromStream(reader, type));
            }
        }

        internal static FillStyleArray CreateFromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();

            var result = new FillStyleArray(len);

            result.FromStream(reader, type, len);

            return result;
        }

        internal byte ToStream(BitWriter writer, TagType type)
        {
            writer.WriteExtendableCount(Count);
            foreach (var fillStyle in this)
            {
                fillStyle.ToStream(writer, type);
            }
            return (byte) BitWriter.GetBitsForValue((uint)Count);
        }
    }
}
