using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineSpriteTag : SwfTag
    {
        public ushort SpriteID { get; set; }
        public ushort FrameCount { get; set; }
        public IList<SwfTag> ControlTags { get; set; }

        public DefineSpriteTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            SpriteID = reader.ReadUI16();
            FrameCount = reader.ReadUI16();
            ControlTags = new List<SwfTag>();
            SwfTag tag;
            do
            {
                tag = TagFactory.ReadTag(reader);
                ControlTags.Add(tag);
            } while (tag.TagType != TagType.End);
        }
    }
}
