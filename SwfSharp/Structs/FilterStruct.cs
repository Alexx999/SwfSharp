using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Filters;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class FilterStruct
    {
        [XmlAttribute]
        public FilterType FilterID { get; set; }
        public DropShadowFilter DropShadowFilter { get; set; }
        public BlurFilter BlurFilter { get; set; }
        public GlowFilter GlowFilter { get; set; }
        public BevelFilter BevelFilter { get; set; }
        public GradientGlowFilter GradientGlowFilter { get; set; }
        public ConvolutionFilter ConvolutionFilter { get; set; }
        public ColorMatrixFilter ColorMatrixFilter { get; set; }
        public GradientBevelFilter GradientBevelFilter { get; set; }

        private void FromStream(BitReader reader)
        {
            FilterID = (FilterType) reader.ReadUI8();
            switch (FilterID)
            {
                case FilterType.DropShadowFilter:
                {
                    DropShadowFilter = DropShadowFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.BlurFilter:
                {
                    BlurFilter = BlurFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.GlowFilter:
                {
                    GlowFilter = GlowFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.BevelFilter:
                {
                    BevelFilter = BevelFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.GradientGlowFilter:
                {
                    GradientGlowFilter = GradientGlowFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.ConvolutionFilter:
                {
                    ConvolutionFilter = ConvolutionFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.ColorMatrixFilter:
                {
                    ColorMatrixFilter = ColorMatrixFilter.CreateFromStream(reader);
                    break;
                }
                case FilterType.GradientBevelFilter:
                {
                    GradientBevelFilter = GradientBevelFilter.CreateFromStream(reader);
                    break;
                }
                default:
                {
                    throw new InvalidDataException("Bad filter type");
                }
            }
        }

        internal static FilterStruct CreateFromStream(BitReader reader)
        {
            var result = new FilterStruct();

            result.FromStream(reader);

            return result;
        }

        public enum FilterType : byte
        {
            DropShadowFilter = 0,
            BlurFilter = 1,
            GlowFilter = 2,
            BevelFilter = 3,
            GradientGlowFilter = 4,
            ConvolutionFilter = 5,
            ColorMatrixFilter = 6,
            GradientBevelFilter = 7
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte) FilterID);
            switch (FilterID)
            {
                case FilterType.DropShadowFilter:
                {
                    DropShadowFilter.ToStream(writer);
                    break;
                }
                case FilterType.BlurFilter:
                {
                    BlurFilter.ToStream(writer);
                    break;
                }
                case FilterType.GlowFilter:
                {
                    GlowFilter.ToStream(writer);
                    break;
                }
                case FilterType.BevelFilter:
                {
                    BevelFilter.ToStream(writer);
                    break;
                }
                case FilterType.GradientGlowFilter:
                {
                    GradientGlowFilter.ToStream(writer);
                    break;
                }
                case FilterType.ConvolutionFilter:
                {
                    ConvolutionFilter.ToStream(writer);
                    break;
                }
                case FilterType.ColorMatrixFilter:
                {
                    ColorMatrixFilter.ToStream(writer);
                    break;
                }
                case FilterType.GradientBevelFilter:
                {
                    GradientBevelFilter.ToStream(writer);
                    break;
                }
            }
        }
    }
}
