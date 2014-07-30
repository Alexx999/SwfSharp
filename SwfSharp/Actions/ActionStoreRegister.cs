using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionStoreRegister : ActionBase
    {
        [XmlAttribute]
        public byte RegisterNumber { get; set; }

        public ActionStoreRegister()
            : base(ActionType.StoreRegister)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            RegisterNumber = reader.ReadUI8();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(1);
            writer.WriteUI8(RegisterNumber);
        }
    }
}