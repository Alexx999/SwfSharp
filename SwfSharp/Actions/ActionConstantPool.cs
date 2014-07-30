using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionConstantPool : ActionBase
    {
        [XmlElement("Constant")]
        public List<string> ConstantPool { get; set; } 

        public ActionConstantPool()
            : base(ActionType.ConstantPool)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            var count = reader.ReadUI16();
            ConstantPool = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                ConstantPool.Add(reader.ReadString());
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            var ms = GetDataStream(swfVersion);
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                writer.WriteUI16((ushort) ConstantPool.Count);
                foreach (var constant in ConstantPool)
                {
                    writer.WriteString(constant, swfVersion);
                }
            }
            return result;
        }
    }
}