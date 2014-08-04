using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ButtonRecordStruct
    {
        private BlendMode? _blendMode;

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
        [XmlElement]
        public MatrixStruct PlaceMatrix { get; set; }
        [XmlElement]
        public CXformWithAlphaStruct ColorTransform { get; set; }
        [XmlElement]
        public FilterListStruct FilterList { get; set; }

        [XmlAttribute]
        public BlendMode BlendMode
        {
            get { return _blendMode.GetValueOrDefault(); }
            set { _blendMode = value; }
        }

        [XmlIgnore]
        public bool BlendModeSpecified
        {
            get { return _blendMode.HasValue; }
        }

        private void FromStream(BitReader reader, TagType type, byte swfVersion)
        {
            bool buttonHasBlendMode = false;
            bool buttonHasFilterList = false;
            reader.Align();
            if (swfVersion >= 8)
            {
                reader.ReadBits(2);
                buttonHasBlendMode = reader.ReadBoolBit();
                buttonHasFilterList = reader.ReadBoolBit();
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
            if (buttonHasFilterList)
            {
                FilterList = FilterListStruct.CreateFromStream(reader);
            }
            if (buttonHasBlendMode)
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
            bool buttonHasBlendMode = false;
            bool buttonHasFilterList = false;
            writer.Align();
            if (swfVersion >= 8)
            {
                buttonHasBlendMode = _blendMode.HasValue;
                buttonHasFilterList = FilterList != null;
                writer.WriteBits(2, 0);
                writer.WriteBoolBit(buttonHasBlendMode);
                writer.WriteBoolBit(buttonHasFilterList);
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
            if (buttonHasFilterList)
            {
                FilterList.ToStream(writer);
            }
            if (buttonHasBlendMode)
            {
                writer.WriteUI8((byte)_blendMode.Value);
            }
        }
    }
}
