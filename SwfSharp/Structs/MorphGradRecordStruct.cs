using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class MorphGradRecordStruct
    {
        [XmlAttribute]
        public byte StartRatio { get; set; }
        [XmlElement]
        public RgbaStruct StartColor { get; set; }
        [XmlAttribute]
        public byte EndRatio { get; set; }
        [XmlElement]
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

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8(StartRatio);
            StartColor.ToStream(writer);
            writer.WriteUI8(EndRatio);
            EndColor.ToStream(writer);
        }
    }
}