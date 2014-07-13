using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class VideoFrameTag : SwfTag
    {
        public ushort StreamID { get; set; }
        public ushort FrameNum { get; set; }
        public byte[] VideoData { get; set; }

        public VideoFrameTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            StreamID = reader.ReadUI16();
            FrameNum = reader.ReadUI16();
            VideoData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }
    }
}
