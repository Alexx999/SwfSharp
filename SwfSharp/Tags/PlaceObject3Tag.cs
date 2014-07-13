using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class PlaceObject3Tag : PlaceObject2Tag
    {
        public bool PlaceFlagOpaqueBackground { get; set; }
        public bool PlaceFlagHasVisible { get; set; }
        public bool PlaceFlagHasImage { get; set; }
        public bool PlaceFlagHasClassName { get; set; }
        public bool PlaceFlagHasCacheAsBitmap { get; set; }
        public bool PlaceFlagHasBlendMode { get; set; }
        public bool PlaceFlagHasFilterList { get; set; }
        public string ClassName { get; set; }
        public FilterListStruct SurfaceFilterList { get; set; }
        public BlendMode BlendMode { get; set; }
        public byte BitmapCache { get; set; }
        public byte Visible { get; set; }
        public RgbaStruct BackgroundColor { get; set; }

        public PlaceObject3Tag(int size)
            : base(TagType.PlaceObject3, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            PlaceFlagHasClipActions = reader.ReadBoolBit();
            PlaceFlagHasClipDepth = reader.ReadBoolBit();
            PlaceFlagHasName = reader.ReadBoolBit();
            PlaceFlagHasRatio = reader.ReadBoolBit();
            PlaceFlagHasColorTransform = reader.ReadBoolBit();
            PlaceFlagHasMatrix = reader.ReadBoolBit();
            PlaceFlagHasCharacter = reader.ReadBoolBit();
            PlaceFlagMove = reader.ReadBoolBit();
            reader.ReadBits(1);
            PlaceFlagOpaqueBackground = reader.ReadBoolBit();
            PlaceFlagHasVisible = reader.ReadBoolBit();
            PlaceFlagHasImage = reader.ReadBoolBit();
            PlaceFlagHasClassName = reader.ReadBoolBit();
            PlaceFlagHasCacheAsBitmap = reader.ReadBoolBit();
            PlaceFlagHasBlendMode = reader.ReadBoolBit();
            PlaceFlagHasFilterList = reader.ReadBoolBit();
            Depth = reader.ReadUI16();
            if (PlaceFlagHasClassName)
            {
                ClassName = reader.ReadString();
            }
            if (PlaceFlagHasCharacter)
            {
                CharacterId = reader.ReadUI16();
            }
            if (PlaceFlagHasMatrix)
            {
                Matrix = MatrixStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasColorTransform)
            {
                ColorTransform = CXformWithAlphaStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasRatio)
            {
                Ratio = reader.ReadUI16();
            }
            if (PlaceFlagHasName)
            {
                Name = reader.ReadString();
            }
            if (PlaceFlagHasClipDepth)
            {
                ClipDepth = reader.ReadUI16();
            }
            if (PlaceFlagHasFilterList)
            {
                SurfaceFilterList = FilterListStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasBlendMode)
            {
                BlendMode = (BlendMode) reader.ReadUI8();
            }
            if (PlaceFlagHasCacheAsBitmap)
            {
                BitmapCache = reader.ReadUI8();
            }
            if (PlaceFlagHasVisible)
            {
                Visible = reader.ReadUI8();
            }
            if (PlaceFlagOpaqueBackground)
            {
                BackgroundColor = RgbaStruct.CreateFromStream(reader);
            }
            if (PlaceFlagHasClipActions)
            {
                ClipActions = ClipActionsStruct.CreateFromStream(reader, swfVersion);
            }
        }
    }
}
