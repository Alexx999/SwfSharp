using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineFontNameTag : SwfTag
    {
        public ushort FontID { get; set; }
        public string FontName { get; set; }
        public string FontCopyright { get; set; }

        public DefineFontNameTag(int size)
            : base(TagType.DefineFontName, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            FontName = reader.ReadString();
            FontCopyright = reader.ReadString();
        }
    }
}
