using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ClipActionRecordStruct
    {
        public ClipEventFlagsStruct EventFlags { get; set; }
        public byte KeyCode { get; set; }
        public List<ActionRecordStruct> Actions { get; set; } 

        private void FromStream(BitReader reader, byte swfVersion)
        {
            EventFlags = ClipEventFlagsStruct.CreateFromStream(reader, swfVersion);
            var actionRecordSize = reader.ReadUI32();
            var startPos = reader.Position;
            if (EventFlags.ClipEventKeyPress)
            {
                KeyCode = reader.ReadUI8();
            }
            Actions = new List<ActionRecordStruct>();

            while (reader.Position < startPos + actionRecordSize)
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

        internal void ToStream(BitWriter writer, byte swfVersion)
        {
            EventFlags.ToStream(writer, swfVersion);
            var ms = GetDataStream();
            writer.WriteUI32((uint) ms.Length);
            writer.WriteBytes(ms.GetBuffer(), 0, (int) ms.Position);
        }

        private MemoryStream GetDataStream()
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                if (EventFlags.ClipEventKeyPress)
                {
                    writer.WriteUI8(KeyCode);
                }

                foreach (var action in Actions)
                {
                    action.ToStream(writer);
                }
            }
            return result;
        }
    }
}
