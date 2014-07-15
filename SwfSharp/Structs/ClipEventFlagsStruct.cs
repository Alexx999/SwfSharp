using System;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ClipEventFlagsStruct
    {
        public bool ClipEventKeyUp { get; set; }
        public bool ClipEventKeyDown { get; set; }
        public bool ClipEventMouseUp { get; set; }
        public bool ClipEventMouseDown { get; set; }
        public bool ClipEventMouseMove { get; set; }
        public bool ClipEventUnload { get; set; }
        public bool ClipEventEnterFrame { get; set; }
        public bool ClipEventLoad { get; set; }
        public bool ClipEventDragOver { get; set; }
        public bool ClipEventRollOut { get; set; }
        public bool ClipEventRollOver { get; set; }
        public bool ClipEventReleaseOutside { get; set; }
        public bool ClipEventRelease { get; set; }
        public bool ClipEventPress { get; set; }
        public bool ClipEventInitialize { get; set; }
        public bool ClipEventData { get; set; }
        public bool ClipEventConstruct { get; set; }
        public bool ClipEventKeyPress { get; set; }
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
