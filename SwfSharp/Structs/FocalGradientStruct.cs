using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class FocalGradientStruct : GradientStruct
    {
        public float FocalPoint { get; set; }

        internal override void FromStream(BitReader reader, TagType type)
        {
            base.FromStream(reader, type);
            FocalPoint = reader.ReadFixed8();
        }

        internal new static FocalGradientStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new FocalGradientStruct();

            result.FromStream(reader, type);

            return result;
        }
    }
}
