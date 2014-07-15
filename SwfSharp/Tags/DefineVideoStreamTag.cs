using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineVideoStreamTag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public ushort NumFrames { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public DeblockingMode VideoFlagsDeblocking { get; set; }
        public bool VideoFlagsSmoothing { get; set; }
        public Codec CodecID { get; set; }

        public DefineVideoStreamTag(int size)
            : base(TagType.DefineVideoStream, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            NumFrames = reader.ReadUI16();
            Width = reader.ReadUI16();
            Height = reader.ReadUI16();
            reader.ReadBits(4);
            VideoFlagsDeblocking = (DeblockingMode) reader.ReadBits(3);
            VideoFlagsSmoothing = reader.ReadBoolBit();
            CodecID = (Codec) reader.ReadUI8();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteUI16(NumFrames);
            writer.WriteUI16(Width);
            writer.WriteUI16(Height);
            writer.WriteBits(4, 0);
            writer.WriteBits(3, (uint) VideoFlagsDeblocking);
            writer.WriteBoolBit(VideoFlagsSmoothing);
            writer.WriteUI8((byte) CodecID);
        }

        public enum DeblockingMode
        {
            UseVideoPacket = 0,
            Off = 1,
            Level1 = 2,
            Level2 = 3,
            Level3 = 4,
            Level4 = 5
        }

        public enum Codec
        {
            Sorenson = 2,
            Screen = 3,
            VP6 = 4,
            VP6Alpha = 5
        }
    }
}
