﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class PlaceObjectTag : SwfTag
    {
        public ushort CharacterId { get; set; }
        public ushort Depth { get; set; }
        public MatrixStruct Matrix { get; set; }
        public CXformStruct ColorTransform { get; set; }

        public PlaceObjectTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            Depth = reader.ReadUI16();
            Matrix = MatrixStruct.CreateFromStream(reader);
            if (!reader.AtTagEnd())
            {
                ColorTransform = CXformStruct.CreateFromStream(reader);
            }
        }
    }
}