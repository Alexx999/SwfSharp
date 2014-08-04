using System;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGotoFrame2 : ActionBase
    {
        private ushort? _sceneBias;

        [XmlAttribute]
        public bool Play { get; set; }

        [XmlAttribute]
        public ushort SceneBias
        {
            get { return _sceneBias.GetValueOrDefault(); }
            set { _sceneBias = value; }
        }

        [XmlIgnore]
        public bool SceneBiasSpecified
        {
            get { return _sceneBias.HasValue; }
        }

        public ActionGotoFrame2()
            : base(ActionType.GotoFrame2)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            reader.ReadBits(6);
            var sceneBiasFlag = reader.ReadBoolBit();
            Play = reader.ReadBoolBit();
            if (sceneBiasFlag)
            {
                SceneBias = reader.ReadUI16();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            var ms = GetDataStream(swfVersion);
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                var sceneBiasFlag = _sceneBias.HasValue;
                writer.WriteBits(6, 0);
                writer.WriteBoolBit(sceneBiasFlag);
                writer.WriteBoolBit(Play);
                if (sceneBiasFlag)
                {
                    writer.WriteUI16(_sceneBias.Value);
                }
            }
            return result;
        }
    }
}