using System;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetURL : ActionBase
    {
        [XmlAttribute]
        public string UrlString { get; set; }
        [XmlAttribute]
        public string TargetString { get; set; }

        public ActionGetURL()
            : base(ActionType.GetURL)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            UrlString = reader.ReadString();
            TargetString = reader.ReadString();
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
                writer.WriteString(UrlString, swfVersion);
                writer.WriteString(TargetString, swfVersion);
            }
            return result;
        }
    }
}