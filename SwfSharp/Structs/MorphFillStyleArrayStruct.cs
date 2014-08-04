using System;
using System.Collections.Generic;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphFillStyleArrayStruct : List<MorphFillStyleStruct>
    {
        public MorphFillStyleArrayStruct()
        {
        }

        public MorphFillStyleArrayStruct(int capacity)
            : base(capacity)
        {
        }

        private void FromStream(BitReader reader, TagType type, int len)
        {
            for (int i = 0; i < len; i++)
            {
                Add(MorphFillStyleStruct.CreateFromStream(reader, type));
            }
        }

        internal static MorphFillStyleArrayStruct CreateFromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();
            var result = new MorphFillStyleArrayStruct(len);

            result.FromStream(reader, type, len);

            return result;
        }

        internal byte ToStream(BitWriter writer)
        {
            writer.WriteExtendableCount(Count);
            foreach (var fillStyle in this)
            {
                fillStyle.ToStream(writer);
            }
            return (byte)BitWriter.GetBitsForValue((uint)Count);
        }
    }
}
