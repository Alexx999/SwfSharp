using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFontTag : SwfTag
    {
        [XmlAttribute]
        public ushort FontID { get; set; }
        public List<ShapeStruct> GlyphShapeTable { get; set; }

        public DefineFontTag() : this(0)
        {
        }

        public DefineFontTag(int size)
            : base(TagType.DefineFont, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            var firstOffset = reader.ReadUI16();
            var nGlyphs = firstOffset/2;
            reader.ReadBytes((nGlyphs-1)*2);
            GlyphShapeTable = new List<ShapeStruct>(nGlyphs);
            for (int i = 0; i < nGlyphs; i++)
            {
                GlyphShapeTable.Add(ShapeStruct.CreateFromStream(reader, TagType));
            }
        }

        private ushort[] WriteGlyphData(BitWriter writer, int numGlyphs)
        {
            var result = new ushort[numGlyphs];
            var startPos = writer.Position;
            for (int i = 0; i < numGlyphs; i++)
            {
                result[i] = (ushort)(writer.Position - startPos);
                GlyphShapeTable[i].ToStream(writer, TagType);
                writer.Align();
            }
            return result;
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var numGlyphs = GlyphShapeTable.Count;
            var ms = new MemoryStream();
            ushort[] offsets;
            using (var glyphWriter = new BitWriter(ms, true))
            {
                offsets = WriteGlyphData(glyphWriter, numGlyphs);
            }
            writer.WriteUI16(FontID);
            var offsetSize = numGlyphs * 2;
            foreach (var offset in offsets)
            {
                writer.WriteUI16((ushort) (offset + offsetSize));
            }
            writer.WriteBytes(ms.GetBuffer(), 0, (int) ms.Position);
        }
    }
}
