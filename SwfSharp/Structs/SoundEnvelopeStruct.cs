using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class SoundEnvelopeStruct
    {
        [XmlAttribute]
        public uint Pos44 { get; set; }
        [XmlAttribute]
        public ushort LeftLevel { get; set; }
        [XmlAttribute]
        public ushort RightLevel { get; set; }

        private void FromStream(BitReader reader)
        {
            Pos44 = reader.ReadUI32();
            LeftLevel = reader.ReadUI16();
            RightLevel = reader.ReadUI16();
        }

        internal static SoundEnvelopeStruct CreateFromStream(BitReader reader)
        {
            var result = new SoundEnvelopeStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI32(Pos44);
            writer.WriteUI16(LeftLevel);
            writer.WriteUI16(RightLevel);
        }
    }
}