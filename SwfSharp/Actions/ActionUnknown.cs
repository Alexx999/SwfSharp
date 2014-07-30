using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionUnknown : ActionBase
    {
        private ActionUnknown()
        {}

        public ActionUnknown(ActionType actionCode) : base(actionCode)
        {}

        [XmlAttribute]
        public new byte ActionCode
        {
            get { return (byte) base.ActionCode; }
            set { base.ActionCode = (ActionType) value; }
        }

        [XmlElement]
        public byte[] Data { get; set; }

        internal override void FromStream(BitReader reader)
        {
            if (ActionCode < 0x80) return;
            var length = reader.ReadUI16();
            Data = reader.ReadBytes(length);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI8(ActionCode);
            if (ActionCode < 0x80) return;
            writer.WriteUI16((ushort)Data.Length);
            writer.WriteBytes(Data);
        }
    }
}
