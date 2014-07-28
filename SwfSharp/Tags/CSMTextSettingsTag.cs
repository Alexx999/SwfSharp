using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class CSMTextSettingsTag : SwfTag
    {
        [XmlAttribute]
        public ushort TextID { get; set; }
        [XmlAttribute]
        public TextRenderType  UseFlashType { get; set; }
        [XmlAttribute]
        public TextGridFit GridFit { get; set; }
        [XmlAttribute]
        public float Thickness { get; set; }
        [XmlAttribute]
        public float Sharpness { get; set; }

        public CSMTextSettingsTag() : this(0)
        {
        }

        public CSMTextSettingsTag(int size)
            : base(TagType.CSMTextSetting, size)
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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(TextID);
            writer.WriteBits(2, (uint) UseFlashType);
            writer.WriteBits(3, (uint) GridFit);
            writer.WriteBits(3, 0);
            writer.WriteFloat(Thickness);
            writer.WriteFloat(Sharpness);
            writer.WriteUI8(0);
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
