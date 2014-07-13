using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class ImportAssetsTag : SwfTag
    {
        public string URL { get; set; }
        public IList<ImportRecord> Records { get; set; }

        public ImportAssetsTag(int size)
            : base(TagType.ImportAssets, size)
        {
        }

        protected ImportAssetsTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            URL = reader.ReadString();

            var count = reader.ReadUI16();
            Records = new List<ImportRecord>(count);

            for (int i = 0; i < count; i++)
            {
                Records.Add(new ImportRecord(reader.ReadUI16(), reader.ReadString()));
            }
        }

        public class ImportRecord
        {
            public ushort Tag { get; set; }
            public string Name { get; set; }
            public ImportRecord(ushort tag, string name)
            {
                Tag = tag;
                Name = name;
            }
        }
    }
}
