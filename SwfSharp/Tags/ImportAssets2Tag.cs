using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class ImportAssets2Tag : ImportAssetsTag
    {
        public ImportAssets2Tag(int size)
            : base(TagType.ImportAssets2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            URL = reader.ReadString();

            reader.ReadUI8();
            reader.ReadUI8();

            var count = reader.ReadUI16();
            Records = new List<ImportRecord>(count);

            for (int i = 0; i < count; i++)
            {
                Records.Add(new ImportRecord(reader.ReadUI16(), reader.ReadString()));
            }
        }
    }
}
