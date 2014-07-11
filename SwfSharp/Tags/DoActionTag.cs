﻿using System.Collections.Generic;
using System.IO;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DoActionTag : SwfTag
    {
        public IList<ActionRecordStruct> Actions { get; set; } 

        public DoActionTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Actions = new List<ActionRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionRecordStruct.CreateFromStream(reader));
                nextFlag = reader.ReadUI8();
            }
        }
    }
}
