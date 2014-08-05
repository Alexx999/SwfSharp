using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    public class TraitFunction : TraitsInfo
    {
        [XmlAttribute]
        public int SlotId { get; set; }
        [XmlAttribute]
        public int Function { get; set; }

        private TraitFunction()
        {}

        public TraitFunction(QName name, TraitAttributes attributes)
            : base(name, TraitKind.Function, attributes)
        {
        }

        internal override void ReadData(BitReader reader, CpoolInfo cpool)
        {
            SlotId = reader.ReadEncodedS32();
            Function = reader.ReadEncodedS32();
        }

        internal override void WriteData(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(SlotId);
            writer.WriteEncodedS32(Function);
        }
    }
}
