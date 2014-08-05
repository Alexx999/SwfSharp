using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class TraitSlot : TraitsInfo
    {
        [XmlAttribute]
        public int SlotId { get; set; }
        [XmlElement]
        public MultinameInfo TypeName { get; set; }
        [XmlAttribute]
        public ConstantKind VKind { get; set; }
        [XmlAttribute]
        public int VIndex { get; set; }

        private TraitSlot()
        {}

        public TraitSlot(QName name, TraitKind kind, TraitAttributes attributes)
            : base(name, kind, attributes)
        {
        }

        internal override void ReadData(BitReader reader, CpoolInfo cpool)
        {
            SlotId = reader.ReadEncodedS32();
            TypeName = cpool.ActualMultinames[reader.ReadEncodedS32()];
            VIndex = reader.ReadEncodedS32();
            if (VIndex != 0)
            {
                VKind = (ConstantKind) reader.ReadUI8();
            }
        }

        internal override void WriteData(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(SlotId);
            writer.WriteEncodedS32(cpool.ActualMultinames.IndexOf(TypeName));
            writer.WriteEncodedS32(VIndex);
            if (VIndex != 0)
            {
                writer.WriteUI8((byte) VKind);
            }
        }
    }
}
