using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class BitmapDataStruct
    {
        public List<RgbStruct> BitmapPixelData { get; set; }

        private void FromStream(BitReader reader, DefineBitsLosslessTag.BitmapFormatType bitmapFormat, int width, int height)
        {
            Func<BitReader, RgbStruct> creator;
            int structSize;
            if (bitmapFormat == DefineBitsLosslessTag.BitmapFormatType.RGB15)
            {
                creator = RgbStruct.CreateFromRGB15Stream;
                structSize = 2;
            }
            else
            {
                creator = RgbStruct.CreateFromRGB24Stream;
                structSize = 4;
            }
            BitmapPixelData = new List<RgbStruct>(width*height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    BitmapPixelData.Add(creator(reader));
                }
                var actualByteWidth = width*structSize;
                var tgtByteWidth = (long)Math.Ceiling(actualByteWidth / 4.0)*4;
                reader.Seek(tgtByteWidth - actualByteWidth, SeekOrigin.Current);
            }
        }

        internal static BitmapDataStruct CreateFromStream(BitReader reader, DefineBitsLosslessTag.BitmapFormatType bitmapFormat, int width, int height)
        {
            var result = new BitmapDataStruct();

            result.FromStream(reader, bitmapFormat, width, height);

            return result;
        }
    }
}
