using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class VideoFrameTag : SwfTag
    {
        [XmlAttribute]
        public ushort StreamID { get; set; }
        [XmlAttribute]
        public ushort FrameNum { get; set; }
        public byte[] VideoData { get; set; }

        public VideoFrameTag() : this(0)
        {
        }

        public VideoFrameTag(int size)
            : base(TagType.VideoFrame, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            StreamID = reader.ReadUI16();
            FrameNum = reader.ReadUI16();
            VideoData = reader.ReadBytes((int) reader.TagBytesRemaining);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(StreamID);
            writer.WriteUI16(FrameNum);
            writer.WriteBytes(VideoData);
        }
    }
}
