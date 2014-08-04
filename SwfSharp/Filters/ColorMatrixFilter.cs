using System;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    [Serializable]
    public class ColorMatrixFilter
    {
        public float[] Matrix { get; set; } 

        private void FromStream(BitReader reader)
        {
            Matrix = new float[20];
            for (int i = 0; i < 20; i++)
            {
                Matrix[i] = reader.ReadFloat();
            }
        }

        internal static ColorMatrixFilter CreateFromStream(BitReader reader)
        {
            var result = new ColorMatrixFilter();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            for (int i = 0; i < 20; i++)
            {
                writer.WriteFloat(Matrix[i]);
            }
        }
    }
}
