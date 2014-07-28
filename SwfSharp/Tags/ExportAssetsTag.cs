using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class ExportAssetsTag : SwfTag
    {
        public List<ExportRecord> Records { get; set; }

        public ExportAssetsTag() : this(0)
        {
        }

        public ExportAssetsTag(int size)
            : base(TagType.ExportAssets, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            var count = reader.ReadUI16();
            Records = new List<ExportRecord>(count);

            for (int i = 0; i < count; i++)
            {
                Records.Add(new ExportRecord(reader.ReadUI16(), reader.ReadString()));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16((ushort)Records.Count);
            foreach (var record in Records)
            {
                writer.WriteUI16(record.Tag);
                writer.WriteString(record.Name, swfVersion);
            }
        }

        [Serializable]
        public class ExportRecord
        {
            [XmlAttribute]
            public ushort Tag { get; set; }
            [XmlAttribute]
            public string Name { get; set; }

            private ExportRecord()
            {}

            public ExportRecord(ushort tag, string name)
            {
                Tag = tag;
                Name = name;
            }
        }
    }
}
