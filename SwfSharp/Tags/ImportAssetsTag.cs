﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ImportAssetsTag : SwfTag
    {
        public string URL { get; set; }
        public IList<ImportRecord> Records { get; set; }

        public ImportAssetsTag() : this(0)
        {
        }

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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteString(URL, swfVersion);
            writer.WriteUI16((ushort) Records.Count);

            foreach (var record in Records)
            {
                writer.WriteUI16(record.Tag);
                writer.WriteString(record.Name, swfVersion);
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
