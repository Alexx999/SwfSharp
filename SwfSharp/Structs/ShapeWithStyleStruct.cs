using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ShapeWithStyleStruct : ShapeStruct
    {
        public FillStyleArray FillStyles { get; set; }
        public LineStyleArray LineStyles { get; set; }

        internal override void FromStream(BitReader reader, TagType type)
        {
            FillStyles = FillStyleArray.CreateFromStream(reader, type);
            LineStyles = LineStyleArray.CreateFromStream(reader, type);
            base.FromStream(reader, type);
        }

        internal new static ShapeWithStyleStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new ShapeWithStyleStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}
