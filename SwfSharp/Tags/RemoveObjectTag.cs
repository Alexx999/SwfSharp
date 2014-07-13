using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class RemoveObjectTag : RemoveObject2Tag
    {
        public ushort CharacterId { get; set; }

        public RemoveObjectTag(int size)
            : base(TagType.RemoveObject, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }
    }
}
