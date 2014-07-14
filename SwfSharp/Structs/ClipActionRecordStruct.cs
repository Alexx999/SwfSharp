﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ClipActionRecordStruct
    {
        public ClipEventFlagsStruct EventFlags { get; set; }
        public uint ActionRecordSize { get; set; }
        public byte KeyCode { get; set; }
        public IList<ActionRecordStruct> Actions { get; set; } 

        private void FromStream(BitReader reader, byte swfVersion)
        {
            EventFlags = ClipEventFlagsStruct.CreateFromStream(reader, swfVersion);
            ActionRecordSize = reader.ReadUI32();
            var startPos = reader.Position;
            if (EventFlags.ClipEventKeyPress)
            {
                KeyCode = reader.ReadUI8();
            }
            Actions = new List<ActionRecordStruct>();

            while (reader.Position < startPos + ActionRecordSize)
            {
                var record = ActionRecordStruct.CreateFromStream(reader);
                Actions.Add(record);
            }
        }

        internal static ClipActionRecordStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipActionRecordStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }
    }
}
