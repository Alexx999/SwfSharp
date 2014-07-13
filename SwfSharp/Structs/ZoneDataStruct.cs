﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class ZoneDataStruct
    {
        public float AlignmentCoordinate { get; set; }
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
    }
}
