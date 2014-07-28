using System;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class SetBackgroundColorTag : SwfTag
    {
        public RgbStruct BackgroundColor { get; set; }

        public SetBackgroundColorTag() : this(0)
        {
        }

        public SetBackgroundColorTag(int size)
            : base(TagType.SetBackgroundColor, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            BackgroundColor = RgbStruct.CreateFromStream(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            BackgroundColor.ToStream(writer);
        }
    }
}
