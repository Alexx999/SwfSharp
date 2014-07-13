using System.Collections.Generic;
using System.IO;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DoInitActionTag : DoActionTag
    {
        public ushort SpriteID { get; set; }

        public DoInitActionTag(int size)
            : base(TagType.DoInitAction, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SpriteID = reader.ReadUI16();
            base.FromStream(reader, swfVersion);
        }
    }
}
