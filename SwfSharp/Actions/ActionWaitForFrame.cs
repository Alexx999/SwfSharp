using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionWaitForFrame : ActionBase
    {
        [XmlAttribute]
        public ushort Frame { get; set; }
        [XmlAttribute]
        public byte SkipCount { get; set; }

        public ActionWaitForFrame()
            : base(ActionType.WaitForFrame)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            Frame = reader.ReadUI16();
            SkipCount = reader.ReadUI8();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(3);
            writer.WriteUI16(Frame);
            writer.WriteUI8(SkipCount);
        }
    }
}