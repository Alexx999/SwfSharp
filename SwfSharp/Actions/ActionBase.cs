using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public abstract class ActionBase
    {
        [XmlIgnore]
        public ActionType ActionCode { get; set; }

        protected ActionBase()
        { }

        public ActionBase(ActionType actionCode)
        {
            ActionCode = actionCode;
        }

        internal virtual void FromStream(BitReader reader)
        {}

        internal virtual void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI8((byte) ActionCode);
        }
    }
}
