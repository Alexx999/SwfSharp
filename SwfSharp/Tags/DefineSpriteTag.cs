using System;
using System.Collections.Generic;
using System.IO;
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

        public DefineSpriteTag() : this(0)
        {
        }

        public DefineSpriteTag(int size)
            : base(TagType.DefineSprite, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SpriteID = reader.ReadUI16();
            FrameCount = reader.ReadUI16();
            ControlTags = new List<SwfTag>();
            SwfTag tag;
            do
            {
                tag = TagFactory.ReadTag(reader, swfVersion);
                ControlTags.Add(tag);
            } while (tag.TagType != TagType.End);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(SpriteID);
            writer.WriteUI16(FrameCount);
            var ms = new MemoryStream();
            foreach (var tag in ControlTags)
            {
                TagFactory.WriteTag(writer, tag, swfVersion, ms);
            }
        }
    }
}
