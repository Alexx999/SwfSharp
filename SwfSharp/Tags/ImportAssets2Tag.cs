using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class ImportAssets2Tag : ImportAssetsTag
    {
        public ImportAssets2Tag() : this(0)
        {
        }

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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteString(URL, swfVersion);

            writer.WriteUI8(1);
            writer.WriteUI8(0);

            writer.WriteUI16((ushort) Records.Count);
            
            foreach (var record in Records)
            {
                writer.WriteUI16(record.Tag);
                writer.WriteString(record.Name, swfVersion);
            }
        }
    }
}
