using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class AlphaBitmapDataStruct
    {
        [XmlElement]
        public List<ArgbStruct> BitmapPixelData { get; set; }

        private void FromStream(BitReader reader, int width, int height)
        {
            BitmapPixelData = new List<ArgbStruct>(width*height);
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
