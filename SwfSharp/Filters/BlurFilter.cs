using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Filters
{
    public class BlurFilter
    {
        public double BlurX { get; set; }
        public double BlurY { get; set; }
        public byte Passes { get; set; }

        private void FromStream(BitReader reader)
        {
            BlurX = reader.ReadFixed();
            BlurY = reader.ReadFixed();
            Passes = (byte)reader.ReadBits(5);
            reader.ReadBits(3);
        }

        internal static BlurFilter CreateFromStream(BitReader reader)
        {
            var result = new BlurFilter();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteFixed(BlurX);
            writer.WriteFixed(BlurY);
            writer.WriteBits(5, Passes);
            writer.WriteBits(3, 0);
        }
    }
}
