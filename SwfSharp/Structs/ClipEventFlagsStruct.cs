using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ClipEventFlagsStruct
    {
        [XmlAttribute]
        public bool ClipEventKeyUp { get; set; }
        [XmlAttribute]
        public bool ClipEventKeyDown { get; set; }
        [XmlAttribute]
        public bool ClipEventMouseUp { get; set; }
        [XmlAttribute]
        public bool ClipEventMouseDown { get; set; }
        [XmlAttribute]
        public bool ClipEventMouseMove { get; set; }
        [XmlAttribute]
        public bool ClipEventUnload { get; set; }
        [XmlAttribute]
        public bool ClipEventEnterFrame { get; set; }
        [XmlAttribute]
        public bool ClipEventLoad { get; set; }
        [XmlAttribute]
        public bool ClipEventDragOver { get; set; }
        [XmlAttribute]
        public bool ClipEventRollOut { get; set; }
        [XmlAttribute]
        public bool ClipEventRollOver { get; set; }
        [XmlAttribute]
        public bool ClipEventReleaseOutside { get; set; }
        [XmlAttribute]
        public bool ClipEventRelease { get; set; }
        [XmlAttribute]
        public bool ClipEventPress { get; set; }
        [XmlAttribute]
        public bool ClipEventInitialize { get; set; }
        [XmlAttribute]
        public bool ClipEventData { get; set; }
        [XmlAttribute]
        public bool ClipEventConstruct { get; set; }
        [XmlAttribute]
        public bool ClipEventKeyPress { get; set; }
        [XmlAttribute]
        public bool ClipEventDragOut { get; set; }

        private void FromStream(BitReader reader, byte swfVersion)
        {
            ClipEventKeyUp = reader.ReadBoolBit();
            ClipEventKeyDown = reader.ReadBoolBit();
            ClipEventMouseUp = reader.ReadBoolBit();
            ClipEventMouseDown = reader.ReadBoolBit();
            ClipEventMouseMove = reader.ReadBoolBit();
            ClipEventUnload = reader.ReadBoolBit();
            ClipEventEnterFrame = reader.ReadBoolBit();
            ClipEventLoad = reader.ReadBoolBit();
            ClipEventDragOver = reader.ReadBoolBit();
            ClipEventRollOut = reader.ReadBoolBit();
            ClipEventRollOver = reader.ReadBoolBit();
            ClipEventReleaseOutside = reader.ReadBoolBit();
            ClipEventRelease = reader.ReadBoolBit();
            ClipEventPress = reader.ReadBoolBit();
            ClipEventInitialize = reader.ReadBoolBit();
            ClipEventData = reader.ReadBoolBit();
            if (swfVersion < 6) return;
            reader.ReadBits(5);
            ClipEventConstruct = reader.ReadBoolBit();
            ClipEventKeyPress = reader.ReadBoolBit();
            ClipEventDragOut = reader.ReadBoolBit();
            reader.ReadBits(8);
        }

        internal static ClipEventFlagsStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipEventFlagsStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }

        internal void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBoolBit(ClipEventKeyUp);
            writer.WriteBoolBit(ClipEventKeyDown);
            writer.WriteBoolBit(ClipEventMouseUp);
            writer.WriteBoolBit(ClipEventMouseDown);
            writer.WriteBoolBit(ClipEventMouseMove);
            writer.WriteBoolBit(ClipEventUnload);
            writer.WriteBoolBit(ClipEventEnterFrame);
            writer.WriteBoolBit(ClipEventLoad);
            writer.WriteBoolBit(ClipEventDragOver);
            writer.WriteBoolBit(ClipEventRollOut);
            writer.WriteBoolBit(ClipEventRollOver);
            writer.WriteBoolBit(ClipEventReleaseOutside);
            writer.WriteBoolBit(ClipEventRelease);
            writer.WriteBoolBit(ClipEventPress);
            writer.WriteBoolBit(ClipEventInitialize);
            writer.WriteBoolBit(ClipEventData);
            if (swfVersion < 6) return;
            writer.WriteBits(5, 0);
            writer.WriteBoolBit(ClipEventConstruct);
            writer.WriteBoolBit(ClipEventKeyPress);
            writer.WriteBoolBit(ClipEventDragOut);
            writer.WriteBits(8, 0);
        }
    }
}
