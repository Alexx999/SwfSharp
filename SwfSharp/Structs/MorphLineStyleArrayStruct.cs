using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MorphLineStyleArrayStruct
    {
        public IList<MorphLineStyleStruct> LineStyles { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            int len = reader.ReadExtendableCount();
            LineStyles = new List<MorphLineStyleStruct>(len);

            for (int i = 0; i < len; i++)
            {
                var style = type == TagType.DefineMorphShape2
                    ? MorphLineStyle2Struct.CreateFromStream(reader)
                    : MorphLineStyleStruct.CreateFromStream(reader);
                LineStyles.Add(style);
            }
        }

        internal static MorphLineStyleArrayStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new MorphLineStyleArrayStruct();

            result.FromStream(reader, type);

            return result;
        }

        internal byte ToStream(BitWriter writer)
        {
            writer.WriteExtendableCount(LineStyles.Count);

            foreach (var style in LineStyles)
            {
                style.ToStream(writer);
            }
            return (byte)BitWriter.GetBitsForValue((uint)LineStyles.Count);
        }
    }
}
