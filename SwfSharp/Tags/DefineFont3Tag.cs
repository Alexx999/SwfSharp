using System;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFont3Tag : DefineFont2Tag
    {
        public DefineFont3Tag() : this(0)
        {
        }

        public DefineFont3Tag(int size)
            : base(TagType.DefineFont3, size)
        {
        }
    }
}
