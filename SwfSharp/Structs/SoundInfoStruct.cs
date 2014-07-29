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
        private uint? _inPoint;
        private uint? _outPoint;
        private ushort? _loopCount;

        [XmlAttribute]
        public bool SyncStop { get; set; }
        [XmlAttribute]
        public bool SyncNoMultiple { get; set; }

        [XmlAttribute]
        public uint InPoint
        {
            get { return _inPoint.GetValueOrDefault(); }
            set { _inPoint = value; }
        }

        [XmlAttribute]
        public uint OutPoint
        {
            get { return _outPoint.GetValueOrDefault(); }
            set { _outPoint = value; }
        }

        [XmlAttribute]
        public ushort LoopCount
        {
            get { return _loopCount.GetValueOrDefault(); }
            set { _loopCount = value; }
        }

        public List<SoundEnvelopeStruct> EnvelopeRecords { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.ReadBits(2);
            SyncStop = reader.ReadBoolBit();
            SyncNoMultiple = reader.ReadBoolBit();
            var hasEnvelope = reader.ReadBoolBit();
            var hasLoops = reader.ReadBoolBit();
            var hasOutPoint = reader.ReadBoolBit();
            var hasInPoint = reader.ReadBoolBit();
            if (hasInPoint)
            {
                _inPoint = reader.ReadUI32();
            }
            if (hasOutPoint)
            {
                _outPoint = reader.ReadUI32();
            }
            if (hasLoops)
            {
                _loopCount = reader.ReadUI16();
            }
            if (hasEnvelope)
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
            var hasEnvelope = EnvelopeRecords != null;
            var hasLoops = _loopCount.HasValue;
            var hasOutPoint = _outPoint.HasValue;
            var hasInPoint = _inPoint.HasValue;

            writer.WriteBits(2, 0);
            writer.WriteBoolBit(SyncStop);
            writer.WriteBoolBit(SyncNoMultiple);
            writer.WriteBoolBit(hasEnvelope);
            writer.WriteBoolBit(hasLoops);
            writer.WriteBoolBit(hasOutPoint);
            writer.WriteBoolBit(hasInPoint);
            if (hasInPoint)
            {
                writer.WriteUI32(_inPoint.Value);
            }
            if (hasOutPoint)
            {
                writer.WriteUI32(_outPoint.Value);
            }
            if (hasLoops)
            {
                writer.WriteUI16(_loopCount.Value);
            }
            if (hasEnvelope)
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
