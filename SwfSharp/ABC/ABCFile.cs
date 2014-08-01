using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [XmlElement]
        public byte[] Data { get; set; }

        private void FromStream(BitReader reader, int dataSize)
        {
            var pos = reader.Position;
            MinorVersion = reader.ReadUI16();
            MajorVersion = reader.ReadUI16();
            ConstantPool = CpoolInfo.CreateFromStream(reader);
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
            writer.WriteBytes(Data);
        }
    }
}
