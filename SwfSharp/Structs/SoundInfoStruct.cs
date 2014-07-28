using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class SoundInfoStruct
    {
        [XmlAttribute]
        public bool SyncStop { get; set; }
        [XmlAttribute]
        public bool SyncNoMultiple { get; set; }
        [XmlAttribute]
        public bool HasEnvelope { get; set; }
        [XmlAttribute]
        public bool HasLoops { get; set; }
        [XmlAttribute]
        public bool HasOutPoint { get; set; }
        [XmlAttribute]
        public bool HasInPoint { get; set; }
        public uint InPoint { get; set; }
        public uint OutPoint { get; set; }
        public ushort LoopCount { get; set; }
        public List<SoundEnvelopeStruct> EnvelopeRecords { get; set; }

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

        internal void ToStream(BitWriter writer)
        {
            writer.WriteBits(2, 0);
            writer.WriteBoolBit(SyncStop);
            writer.WriteBoolBit(SyncNoMultiple);
            writer.WriteBoolBit(HasEnvelope);
            writer.WriteBoolBit(HasLoops);
            writer.WriteBoolBit(HasOutPoint);
            writer.WriteBoolBit(HasInPoint);
            if (HasInPoint)
            {
                writer.WriteUI32(InPoint);
            }
            if (HasOutPoint)
            {
                writer.WriteUI32(OutPoint);
            }
            if (HasLoops)
            {
                writer.WriteUI16(LoopCount);
            }
            if (HasEnvelope)
            {
                writer.WriteUI8((byte) EnvelopeRecords.Count);
                foreach (var record in EnvelopeRecords)
                {
                    record.ToStream(writer);
                }
            }
        }
    }
}
