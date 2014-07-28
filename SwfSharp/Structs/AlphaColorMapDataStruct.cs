using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class AlphaColorMapDataStruct
    {
        public List<RgbaStruct> ColorTableRGB { get; set; }
        public byte[] ColormapPixelData { get; set; }

        private void FromStream(BitReader reader, byte bitmapColorTableSize, int dataSize)
        {
            var structsToRead = bitmapColorTableSize + 1;
            ColorTableRGB = new List<RgbaStruct>(structsToRead);
            for (int i = 0; i < structsToRead; i++)
            {
                ColorTableRGB.Add(RgbaStruct.CreateFromStream(reader));
            }
            ColormapPixelData = reader.ReadBytes(dataSize - structsToRead * 3);
        }

        internal static AlphaColorMapDataStruct CreateFromStream(BitReader reader, byte bitmapColorTableSize, int dataSize)
        {
            var result = new AlphaColorMapDataStruct();

            result.FromStream(reader, bitmapColorTableSize, dataSize);

            return result;
        }
    }
}
