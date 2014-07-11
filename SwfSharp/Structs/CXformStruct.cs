﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class CXformStruct
    {
        public bool HasAddTerms { get; set; }
        public bool HasMultTerms { get; set; }
        public int RedMultTerm { get; set; }
        public int GreenMultTerm { get; set; }
        public int BlueMultTerm { get; set; }
        public int RedAddTerm { get; set; }
        public int GreenAddTerm { get; set; }
        public int BlueAddTerm { get; set; }

        private void FromStream(BitReader reader)
        {
            reader.Align();

            HasAddTerms = reader.ReadBoolBit();
            HasMultTerms = reader.ReadBoolBit();

            var nbits = reader.ReadBits(4);

            if (HasMultTerms)
            {
                RedMultTerm = reader.ReadBitsSigned(nbits);
                GreenMultTerm = reader.ReadBitsSigned(nbits);
                BlueMultTerm = reader.ReadBitsSigned(nbits);
            }
            if (HasAddTerms)
            {
                RedAddTerm = reader.ReadBitsSigned(nbits);
                GreenAddTerm = reader.ReadBitsSigned(nbits);
                BlueAddTerm = reader.ReadBitsSigned(nbits);
            }
        }

        internal static CXformStruct CreateFromStream(BitReader reader)
        {
            var result = new CXformStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
