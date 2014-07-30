using System;
using System.Diagnostics;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionGetURL2 : ActionBase
    {
        [XmlAttribute]
        public Method SendVarsMethod { get; set; }
        [XmlAttribute]
        public bool LoadTargetFlag { get; set; }
        [XmlAttribute]
        public bool LoadVariablesFlag { get; set; }

        public ActionGetURL2()
            : base(ActionType.GetURL2)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            SendVarsMethod = (Method) reader.ReadBits(2);
            reader.ReadBits(4);
            LoadTargetFlag = reader.ReadBoolBit();
            LoadVariablesFlag = reader.ReadBoolBit();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            writer.WriteUI16(1);
            writer.WriteBits(2, (uint)SendVarsMethod);
            writer.WriteBits(4, 0);
            writer.WriteBoolBit(LoadTargetFlag);
            writer.WriteBoolBit(LoadVariablesFlag);
        }
    }

    public enum Method
    {
        None, Get, Post
    }
}