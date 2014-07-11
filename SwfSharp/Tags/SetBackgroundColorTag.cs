using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class SetBackgroundColorTag : SwfTag
    {
        public SwfRgbStruct BackgroundColor { get; set; }

        public SetBackgroundColorTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            BackgroundColor = SwfRgbStruct.CreateFromStream(reader);
        }
    }
}
