using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class AlphaColorMapDataStruct
    {
        [XmlArrayItem("Color")]
        public List<RgbaStruct> ColorTableRGB { get; set; }
        [XmlElement]
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
