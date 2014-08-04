using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Actions;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ButtonCondActionStruct
    {
        [XmlAttribute]
        public ushort CondActionSize { get; set; }
        [XmlAttribute]
        public bool CondIdleToOverDown { get; set; }
        [XmlAttribute]
        public bool CondOutDownToIdle { get; set; }
        [XmlAttribute]
        public bool CondOutDownToOverDown { get; set; }
        [XmlAttribute]
        public bool CondOverDownToOutDown { get; set; }
        [XmlAttribute]
        public bool CondOverDownToOverUp { get; set; }
        [XmlAttribute]
        public bool CondOverUpToOverDown { get; set; }
        [XmlAttribute]
        public bool CondOverUpToIdle { get; set; }
        [XmlAttribute]
        public bool CondIdleToOverUp { get; set; }
        [XmlAttribute]
        public byte CondKeyPress { get; set; }
        [XmlAttribute]
        public bool CondOverDownToIdle { get; set; }
        [XmlElement("ActionRecord")]
        public List<ActionBase> Actions { get; set; }

        private void FromStream(BitReader reader)
        {
            CondActionSize = reader.ReadUI16();
            CondIdleToOverDown = reader.ReadBoolBit();
            CondOutDownToIdle = reader.ReadBoolBit();
            CondOutDownToOverDown = reader.ReadBoolBit();
            CondOverDownToOutDown = reader.ReadBoolBit();
            CondOverDownToOverUp = reader.ReadBoolBit();
            CondOverUpToOverDown = reader.ReadBoolBit();
            CondOverUpToIdle = reader.ReadBoolBit();
            CondIdleToOverUp = reader.ReadBoolBit();
            CondKeyPress = (byte)reader.ReadBits(7);
            CondOverDownToIdle = reader.ReadBoolBit();
            Actions = new List<ActionBase>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionFactory.ReadAction(reader));
                nextFlag = reader.ReadUI8();
            }
        }

        internal static ButtonCondActionStruct CreateFromStream(BitReader reader)
        {
            var result = new ButtonCondActionStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CondActionSize);
            writer.WriteBoolBit(CondIdleToOverDown);
            writer.WriteBoolBit(CondOutDownToIdle);
            writer.WriteBoolBit(CondOutDownToOverDown);
            writer.WriteBoolBit(CondOverDownToOutDown);
            writer.WriteBoolBit(CondOverDownToOverUp);
            writer.WriteBoolBit(CondOverUpToOverDown);
            writer.WriteBoolBit(CondOverUpToIdle);
            writer.WriteBoolBit(CondIdleToOverUp);
            writer.WriteBits(7, CondKeyPress);
            writer.WriteBoolBit(CondOverDownToIdle);
            foreach (var action in Actions)
            {
                action.ToStream(writer, swfVersion);
            }
            writer.WriteUI8(0);
        }
    }
}
