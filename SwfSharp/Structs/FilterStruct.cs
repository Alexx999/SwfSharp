﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Filters;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class FilterStruct
    {
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
    }
}