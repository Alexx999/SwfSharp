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
    public class PlaceObject3Tag : PlaceObject2Tag
    {
        [XmlAttribute]
        public bool PlaceFlagOpaqueBackground { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasVisible { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasImage { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasClassName { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasCacheAsBitmap { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasBlendMode { get; set; }
        [XmlAttribute]
        public bool PlaceFlagHasFilterList { get; set; }
        public string ClassName { get; set; }
        public FilterListStruct SurfaceFilterList { get; set; }
        public BlendMode BlendMode { get; set; }
        public byte BitmapCache { get; set; }
        public byte Visible { get; set; }
        public RgbaStruct BackgroundColor { get; set; }

        public PlaceObject3Tag() : this(0)
        {
        }

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
        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteBoolBit(PlaceFlagHasClipActions);
            writer.WriteBoolBit(PlaceFlagHasClipDepth);
            writer.WriteBoolBit(PlaceFlagHasName);
            writer.WriteBoolBit(PlaceFlagHasRatio);
            writer.WriteBoolBit(PlaceFlagHasColorTransform);
            writer.WriteBoolBit(PlaceFlagHasMatrix);
            writer.WriteBoolBit(PlaceFlagHasCharacter);
            writer.WriteBoolBit(PlaceFlagMove);
            writer.WriteBits(1, 0);
            writer.WriteBoolBit(PlaceFlagOpaqueBackground);
            writer.WriteBoolBit(PlaceFlagHasVisible);
            writer.WriteBoolBit(PlaceFlagHasImage);
            writer.WriteBoolBit(PlaceFlagHasClassName);
            writer.WriteBoolBit(PlaceFlagHasCacheAsBitmap);
            writer.WriteBoolBit(PlaceFlagHasBlendMode);
            writer.WriteBoolBit(PlaceFlagHasFilterList);
            writer.WriteUI16(Depth);
            if (PlaceFlagHasClassName)
            {
                writer.WriteString(ClassName, swfVersion);
            }
            if (PlaceFlagHasCharacter)
            {
                writer.WriteUI16(CharacterId);
            }
            if (PlaceFlagHasMatrix)
            {
                Matrix.ToStream(writer);
            }
            if (PlaceFlagHasColorTransform)
            {
                ColorTransform.ToStream(writer);
            }
            if (PlaceFlagHasRatio)
            {
                writer.WriteUI16(Ratio);
            }
            if (PlaceFlagHasName)
            {
                writer.WriteString(Name, swfVersion);
            }
            if (PlaceFlagHasClipDepth)
            {
                writer.WriteUI16(ClipDepth);
            }
            if (PlaceFlagHasFilterList)
            {
                SurfaceFilterList.ToStream(writer);
            }
            if (PlaceFlagHasBlendMode)
            {
                writer.WriteUI8((byte) BlendMode);
            }
            if (PlaceFlagHasCacheAsBitmap)
            {
                writer.WriteUI8(BitmapCache);
            }
            if (PlaceFlagHasVisible)
            {
                writer.WriteUI8(Visible);
            }
            if (PlaceFlagOpaqueBackground)
            {
                BackgroundColor.ToStream(writer);
            }
            if (PlaceFlagHasClipActions)
            {
                ClipActions.ToStream(writer, swfVersion);
            }
        }
    }
}
