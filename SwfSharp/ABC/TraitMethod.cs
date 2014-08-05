using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    public class TraitMethod : TraitsInfo
    {
        [XmlAttribute]
        public int DispId { get; set; }
        [XmlAttribute]
        public int Method { get; set; }

        private TraitMethod()
        {}

        public TraitMethod(QName name, TraitKind kind, TraitAttributes attributes)
            : base(name, kind, attributes)
        {
        }

        internal override void ReadData(BitReader reader, CpoolInfo cpool)
        {
            DispId = reader.ReadEncodedS32();
            Method = reader.ReadEncodedS32();
        }

        internal override void WriteData(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(DispId);
            writer.WriteEncodedS32(Method);
        }
    }
}
