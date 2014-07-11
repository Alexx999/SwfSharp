using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Annotations;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    internal static class TagFactory
    {
        private const ushort SizeMask = 0xFFFF >> 10;

        [NotNull]
        public static SwfTag ReadTag([NotNull]BitReader reader)
        {
            var tagCodeAndLength = reader.ReadUI16();
            var type = (TagType)(tagCodeAndLength >> 6);
            var size = tagCodeAndLength & SizeMask;
            if (size == SizeMask)
            {
                size = reader.ReadSI32();
            }
            var tag = GetTag(type, size);
            tag.FromStream(reader);
            return tag;
        }

        [NotNull]
        private static SwfTag GetTag(TagType type, int size)
        {
            switch (type)
            {
                case TagType.End:
                {
                    return new EndTag(type, size);
                }
                case TagType.FileAttributes:
                {
                    return new FileAttributesTag(type, size);
                }
                case TagType.Metadata:
                {
                    return new MetadataTag(type, size);
                }
                default:
                {
                    return new UnknownTag(type, size);
                }
            }
        }
    }
}
