using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineFontTag : SwfTag
    {
        public ushort FontID { get; set; }
        public IList<ushort> OffsetTable { get; set; }
        public IList<ShapeStruct> GlyphShapeTable { get; set; } 

        public DefineFontTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            var firstOffset = reader.ReadUI16();
            var nGlyphs = firstOffset/2;
            OffsetTable = new List<ushort>(nGlyphs) {firstOffset};
            for (int i = 1; i < nGlyphs; i++)
            {
                OffsetTable.Add(reader.ReadUI16());
            }
            GlyphShapeTable = new List<ShapeStruct>(nGlyphs);
            for (int i = 0; i < nGlyphs; i++)
            {
                GlyphShapeTable.Add(ShapeStruct.CreateFromStream(reader, TagType));
            }
        }
    }
}
