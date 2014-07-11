using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class MetadataTag : SwfTag
    {
        public string Metadata { get; set; }

        public MetadataTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            Metadata = reader.ReadString(Size);
        }
    }
}
