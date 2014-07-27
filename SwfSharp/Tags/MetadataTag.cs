using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class MetadataTag : SwfTag
    {
        public string Metadata { get; set; }

        public MetadataTag() : this(0)
        {
        }

        public MetadataTag(int size)
            : base(TagType.Metadata, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Metadata = reader.ReadString(Size - 1);
            reader.ReadUI8();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteStringBytes(Metadata, swfVersion);
            writer.WriteUI8(0);
        }
    }
}
