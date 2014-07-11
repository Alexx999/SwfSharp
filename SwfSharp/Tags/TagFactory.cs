using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    internal static class TagFactory
    {
        private const ushort SizeMask = 0xFFFF >> 10;

        public static SwfTag ReadTag(BitReader reader)
        {
            var tagCodeAndLength = reader.ReadUI16();
            var type = (TagType)(tagCodeAndLength >> 6);
            var size = tagCodeAndLength & SizeMask;
            if (size == SizeMask)
            {
                size = reader.ReadSI32();
            }
            var tag = GetTagForType(type);
            tag.FromStream(reader, size);
            return tag;
        }

        private static SwfTag GetTagForType(TagType type)
        {
            switch (type)
            {
                case TagType.End:
                {
                    return new EndTag();
                }
                case TagType.FileAttributes:
                {
                    return new FileAttributesTag();
                }
                default:
                {
                    return new UnknownTag();
                }
            }
        }
    }
}
