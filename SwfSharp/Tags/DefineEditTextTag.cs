using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineEditTextTag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public RectStruct Bounds { get; set; }
        public bool HasText { get; set; }
        public bool WordWrap { get; set; }
        public bool Multiline { get; set; }
        public bool Password { get; set; }
        public bool ReadOnly { get; set; }
        public bool HasTextColor { get; set; }
        public bool HasMaxLength { get; set; }
        public bool HasFont { get; set; }
        public bool HasFontClass { get; set; }
        public bool AutoSize { get; set; }
        public bool HasLayout { get; set; }
        public bool NoSelect { get; set; }
        public bool Border { get; set; }
        public bool WasStatic { get; set; }
        public bool HTML { get; set; }
        public bool UseOutlines { get; set; }
        public ushort FontID { get; set; }
        public string FontClass { get; set; }
        public ushort FontHeight { get; set; }
        public RgbaStruct TextColor { get; set; }
        public ushort MaxLength { get; set; }
        public AlignMode Align { get; set; }
        public ushort LeftMargin { get; set; }
        public ushort RightMargin { get; set; }
        public ushort Indent { get; set; }
        public short Leading { get; set; }
        public string VariableName { get; set; }
        public string InitialText { get; set; }

        public DefineEditTextTag(int size)
            : base(TagType.DefineEditText, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            Bounds = RectStruct.CreateFromStream(reader);
            reader.Align();
            HasText = reader.ReadBoolBit();
            WordWrap = reader.ReadBoolBit();
            Multiline = reader.ReadBoolBit();
            Password = reader.ReadBoolBit();
            ReadOnly = reader.ReadBoolBit();
            HasTextColor = reader.ReadBoolBit();
            HasMaxLength = reader.ReadBoolBit();
            HasFont = reader.ReadBoolBit();
            HasFontClass = reader.ReadBoolBit();
            AutoSize = reader.ReadBoolBit();
            HasLayout = reader.ReadBoolBit();
            NoSelect = reader.ReadBoolBit();
            Border = reader.ReadBoolBit();
            WasStatic = reader.ReadBoolBit();
            HTML = reader.ReadBoolBit();
            UseOutlines = reader.ReadBoolBit();
            if (HasFont)
            {
                FontID = reader.ReadUI16();
            }
            if (HasFontClass)
            {
                FontClass = reader.ReadString();
            }
            if (HasFont || HasFontClass)
            {
                FontHeight = reader.ReadUI16();
            }
            if (HasTextColor)
            {
                TextColor = RgbaStruct.CreateFromStream(reader);
            }
            if (HasMaxLength)
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
            if (HasText)
            {
                InitialText = reader.ReadString();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            Bounds.ToStream(writer);
            writer.Align();
            writer.WriteBoolBit(HasText);
            writer.WriteBoolBit(WordWrap);
            writer.WriteBoolBit(Multiline);
            writer.WriteBoolBit(Password);
            writer.WriteBoolBit(ReadOnly);
            writer.WriteBoolBit(HasTextColor);
            writer.WriteBoolBit(HasMaxLength);
            writer.WriteBoolBit(HasFont);
            writer.WriteBoolBit(HasFontClass);
            writer.WriteBoolBit(AutoSize);
            writer.WriteBoolBit(HasLayout);
            writer.WriteBoolBit(NoSelect);
            writer.WriteBoolBit(Border);
            writer.WriteBoolBit(WasStatic);
            writer.WriteBoolBit(HTML);
            writer.WriteBoolBit(UseOutlines);
            if (HasFont)
            {
                writer.WriteUI16(FontID);
            }
            if (HasFontClass)
            {
                writer.WriteString(FontClass, swfVersion);
            }
            if (HasFont || HasFontClass)
            {
                writer.WriteUI16(FontHeight);
            }
            if (HasTextColor)
            {
                TextColor.ToStream(writer);
            }
            if (HasMaxLength)
            {
                writer.WriteUI16(MaxLength);
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
            if (HasText)
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
