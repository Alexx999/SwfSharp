using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class ABCFile
    {
        [XmlAttribute]
        public ushort MinorVersion { get; set; }
        [XmlAttribute]
        public ushort MajorVersion { get; set; }
        [XmlElement]
        public CpoolInfo ConstantPool { get; set; }
        [XmlArrayItem("Method")]
        public List<MethodInfo> Methods { get; set; }
        [XmlArrayItem("Metadata")]
        public List<MetadataInfo> Metadata { get; set; }
        [XmlElement]
        public byte[] Data { get; set; }

        private void FromStream(BitReader reader, int dataSize)
        {
            var pos = reader.Position;
            MinorVersion = reader.ReadUI16();
            MajorVersion = reader.ReadUI16();
            ConstantPool = CpoolInfo.CreateFromStream(reader);
            var methodCount = reader.ReadEncodedS32();
            Methods = new List<MethodInfo>(methodCount);
            for (int i = 0; i < methodCount; i++)
            {
                Methods.Add(MethodInfo.CreateFromStream(reader, ConstantPool));
            }
            var methadataCount = reader.ReadEncodedS32();
            Metadata = new List<MetadataInfo>(methadataCount);
            for (int i = 0; i < methadataCount; i++)
            {
                Metadata.Add(MetadataInfo.CreateFromStream(reader, ConstantPool));
            }
            Data = reader.ReadBytes(dataSize - (int)(reader.Position - pos));
        }

        internal static ABCFile CreateFromStream(BitReader reader, int dataSize)
        {
            var result = new ABCFile();
            result.FromStream(reader, dataSize);
            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI16(MinorVersion);
            writer.WriteUI16(MajorVersion);
            ConstantPool.ToStream(writer);
            writer.WriteEncodedS32(Methods.Count);
            foreach (var method in Methods)
            {
                method.ToStream(writer, ConstantPool);
            }
            writer.WriteEncodedS32(Metadata.Count);
            foreach (var metadataInfo in Metadata)
            {
                metadataInfo.ToStream(writer, ConstantPool);
            }
            writer.WriteBytes(Data);
        }
    }
}
