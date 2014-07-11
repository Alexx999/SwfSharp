using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class FileAttributesTag : SwfTag
    {
        public FileAttributesTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        public bool UseDirectBlit { get; set; }
        public bool UseGPU { get; set; }
        public bool HasMetadata { get; set; }
        public bool ActionScript3 { get; set; }
        public bool UseNetwork { get; set; }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadBoolBit();
            UseDirectBlit = reader.ReadBoolBit();
            UseGPU = reader.ReadBoolBit();
            HasMetadata = reader.ReadBoolBit();
            ActionScript3 = reader.ReadBoolBit();
            reader.ReadBits(2);
            UseNetwork = reader.ReadBoolBit();
            reader.ReadUI24();
        }
    }
}
