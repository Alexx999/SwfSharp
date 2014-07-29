using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class ProductInfoTag : SwfTag
    {
        [XmlAttribute]
        public ProductInfoProduct Product { get; set; }
        [XmlAttribute]
        public ProductInfoEdition Edition { get; set; }
        [XmlAttribute]
        public sbyte MajorVersion { get; set; }
        [XmlAttribute]
        public sbyte MinorVersion { get; set; }
        [XmlAttribute]
        public long Build { get; set; }
        [XmlAttribute]
        public long CompileDate { get; set; }

        public ProductInfoTag() : this(0)
        {
        }

        public ProductInfoTag(int size)
            : base(TagType.ProductInfo, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Product = (ProductInfoProduct) reader.ReadSI32();
            Edition = (ProductInfoEdition) reader.ReadSI32();
            MajorVersion = reader.ReadSI8();
            MinorVersion = reader.ReadSI8();
            Build = reader.ReadSI64();
            CompileDate = reader.ReadSI64();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteSI32((int)Product);
            writer.WriteSI32((int)Edition);
            writer.WriteSI8(MajorVersion);
            writer.WriteSI8(MinorVersion);
            writer.WriteSI64(Build);
            writer.WriteSI64(CompileDate);
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
