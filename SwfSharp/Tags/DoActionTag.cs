using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Actions;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DoActionTag : SwfTag
    {
        [XmlElement("ActionUnknown", typeof(ActionUnknown))]
        public List<ActionBase> Actions { get; set; }

        public DoActionTag() : this(0)
        {
        }

        public DoActionTag(int size)
            : base(TagType.DoAction, size)
        {
        }

        protected DoActionTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Actions = new List<ActionBase>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionFactory.ReadAction(reader));
                nextFlag = reader.ReadUI8();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            foreach (var action in Actions)
            {
                action.ToStream(writer, swfVersion);
            }
            writer.WriteUI8(0);
        }
    }
}
