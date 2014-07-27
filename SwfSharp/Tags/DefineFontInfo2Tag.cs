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

        public DefineFontInfo2Tag() : this(0)
        {
        }

        public DefineFontInfo2Tag(int size)
            : base(TagType.DefineFontInfo2, size)
        {
        }

        internal override void ReadCodeTable(BitReader reader)
        {
            LanguageCode = reader.ReadUI8();
            var nGlyphs = (int)reader.TagBytesRemaining / 2;
            CodeTable = new List<ushort>(nGlyphs);
            for (int i = 0; i < nGlyphs; i++)
            {
                CodeTable.Add(reader.ReadUI16());
            }
        }

        internal override void WriteCodeTable(BitWriter writer)
        {
            writer.WriteUI8(LanguageCode);
            foreach (var code in CodeTable)
            {
                writer.WriteUI16(code);
            }
        }
    }
}
