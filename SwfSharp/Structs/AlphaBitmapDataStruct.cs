﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class AlphaBitmapDataStruct
    {
        public IList<ArgbStruct> BitmapPixelData { get; set; }

        private void FromStream(BitReader reader, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    BitmapPixelData.Add(ArgbStruct.CreateFromStream(reader));
                }
            }
        }

        internal static AlphaBitmapDataStruct CreateFromStream(BitReader reader, int width, int height)
        {
            var result = new AlphaBitmapDataStruct();

            result.FromStream(reader, width, height);

            return result;
        }
    }
}