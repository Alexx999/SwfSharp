using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ZoneDataStruct
    {
        [XmlAttribute]
        public float AlignmentCoordinate { get; set; }
        [XmlAttribute]
        public float Range { get; set; }

        private void FromStream(BitReader reader)
        {
            AlignmentCoordinate = reader.ReadFloat16();
            Range = reader.ReadFloat16();
        }

        internal static ZoneDataStruct CreateFromStream(BitReader reader)
        {
            var result = new ZoneDataStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteFloat16(AlignmentCoordinate);
            writer.WriteFloat16(Range);
        }
    }
}
