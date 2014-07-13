using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineButton2Tag : SwfTag
    {
        public ushort ButtonId { get; set; }
        public bool TrackAsMenu { get; set; }
        public IList<ButtonRecordStruct> Characters { get; set; }
        public IList<ButtonCondActionStruct> Actions { get; set; }

        public DefineButton2Tag(int size)
            : base(TagType.DefineButton2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            reader.ReadBits(7);
            TrackAsMenu = reader.ReadBoolBit();
            var actionOffset = reader.ReadUI16();
            Characters = new List<ButtonRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Characters.Add(ButtonRecordStruct.CreateFromStream(reader, TagType, swfVersion));
                nextFlag = reader.ReadUI8();
            }
            if(actionOffset == 0) return;
            Actions = new List<ButtonCondActionStruct>();
            ButtonCondActionStruct lastAction;
            do
            {
                lastAction = ButtonCondActionStruct.CreateFromStream(reader);
                Actions.Add(lastAction);

            } while (lastAction.CondActionSize > 0);
        }
    }
}
