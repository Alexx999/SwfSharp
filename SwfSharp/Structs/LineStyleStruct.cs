using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class LineStyleStruct
    {
        public ushort Width { get; set; }
        public RgbaStruct Color { get; set; }

        private void FromStream(BitReader reader, TagType type)
        {
            Width = reader.ReadUI16();
            if (type < TagType.DefineShape3)
            {
                RgbaStruct.CreateFromRgbStream(reader);
            }
            else
            {
                RgbaStruct.CreateFromStream(reader);
            }
        }

        internal static LineStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new LineStyleStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}
