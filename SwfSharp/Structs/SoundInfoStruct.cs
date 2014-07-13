using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class SoundInfoStruct
    {
        public bool SyncStop { get; set; }
        public bool SyncNoMultiple { get; set; }
        public bool HasEnvelope { get; set; }
        public bool HasLoops { get; set; }
        public bool HasOutPoint { get; set; }
        public bool HasInPoint { get; set; }
        public uint InPoint { get; set; }
        public uint OutPoint { get; set; }
        public ushort LoopCount { get; set; }
        public IList<SoundEnvelopeStruct> EnvelopeRecords { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.ReadBits(2);
            SyncStop = reader.ReadBoolBit();
            SyncNoMultiple = reader.ReadBoolBit();
            HasEnvelope = reader.ReadBoolBit();
            HasLoops = reader.ReadBoolBit();
            HasOutPoint = reader.ReadBoolBit();
            HasInPoint = reader.ReadBoolBit();
            if (HasInPoint)
            {
                InPoint = reader.ReadUI32();
            }
            if (HasOutPoint)
            {
                OutPoint = reader.ReadUI32();
            }
            if (HasLoops)
            {
                LoopCount = reader.ReadUI16();
            }
            if (HasEnvelope)
            {
                var envPoints = reader.ReadUI8();
                EnvelopeRecords = new List<SoundEnvelopeStruct>(envPoints);
                for (int i = 0; i < envPoints; i++)
                {
                    EnvelopeRecords.Add(SoundEnvelopeStruct.CreateFromStream(reader));
                }
            }
        }

        internal static SoundInfoStruct CreateFromStream(BitReader reader)
        {
            var result = new SoundInfoStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
