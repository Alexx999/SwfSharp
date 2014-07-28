using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineEditTextTag : SwfTag
    {
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
        public int FontID { get; set; }
        public string FontClass { get; set; }
        public ushort FontHeight { get; set; }
        public RgbaStruct TextColor { get; set; }
        public int MaxLength { get; set; }
        public AlignMode Align { get; set; }
        public ushort LeftMargin { get; set; }
        public ushort RightMargin { get; set; }
        public ushort Indent { get; set; }
        public short Leading { get; set; }
        public string VariableName { get; set; }
        public string InitialText { get; set; }
        internal bool HasLayout { get; set; }

        public DefineEditTextTag() : this(0)
        {
        }

        public DefineEditTextTag(int size)
            : base(TagType.DefineEditText, size)
        {
            FontID = -1;
            MaxLength = -1;
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
            HasLayout = reader.ReadBoolBit();
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
            if (HasLayout)
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
            var hasMaxLength = MaxLength != -1;
            var hasFont = FontID != -1;
            var hasFontClass = !string.IsNullOrEmpty(FontClass);

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
            writer.WriteBoolBit(HasLayout);
            writer.WriteBoolBit(NoSelect);
            writer.WriteBoolBit(Border);
            writer.WriteBoolBit(WasStatic);
            writer.WriteBoolBit(HTML);
            writer.WriteBoolBit(UseOutlines);
            if (hasFont)
            {
                writer.WriteUI16((ushort) FontID);
            }
            if (hasFontClass)
            {
                writer.WriteString(FontClass, swfVersion);
            }
            if (hasFont || hasFontClass)
            {
                writer.WriteUI16(FontHeight);
            }
            if (hasTextColor)
            {
                TextColor.ToStream(writer);
            }
            if (hasMaxLength)
            {
                writer.WriteUI16((ushort) MaxLength);
            }
            if (HasLayout)
            {
                writer.WriteUI8((byte)Align);
                writer.WriteUI16(LeftMargin);
                writer.WriteUI16(RightMargin);
                writer.WriteUI16(Indent);
                writer.WriteSI16(Leading);
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
