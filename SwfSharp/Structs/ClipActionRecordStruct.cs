using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Actions;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ClipActionRecordStruct
    {
        private byte? _keyCode;

        [XmlElement]
        public ClipEventFlagsStruct EventFlags { get; set; }

        [XmlAttribute]
        public byte KeyCode
        {
            get { return _keyCode.GetValueOrDefault(); }
            set { _keyCode = value; }
        }

        [XmlIgnore]
        public bool KeyCodeSpecified
        {
            get { return _keyCode.HasValue; }
        }

        [XmlArrayItem("ActionRecord")]
        public List<ActionBase> Actions { get; set; } 

        private void FromStream(BitReader reader, byte swfVersion)
        {
            EventFlags = ClipEventFlagsStruct.CreateFromStream(reader, swfVersion);
            var actionRecordSize = reader.ReadUI32();
            var startPos = reader.Position;
            if (EventFlags.ClipEventKeyPress)
            {
                _keyCode = reader.ReadUI8();
            }
            Actions = new List<ActionBase>();

            while (reader.Position < startPos + actionRecordSize)
            {
                var record = ActionFactory.ReadAction(reader);
                Actions.Add(record);
            }
        }

        internal static ClipActionRecordStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipActionRecordStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }

        internal void ToStream(BitWriter writer, byte swfVersion)
        {
            EventFlags.ToStream(writer, swfVersion);
            var ms = GetDataStream(swfVersion);
            writer.WriteUI32((uint) ms.Length);
            writer.WriteBytes(ms.GetBuffer(), 0, (int) ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                if (EventFlags.ClipEventKeyPress)
                {
                    writer.WriteUI8(_keyCode.GetValueOrDefault());
                }

                foreach (var action in Actions)
                {
                    action.ToStream(writer, swfVersion);
                }
            }
            return result;
        }
    }
}
