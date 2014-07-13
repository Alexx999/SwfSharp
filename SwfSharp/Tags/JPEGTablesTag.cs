﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class JPEGTablesTag : SwfTag
    {
        public byte[] JPEGData { get; set; }

        public JPEGTablesTag(int size)
            : base(TagType.JPEGTables, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            JPEGData = reader.ReadBytes(Size);
        }
    }
}
