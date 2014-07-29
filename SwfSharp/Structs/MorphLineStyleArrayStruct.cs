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
    public class MorphLineStyleArrayStruct : List<MorphLineStyleStruct>
    {
        public MorphLineStyleArrayStruct()
        {
        }

        public MorphLineStyleArrayStruct(int capacity)
            : base(capacity)
        {
        }

        private void FromStream(BitReader reader, TagType type, int len)
        {
            for (int i = 0; i < len; i++)
            {
                var style = type == TagType.DefineMorphShape2
                    ? MorphLineStyle2Struct.CreateFromStream(reader, type)
                    : MorphLineStyleStruct.CreateFromStream(reader);
                Add(style);
            }
        }

        internal static MorphLineStyleArrayStruct CreateFromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();
            var result = new MorphLineStyleArrayStruct(len);

            result.FromStream(reader, type, len);

            return result;
        }

        internal byte ToStream(BitWriter writer)
        {
            writer.WriteExtendableCount(Count);

            foreach (var style in this)
            {
                style.ToStream(writer);
            }
            return (byte)BitWriter.GetBitsForValue((uint)Count);
        }
    }
}
