using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineButtonTag : SwfTag
    {
        public ushort ButtonId { get; set; }
        public IList<ButtonRecordStruct> Characters { get; set; }
        public IList<ActionRecordStruct> Actions { get; set; } 

        public DefineButtonTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            Characters = new List<ButtonRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Characters.Add(ButtonRecordStruct.CreateFromStream(reader, TagType, swfVersion));
                nextFlag = reader.ReadUI8();
            }
            Actions = new List<ActionRecordStruct>();
            nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionRecordStruct.CreateFromStream(reader));
                nextFlag = reader.ReadUI8();
            }
        }
    }
}
