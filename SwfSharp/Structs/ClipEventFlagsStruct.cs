using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ClipEventKeyDown = reader.ReadBoolBit();
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
            reader.ReadUI8();
        }

        internal static ClipEventFlagsStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipEventFlagsStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }
    }
}
