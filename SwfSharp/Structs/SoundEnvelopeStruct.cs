using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class SoundEnvelopeStruct
    {
        public uint Pos44 { get; set; }
        public ushort LeftLevel { get; set; }
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