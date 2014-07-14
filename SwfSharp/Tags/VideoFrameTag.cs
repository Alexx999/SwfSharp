﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class VideoFrameTag : SwfTag
    {
        public ushort StreamID { get; set; }
        public ushort FrameNum { get; set; }
        public byte[] VideoData { get; set; }

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
            throw new NotImplementedException();
        }
    }
}
