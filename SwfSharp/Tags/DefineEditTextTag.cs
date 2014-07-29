using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineEditTextTag : SwfTag
    {
        private ushort? _fontID;
        private ushort? _fontHeight;
        private ushort? _maxLength;
        private AlignMode? _align;
        private ushort? _leftMargin;
        private ushort? _rightMargin;
        private ushort? _indent;
        private short? _leading;

        [XmlAttribute]
        public ushort CharacterID { get; set; }
        public RectStruct Bounds { get; set; }
        [XmlAttribute]
        public bool WordWrap { get; set; }
        [XmlAttribute]
        public bool Multiline { get; set; }
        [XmlAttribute]
        public bool Password { get; set; }
        [XmlAttribute]
        public bool ReadOnly { get; set; }
        [XmlAttribute]
        public bool AutoSize { get; set; }
        [XmlAttribute]
        public bool NoSelect { get; set; }
        [XmlAttribute]
        public bool Border { get; set; }
        [XmlAttribute]
        public bool WasStatic { get; set; }
        [XmlAttribute]
        public bool HTML { get; set; }
        [XmlAttribute]
        public bool UseOutlines { get; set; }

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

        public string FontClass { get; set; }

        [XmlAttribute]
        public ushort FontHeight
        {
            get { return _fontHeight.GetValueOrDefault(); }
            set { _fontHeight = value; }
        }

        [XmlIgnore]
        public bool FontHeightSpecified
        {
            get { return _fontHeight.HasValue; }
        }

        public RgbaStruct TextColor { get; set; }

        [XmlAttribute]
        public ushort MaxLength
        {
            get { return _maxLength.GetValueOrDefault(); }
            set { _maxLength = value; }
        }

        [XmlIgnore]
        public bool MaxLengthSpecified
        {
            get { return _maxLength.HasValue; }
        }

        [XmlAttribute]
        public AlignMode Align
        {
            get { return _align.GetValueOrDefault(); }
            set { _align = value; }
        }

        [XmlIgnore]
        public bool AlignSpecified
        {
            get { return _align.HasValue; }
        }

        [XmlAttribute]
        public ushort LeftMargin
        {
            get { return _leftMargin.GetValueOrDefault(); }
            set { _leftMargin = value; }
        }

        [XmlIgnore]
        public bool LeftMarginSpecified
        {
            get { return _leftMargin.HasValue; }
        }

        [XmlAttribute]
        public ushort RightMargin
        {
            get { return _rightMargin.GetValueOrDefault(); }
            set { _rightMargin = value; }
        }

        [XmlIgnore]
        public bool RightMarginSpecified
        {
            get { return _rightMargin.HasValue; }
        }

        [XmlAttribute]
        public ushort Indent
        {
            get { return _indent.GetValueOrDefault(); }
            set { _indent = value; }
        }

        [XmlIgnore]
        public bool IndentSpecified
        {
            get { return _indent.HasValue; }
        }

        [XmlAttribute]
        public short Leading
        {
            get { return _leading.GetValueOrDefault(); }
            set { _leading = value; }
        }

        [XmlIgnore]
        public bool LeadingSpecified
        {
            get { return _leading.HasValue; }
        }

        [XmlAttribute]
        public string VariableName { get; set; }

        [XmlText]
        public string InitialText { get; set; }

        public DefineEditTextTag() : this(0)
        {
        }

        public DefineEditTextTag(int size)
            : base(TagType.DefineEditText, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            Bounds = RectStruct.CreateFromStream(reader);
            reader.Align();
            var hasText = reader.ReadBoolBit();
            WordWrap = reader.ReadBoolBit();
            Multiline = reader.ReadBoolBit();
            Password = reader.ReadBoolBit();
            ReadOnly = reader.ReadBoolBit();
            var hasTextColor = reader.ReadBoolBit();
            var hasMaxLength = reader.ReadBoolBit();
            var hasFont = reader.ReadBoolBit();
            var hasFontClass = reader.ReadBoolBit();
            AutoSize = reader.ReadBoolBit();
            var hasLayout = reader.ReadBoolBit();
            NoSelect = reader.ReadBoolBit();
            Border = reader.ReadBoolBit();
            WasStatic = reader.ReadBoolBit();
            HTML = reader.ReadBoolBit();
            UseOutlines = reader.ReadBoolBit();
            if (hasFont)
            {
                FontID = reader.ReadUI16();
            }
            if (hasFontClass)
            {
                FontClass = reader.ReadString();
            }
            if (hasFont || hasFontClass)
            {
                FontHeight = reader.ReadUI16();
            }
            if (hasTextColor)
            {
                TextColor = RgbaStruct.CreateFromStream(reader);
            }
            if (hasMaxLength)
            {
                MaxLength = reader.ReadUI16();
            }
            if (hasLayout)
            {
                Align = (AlignMode) reader.ReadUI8();
                LeftMargin = reader.ReadUI16();
                RightMargin = reader.ReadUI16();
                Indent = reader.ReadUI16();
                Leading = reader.ReadSI16();
            }
            VariableName = reader.ReadString();
            if (hasText)
            {
                InitialText = reader.ReadString();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var hasText = !string.IsNullOrEmpty(InitialText);
            var hasTextColor = TextColor != null;
            var hasMaxLength = _maxLength.HasValue;
            var hasFont = _fontID.HasValue;
            var hasFontClass = !string.IsNullOrEmpty(FontClass);
            var hasLayout = (_align.HasValue && _leftMargin.HasValue && _rightMargin.HasValue && _indent.HasValue &&
                             _leading.HasValue);

            writer.WriteUI16(CharacterID);
            Bounds.ToStream(writer);
            writer.Align();
            writer.WriteBoolBit(hasText);
            writer.WriteBoolBit(WordWrap);
            writer.WriteBoolBit(Multiline);
            writer.WriteBoolBit(Password);
            writer.WriteBoolBit(ReadOnly);
            writer.WriteBoolBit(hasTextColor);
            writer.WriteBoolBit(hasMaxLength);
            writer.WriteBoolBit(hasFont);
            writer.WriteBoolBit(hasFontClass);
            writer.WriteBoolBit(AutoSize);
            writer.WriteBoolBit(hasLayout);
            writer.WriteBoolBit(NoSelect);
            writer.WriteBoolBit(Border);
            writer.WriteBoolBit(WasStatic);
            writer.WriteBoolBit(HTML);
            writer.WriteBoolBit(UseOutlines);
            if (hasFont)
            {
                writer.WriteUI16(_fontID.Value);
            }
            if (hasFontClass)
            {
                writer.WriteString(FontClass, swfVersion);
            }
            if (hasFont || hasFontClass)
            {
                writer.WriteUI16(_fontHeight.GetValueOrDefault());
            }
            if (hasTextColor)
            {
                TextColor.ToStream(writer);
            }
            if (hasMaxLength)
            {
                writer.WriteUI16(_maxLength.Value);
            }
            if (hasLayout)
            {
                writer.WriteUI8((byte)_align.Value);
                writer.WriteUI16(_leftMargin.Value);
                writer.WriteUI16(_rightMargin.Value);
                writer.WriteUI16(_indent.Value);
                writer.WriteSI16(_leading.Value);
            }
            writer.WriteString(VariableName, swfVersion);
            if (hasText)
            {
                writer.WriteString(InitialText, swfVersion);
            }
        }

        public enum AlignMode
        {
            Left = 0,
            Right = 1,
            Center = 2,
            Justify = 3
        }
    }
}
