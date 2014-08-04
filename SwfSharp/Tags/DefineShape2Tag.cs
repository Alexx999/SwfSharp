using System;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineShape2Tag : DefineShapeTag
    {
        public DefineShape2Tag() : this(0)
        {
        }

        public DefineShape2Tag(int size)
            : base(TagType.DefineShape2, size)
        {
        }

        protected DefineShape2Tag(TagType tagType, int size)
            : base(tagType, size)
        {
        }
    }
}
