using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ActionRecordStruct
    {
        [XmlAttribute]
        public byte ActionCode { get; set; }
        public byte[] Data { get; set; }

        private void FromStream(BitReader reader)
        {
            ActionCode = reader.ReadUI8();
            if (ActionCode < 0x80) return;
            var length = reader.ReadUI16();
            Data = reader.ReadBytes(length);
        }

        internal static ActionRecordStruct CreateFromStream(BitReader reader)
        {
            var result = new ActionRecordStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8(ActionCode);
            if (ActionCode < 0x80) return;
            writer.WriteUI16((ushort) Data.Length);
            writer.WriteBytes(Data);
        }
    }
}
