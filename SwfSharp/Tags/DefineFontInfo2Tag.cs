using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineFontInfo2Tag : DefineFontInfoTag
    {
        public byte LanguageCode { get; set; }

        public DefineFontInfo2Tag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void ReadCodeTable(BitReader reader, int charSize)
        {
            LanguageCode = reader.ReadUI8();
            var nGlyphs = (int)reader.GetTagRemaining() / 2;
            CodeTable = new List<ushort>(nGlyphs);
            for (int i = 0; i < nGlyphs; i++)
            {
                CodeTable.Add(reader.ReadUI16());
            }
        }
    }
}
