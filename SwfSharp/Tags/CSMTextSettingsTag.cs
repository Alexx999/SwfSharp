using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class CSMTextSettingsTag : SwfTag
    {
        public ushort TextID { get; set; }
        public TextRenderType  UseFlashType { get; set; }
        public TextGridFit GridFit { get; set; }
        public float Thickness { get; set; }
        public float Sharpness { get; set; }

        public CSMTextSettingsTag(TagType tagType, int size) : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            TextID = reader.ReadUI16();
            UseFlashType = (TextRenderType) reader.ReadBits(2);
            GridFit = (TextGridFit) reader.ReadBits(3);
            reader.ReadBits(3);
            Thickness = reader.ReadFloat();
            Sharpness = reader.ReadFloat();
            reader.ReadUI8();
        }

        public enum TextRenderType
        {
            Normal = 0,
            Advanced = 1
        }

        public enum TextGridFit
        {
            None = 0,
            Pixel = 1,
            Subpixel = 2
        }
    }
}
