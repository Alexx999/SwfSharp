using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ColorMapDataStruct
    {
        public List<RgbStruct> ColorTableRGB { get; set; }
        public byte[] ColormapPixelData { get; set; }

        private void FromStream(BitReader reader, byte bitmapColorTableSize, int dataSize)
        {
            var structsToRead = bitmapColorTableSize + 1;
            ColorTableRGB = new List<RgbStruct>(structsToRead);
            for (int i = 0; i < structsToRead; i++)
            {
                ColorTableRGB.Add(RgbStruct.CreateFromStream(reader));
            }
            ColormapPixelData = reader.ReadBytes(dataSize - structsToRead * 3);
        }

        internal static ColorMapDataStruct CreateFromStream(BitReader reader, byte bitmapColorTableSize, int dataSize)
        {
            var result = new ColorMapDataStruct();

            result.FromStream(reader, bitmapColorTableSize, dataSize);

            return result;
        }
    }
}
