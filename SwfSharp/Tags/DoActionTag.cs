using System.Collections.Generic;
using System.IO;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DoActionTag : SwfTag
    {
        public IList<ActionRecordStruct> Actions { get; set; }

        public DoActionTag(int size)
            : base(TagType.DoAction, size)
        {
        }

        protected DoActionTag(TagType tagType, int size)
            : base(tagType, size)
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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            foreach (var action in Actions)
            {
                action.ToStream(writer);
            }
            writer.WriteUI8(0);
        }
    }
}
