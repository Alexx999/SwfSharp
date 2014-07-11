using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class SetBackgroundColorTag : SwfTag
    {
        public RgbStruct BackgroundColor { get; set; }

        public SetBackgroundColorTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            BackgroundColor = RgbStruct.CreateFromStream(reader);
        }
    }
}
