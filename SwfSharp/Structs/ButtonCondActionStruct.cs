﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    class ButtonCondActionStruct
    {
        public ushort CondActionSize { get; set; }
        public bool CondIdleToOverDown { get; set; }
        public bool CondOutDownToIdle { get; set; }
        public bool CondOutDownToOverDown { get; set; }
        public bool CondOverDownToOutDown { get; set; }
        public bool CondOverDownToOverUp { get; set; }
        public bool CondOverUpToOverDown { get; set; }
        public bool CondOverUpToIdle { get; set; }
        public bool CondIdleToOverUp { get; set; }
        public byte CondKeyPress { get; set; }
        public bool CondOverDownToIdle { get; set; }
        public IList<ActionRecordStruct> Actions { get; set; }

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
            Actions = new List<ActionRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionRecordStruct.CreateFromStream(reader));
                nextFlag = reader.ReadUI8();
            }
        }

        internal static ButtonCondActionStruct CreateFromStream(BitReader reader)
        {
            var result = new ButtonCondActionStruct();

            result.FromStream(reader);

            return result;
        }
    }
}