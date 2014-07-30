using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionIf : ActionBase
    {
        [XmlAttribute]
        public short BranchOffset { get; set; }

        public ActionIf()
            : base(ActionType.If)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            BranchOffset = reader.ReadSI16();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(2);
            writer.WriteSI16(BranchOffset);
        }
    }
}