using System.Collections.Generic;
using System.IO;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DoInitActionTag : SwfTag
    {
        public ushort SpriteID { get; set; }
        public IList<ActionRecordStruct> Actions { get; set; } 

        public DoInitActionTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SpriteID = reader.ReadUI16();
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
