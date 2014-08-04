using System;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineShape3Tag : DefineShape2Tag
    {
        public DefineShape3Tag() : this(0)
        {
        }

        public DefineShape3Tag(int size)
            : base(TagType.DefineShape3, size)
        {
        }

        protected DefineShape3Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }
    }
}
