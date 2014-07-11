using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public abstract class SwfTag
    {
        protected SwfTag(TagType tagType, int size)
        {
            TagType = tagType;
            Size = size;
        }

        public TagType TagType { get; set; }

        protected int Size { get; set; }
        internal abstract void FromStream(BitReader reader, byte swfVersion);
    }
}
