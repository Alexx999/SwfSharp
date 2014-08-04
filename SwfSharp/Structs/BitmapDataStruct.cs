using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class BitmapDataStruct
    {
        [XmlArrayItem("Color")]
        public List<RgbStruct> BitmapPixelData { get; set; }

        private void FromStream(BitReader reader, DefineBitsLosslessTag.BitmapFormatType bitmapFormat, int width, int height)
        {
            int structSize = bitmapFormat == DefineBitsLosslessTag.BitmapFormatType.RGB15 ? 2 : 4;
            BitmapPixelData = new List<RgbStruct>(width*height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    BitmapPixelData.Add(bitmapFormat == DefineBitsLosslessTag.BitmapFormatType.RGB15
                        ? RgbStruct.CreateFromRGB15Stream(reader)
                        : RgbStruct.CreateFromRGB24Stream(reader));
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
