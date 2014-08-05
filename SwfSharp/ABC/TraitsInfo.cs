using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public abstract class TraitsInfo
    {
        [XmlElement]
        public QName Name { get; set; }
        [XmlAttribute]
        public TraitKind Kind { get; set; }
        [XmlAttribute]
        public TraitAttributes Attributes { get; set; }
        [XmlArrayItem("Metadata")]
        public List<MetadataInfo> Metadata { get; set; }

        protected TraitsInfo()
        {}

        protected TraitsInfo(QName name, TraitKind kind, TraitAttributes attributes)
        {
            Name = name;
            Kind = kind;
            Attributes = attributes;
        }

        private void FromStream(BitReader reader, CpoolInfo cpool, IList<MetadataInfo> metadata)
        {
            ReadData(reader, cpool);
            if ((Attributes & TraitAttributes.Metadata) != 0)
            {
                var metadataCount = reader.ReadEncodedS32();
                Metadata = new List<MetadataInfo>(metadataCount);
                for (int i = 0; i < metadataCount; i++)
                {
                    Metadata.Add(metadata[reader.ReadEncodedS32()]);
                }
            }
        }

        internal abstract void ReadData(BitReader reader, CpoolInfo cpool);

        internal static TraitsInfo CreateFromStream(BitReader reader, CpoolInfo cpool, IList<MetadataInfo> metadata)
        {
            TraitsInfo result;
            var name = (QName)cpool.ActualMultinames[reader.ReadEncodedS32()];
            var attributes = (TraitAttributes)reader.ReadBits(4);
            var kind = (TraitKind)reader.ReadBits(4);
            switch (kind)
            {
                case TraitKind.Slot:
                case TraitKind.Const:
                {
                    result = new TraitSlot(name, kind, attributes);
                    break;
                }
                case TraitKind.Class:
                {
                    result = new TraitClass(name, attributes);
                    break;
                }
                case TraitKind.Function:
                {
                    result = new TraitFunction(name, attributes);
                    break;
                }
                case TraitKind.Method:
                case TraitKind.Getter:
                case TraitKind.Setter:
                {
                    result = new TraitMethod(name, kind, attributes);
                    break;
                }
                default:
                {
                    throw new InvalidDataException("Bad Trait kind");
                }
            }

            result.FromStream(reader, cpool, metadata);
            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool, IList<MetadataInfo> metadata)
        {
            writer.WriteEncodedS32(cpool.ActualMultinames.IndexOf(Name));
            writer.WriteBits(4, (uint) Attributes);
            writer.WriteBits(4, (uint) Kind);
            WriteData(writer, cpool);
            if ((Attributes & TraitAttributes.Metadata) != 0)
            {
                writer.WriteEncodedS32(Metadata.Count);
                foreach (var metadataInfo in Metadata)
                {
                    writer.WriteEncodedS32(metadata.IndexOf(metadataInfo));
                }
            }
        }

        internal abstract void WriteData(BitWriter writer, CpoolInfo cpool);
    }
}
