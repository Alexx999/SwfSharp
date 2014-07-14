using System.Collections.Generic;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ExportAssetsTag : SwfTag
    {
        public IList<ExportRecord> Records { get; set; }

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
            throw new System.NotImplementedException();
        }

        public class ExportRecord
        {
            public ushort Tag { get; set; }
            public string Name { get; set; }
            public ExportRecord(ushort tag, string name)
            {
                Tag = tag;
                Name = name;
            }
        }
    }
}
