using System;

namespace SwfSharp.Tags
{
    [Serializable]
    public class SoundStreamHead2Tag : SoundStreamHeadTag
    {
        public SoundStreamHead2Tag() : this(0)
        {
        }

        public SoundStreamHead2Tag(int size)
            : base(TagType.SoundStreamHead2, size)
        {
        }
    }
}
