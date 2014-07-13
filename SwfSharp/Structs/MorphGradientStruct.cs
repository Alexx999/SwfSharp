using System.Collections.Generic;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class MorphGradientStruct
    {
        public IList<MorphGradRecordStruct> GradientRecords { get; set; }

        private void FromStream(BitReader reader)
        {
            var numGradients = reader.ReadUI8();
            GradientRecords = new List<MorphGradRecordStruct>(numGradients);
            for (int i = 0; i < numGradients; i++)
            {
                GradientRecords.Add(MorphGradRecordStruct.CreateFromStream(reader));
            }
        }

        internal static MorphGradientStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphGradientStruct();

            result.FromStream(reader);

            return result;
        }
    }
}