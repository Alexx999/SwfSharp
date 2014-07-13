using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MorphGradRecordStruct
    {
        public byte StartRatio { get; set; }
        public RgbaStruct StartColor { get; set; }
        public byte EndRatio { get; set; }
        public RgbaStruct EndColor { get; set; }

        private void FromStream(BitReader reader)
        {
            StartRatio = reader.ReadUI8();
            StartColor = RgbaStruct.CreateFromStream(reader);
            EndRatio = reader.ReadUI8();
            EndColor = RgbaStruct.CreateFromStream(reader);
        }

        internal static MorphGradRecordStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphGradRecordStruct();

            result.FromStream(reader);

            return result;
        }
    }
}