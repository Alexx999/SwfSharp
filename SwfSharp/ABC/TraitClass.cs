using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    public class TraitClass : TraitsInfo
    {
        [XmlAttribute]
        public int SlotId { get; set; }
        [XmlAttribute]
        public int ClassI { get; set; }

        private TraitClass()
        {}

        public TraitClass(QName name, TraitAttributes attributes)
            : base(name, TraitKind.Class, attributes)
        {
        }

        internal override void ReadData(BitReader reader, CpoolInfo cpool)
        {
            SlotId = reader.ReadEncodedS32();
            ClassI = reader.ReadEncodedS32();
        }

        internal override void WriteData(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(SlotId);
            writer.WriteEncodedS32(ClassI);
        }
    }
}
