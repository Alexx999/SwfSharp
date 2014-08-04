using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class JPEGTablesTag : SwfTag
    {
        [XmlElement]
        public byte[] JPEGData { get; set; }

        public JPEGTablesTag() : this(0)
        {
        }

        public JPEGTablesTag(int size)
            : base(TagType.JPEGTables, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            JPEGData = reader.ReadBytes(Size);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBytes(JPEGData);
        }
    }
}
