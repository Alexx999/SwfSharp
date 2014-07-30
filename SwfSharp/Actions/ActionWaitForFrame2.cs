using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionWaitForFrame2 : ActionBase
    {
        [XmlAttribute]
        public byte SkipCount { get; set; }

        public ActionWaitForFrame2()
            : base(ActionType.WaitForFrame2)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            SkipCount = reader.ReadUI8();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(1);
            writer.WriteUI8(SkipCount);
        }
    }
}