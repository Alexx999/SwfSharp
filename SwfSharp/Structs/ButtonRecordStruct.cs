using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ButtonRecordStruct
    {
        public bool ButtonHasBlendMode { get; set; }
        public bool ButtonHasFilterList { get; set; }
        public bool ButtonStateHitTest { get; set; }
        public bool ButtonStateDown { get; set; }
        public bool ButtonStateOver { get; set; }
        public bool ButtonStateUp { get; set; }
        public ushort CharacterID { get; set; }
        public ushort PlaceDepth { get; set; }
        public MatrixStruct PlaceMatrix { get; set; }
        public CXformWithAlphaStruct ColorTransform { get; set; }
        public FilterListStruct FilterList { get; set; }
        public BlendMode BlendMode { get; set; }

        private void FromStream(BitReader reader, TagType type, byte swfVersion)
        {
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
    }
}
