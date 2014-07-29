using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class FileAttributesTag : SwfTag
    {

        [XmlAttribute]
        public bool UseDirectBlit { get; set; }
        [XmlAttribute]
        public bool UseGPU { get; set; }
        [XmlAttribute]
        public bool HasMetadata { get; set; }
        [XmlAttribute]
        public bool ActionScript3 { get; set; }
        [XmlAttribute]
        public bool SuppressCrossDomainCaching { get; set; }
        [XmlAttribute]
        public bool SwfRelativeUrls { get; set; }
        [XmlAttribute]
        public bool UseNetwork { get; set; }

        public FileAttributesTag()
            : base(TagType.FileAttributes, 0)
        {
        }

        public FileAttributesTag(int size)
            : base(TagType.FileAttributes, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadBits(1);
            UseDirectBlit = reader.ReadBoolBit();
            UseGPU = reader.ReadBoolBit();
            HasMetadata = reader.ReadBoolBit();
            ActionScript3 = reader.ReadBoolBit();
            SuppressCrossDomainCaching = reader.ReadBoolBit();
            SwfRelativeUrls = reader.ReadBoolBit();
            UseNetwork = reader.ReadBoolBit();
            reader.ReadUI24();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBits(1, 0);
            writer.WriteBoolBit(UseDirectBlit);
            writer.WriteBoolBit(UseGPU);
            writer.WriteBoolBit(HasMetadata);
            writer.WriteBoolBit(ActionScript3);
            writer.WriteBoolBit(SuppressCrossDomainCaching);
            writer.WriteBoolBit(SwfRelativeUrls);
            writer.WriteBoolBit(UseNetwork);
            writer.WriteUI24(0);
        }
    }
}
