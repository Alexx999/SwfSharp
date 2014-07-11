using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class FrameLabelTag : SwfTag
    {
        public string Name { get; set; }
        public bool IsNamedAnchor { get; set; }

        public FrameLabelTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader)
        {
            var ms = new MemoryStream(Size);
            byte b;
            while ((b = reader.ReadUI8()) != 0)
            {
                ms.WriteByte(b);
            }
            Name = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int) ms.Position);

            if (Size - ms.Position > 1)
            {
                IsNamedAnchor = reader.ReadUI8() != 0;
            }
        }
    }
}
