using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ProductInfoTag : SwfTag
    {
        public ProductInfoProduct Product { get; set; }
        public ProductInfoEdition Edition { get; set; }
        public sbyte MajorVersion { get; set; }
        public sbyte MinorVersion { get; set; }
        public long Build { get; set; }
        public long CompileDate { get; set; }

        public ProductInfoTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            Product = (ProductInfoProduct) reader.ReadSI32();
            Edition = (ProductInfoEdition) reader.ReadSI32();
            MajorVersion = reader.ReadSI8();
            MinorVersion = reader.ReadSI8();
            Build = reader.ReadSI64();
            CompileDate = reader.ReadSI64();
        }

        public enum ProductInfoEdition
        {
            Developer = 0,
            FullCommercial = 1,
            NonCommercial = 2,
            Educational = 3,
            NFR = 4,
            Trial = 5,
            None = 6
        }

        public enum ProductInfoProduct
        {
            Unknown = 0,
            J2EE = 1,
            Dotnet = 2,
            Flex = 3
        }
    }
}
