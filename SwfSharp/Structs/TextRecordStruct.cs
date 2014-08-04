using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class TextRecordStruct
    {
        private ushort? _fontID;
        private short? _xOffset;
        private short? _yOffset;
        private ushort? _textHeight;

        [XmlAttribute]
        public byte TextRecordType { get; set; }

        [XmlAttribute]
        public ushort FontID
        {
            get { return _fontID.GetValueOrDefault(); }
            set { _fontID = value; }
        }

        [XmlIgnore]
        public bool FontIDSpecified
        {
            get { return _fontID.HasValue; }
        }

        [XmlElement]
        public RgbaStruct TextColor { get; set; }

        [XmlAttribute]
        public short XOffset
        {
            get { return _xOffset.GetValueOrDefault(); }
            set { _xOffset = value; }
        }

        [XmlIgnore]
        public bool XOffsetSpecified
        {
            get { return _xOffset.HasValue; }
        }

        [XmlAttribute]
        public short YOffset
        {
            get { return _yOffset.GetValueOrDefault(); }
            set { _yOffset = value; }
        }

        [XmlIgnore]
        public bool YOffsetSpecified
        {
            get { return _yOffset.HasValue; }
        }

        [XmlAttribute]
        public ushort TextHeight
        {
            get { return _textHeight.GetValueOrDefault(); }
            set { _textHeight = value; }
        }

        [XmlIgnore]
        public bool TextHeightSpecified
        {
            get { return _textHeight.HasValue; }
        }

        [XmlArrayItem("GlyphEntry")]
        public List<GlyphEntryStruct> GlyphEntries { get; set; }

        private void FromStream(BitReader reader, TagType type, byte glyphBits, byte advanceBits)
        {
            reader.Align();
            TextRecordType = (byte) reader.ReadBits(1);
            reader.ReadBits(3);
            var styleFlagsHasFont = reader.ReadBoolBit();
            var styleFlagsHasColor = reader.ReadBoolBit();
            var styleFlagsHasYOffset = reader.ReadBoolBit();
            var styleFlagsHasXOffset = reader.ReadBoolBit();
            if (TextRecordType == 0)
            {
                return;
            }

            if (styleFlagsHasFont)
            {
                _fontID = reader.ReadUI16();
            }
            if (styleFlagsHasColor)
            {
                TextColor = type == TagType.DefineText2 
                    ? RgbaStruct.CreateFromStream(reader) 
                    : RgbaStruct.CreateFromRgbStream(reader);
            }
            if (styleFlagsHasXOffset)
            {
                _xOffset = reader.ReadSI16();
            }
            if (styleFlagsHasYOffset)
            {
                _yOffset = reader.ReadSI16();
            }
            if (styleFlagsHasFont)
            {
                _textHeight = reader.ReadUI16();
            }
            var glyphCount = reader.ReadUI8();
            GlyphEntries = new List<GlyphEntryStruct>(glyphCount);
            for (int i = 0; i < glyphCount; i++)
            {
                GlyphEntries.Add(GlyphEntryStruct.CreateFromStream(reader, glyphBits, advanceBits));
            }
        }

        internal static TextRecordStruct CreateFromStream(BitReader reader, TagType type, byte glyphBits, byte advanceBits)
        {
            var result = new TextRecordStruct();

            result.FromStream(reader, type, glyphBits, advanceBits);

            return result;
        }

        internal void ToStream(BitWriter writer, TagType type, byte glyphBits, byte advanceBits)
        {
            var styleFlagsHasFont = _fontID.HasValue && _textHeight.HasValue;
            var styleFlagsHasColor = TextColor != null;
            var styleFlagsHasYOffset = _yOffset.HasValue;
            var styleFlagsHasXOffset = _xOffset.HasValue;

            writer.Align();
            writer.WriteBits(1, 1);
            writer.WriteBits(3, 0);
            writer.WriteBoolBit(styleFlagsHasFont);
            writer.WriteBoolBit(styleFlagsHasColor);
            writer.WriteBoolBit(styleFlagsHasYOffset);
            writer.WriteBoolBit(styleFlagsHasXOffset);

            if (styleFlagsHasFont)
            {
                writer.WriteUI16(_fontID.Value);
            }
            if (styleFlagsHasColor)
            {
                if (type == TagType.DefineText2)
                {
                    TextColor.ToStream(writer);
                }
                else
                {
                    TextColor.ToRgbStream(writer);
                }
            }
            if (styleFlagsHasXOffset)
            {
                writer.WriteSI16(_xOffset.Value);
            }
            if (styleFlagsHasYOffset)
            {
                writer.WriteSI16(_yOffset.Value);
            }
            if (styleFlagsHasFont)
            {
                writer.WriteUI16(_textHeight.Value);
            }
            writer.WriteUI8((byte) GlyphEntries.Count);

            foreach (var glyphEntry in GlyphEntries)
            {
                glyphEntry.ToStream(writer, glyphBits, advanceBits);
            }
        }
    }
}
