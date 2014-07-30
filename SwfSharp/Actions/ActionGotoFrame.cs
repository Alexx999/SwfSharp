using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGotoFrame : ActionBase
    {
        [XmlAttribute]
        public ushort Frame { get; set; }

        public ActionGotoFrame()
            : base(ActionType.GotoFrame)
        {}

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            Frame = reader.ReadUI16();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(2);
            writer.WriteUI16(Frame);
        }
    }
}