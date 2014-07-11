using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public abstract class SwfTag
    {
        public TagType TagType { get; set; }

        protected uint Size { get; set; }

        internal abstract void FromStream(BitReader reader, int size);
    }
}
