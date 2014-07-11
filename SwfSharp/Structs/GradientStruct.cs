using System.Collections.Generic;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class GradientStruct
    {
        public Spread SpreadMode { get; set; }
        public Interpolation InterpolationMode { get; set; }
        public IList<GradRecordStruct> GradientRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            SpreadMode = (Spread) reader.ReadBits(2);
            InterpolationMode = (Interpolation)reader.ReadBits(2);
            var numGradients = reader.ReadBits(4);
            GradientRecords = new List<GradRecordStruct>((int) numGradients);
            for (int i = 0; i < numGradients; i++)
            {
                GradientRecords.Add(GradRecordStruct.CreateFromStream(reader, type));
            }
        }

        internal static GradientStruct CreateFromStream(BitReader reader, TagType type)
        {
            var result = new GradientStruct();

            result.FromStream(reader, type);

            return result;
        }

        public enum Spread
        {
            Pad = 0,
            Reflect = 1,
            Repeat = 2
        }

        public enum Interpolation
        {
            Normal = 0,
            Linear = 1
        }
    }
}