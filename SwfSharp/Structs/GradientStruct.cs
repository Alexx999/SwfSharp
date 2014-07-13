using System.Collections.Generic;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class GradientStruct
    {
        public SpreadMode SpreadMode { get; set; }
        public InterpolationMode InterpolationMode { get; set; }
        public IList<GradRecordStruct> GradientRecords { get; set; }

        internal virtual void FromStream(BitReader reader, TagType type)
        {
            reader.Align();
            SpreadMode = (SpreadMode)reader.ReadBits(2);
            InterpolationMode = (InterpolationMode)reader.ReadBits(2);
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
    }
}