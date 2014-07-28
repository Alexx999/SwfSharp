using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ButtonRecordStruct
    {
        [XmlAttribute]
        public bool ButtonHasBlendMode { get; set; }
        [XmlAttribute]
        public bool ButtonHasFilterList { get; set; }
        [XmlAttribute]
        public bool ButtonStateHitTest { get; set; }
        [XmlAttribute]
        public bool ButtonStateDown { get; set; }
        [XmlAttribute]
        public bool ButtonStateOver { get; set; }
        [XmlAttribute]
        public bool ButtonStateUp { get; set; }
        [XmlAttribute]
        public ushort CharacterID { get; set; }
        [XmlAttribute]
        public ushort PlaceDepth { get; set; }
        public MatrixStruct PlaceMatrix { get; set; }
        public CXformWithAlphaStruct ColorTransform { get; set; }
        public FilterListStruct FilterList { get; set; }
        public BlendMode BlendMode { get; set; }

        public ButtonRecordStruct()
        {
            BlendMode = BlendMode.Normal;
        }

        private void FromStream(BitReader reader, TagType type, byte swfVersion)
        {
            reader.Align();
            if (swfVersion >= 8)
            {
                reader.ReadBits(2);
                ButtonHasBlendMode = reader.ReadBoolBit();
                ButtonHasFilterList = reader.ReadBoolBit();
            }
            else
            {
                reader.ReadBits(4);
            }
            ButtonStateHitTest = reader.ReadBoolBit();
            ButtonStateDown = reader.ReadBoolBit();
            ButtonStateOver = reader.ReadBoolBit();
            ButtonStateUp = reader.ReadBoolBit();
            CharacterID = reader.ReadUI16();
            PlaceDepth = reader.ReadUI16();
            PlaceMatrix = MatrixStruct.CreateFromStream(reader);

            if(type < TagType.DefineButton2) return;

            ColorTransform = CXformWithAlphaStruct.CreateFromStream(reader);
            if (ButtonHasFilterList)
            {
                FilterList = FilterListStruct.CreateFromStream(reader);
            }
            if (ButtonHasBlendMode)
            {
                BlendMode = BlendModeHelper.GetBlendMode(reader.ReadUI8());
            }
        }

        internal static ButtonRecordStruct CreateFromStream(BitReader reader, TagType type, byte swfVersion)
        {
            var result = new ButtonRecordStruct();

            result.FromStream(reader, type, swfVersion);

            return result;
        }

        internal void ToStream(BitWriter writer, TagType type, byte swfVersion)
        {
            writer.Align();
            if (swfVersion >= 8)
            {
                writer.WriteBits(2, 0);
                writer.WriteBoolBit(ButtonHasBlendMode);
                writer.WriteBoolBit(ButtonHasFilterList);
            }
            else
            {
                writer.WriteBits(4, 0);
            }
            writer.WriteBoolBit(ButtonStateHitTest);
            writer.WriteBoolBit(ButtonStateDown);
            writer.WriteBoolBit(ButtonStateOver);
            writer.WriteBoolBit(ButtonStateUp);
            writer.WriteUI16(CharacterID);
            writer.WriteUI16(PlaceDepth);
            PlaceMatrix.ToStream(writer);

            if (type < TagType.DefineButton2) return;

            ColorTransform.ToStream(writer);
            if (ButtonHasFilterList)
            {
                FilterList.ToStream(writer);
            }
            if (ButtonHasBlendMode)
            {
                writer.WriteUI8((byte) BlendMode);
            }
        }
    }
}
