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
        private BlendMode? _blendMode;
        private byte? _bitmapCache;
        private byte? _visible;

        [XmlAttribute]
        public bool PlaceFlagHasImage { get; set; }
        [XmlAttribute]
        public string ClassName { get; set; }
        [XmlElement]
        public FilterListStruct SurfaceFilterList { get; set; }

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

        [XmlAttribute]
        public byte BitmapCache
        {
            get { return _bitmapCache.GetValueOrDefault(); }
            set { _bitmapCache = value; }
        }

        [XmlIgnore]
        public bool BitmapCacheSpecified
        {
            get { return _bitmapCache.HasValue; }
        }

        [XmlAttribute]
        public byte Visible
        {
            get { return _visible.GetValueOrDefault(); }
            set { _visible = value; }
        }

        [XmlIgnore]
        public bool VisibleSpecified
        {
            get { return _visible.HasValue; }
        }

        [XmlElement]
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
            var placeFlagHasClipActions = reader.ReadBoolBit();
            var placeFlagHasClipDepth = reader.ReadBoolBit();
            var placeFlagHasName = reader.ReadBoolBit();
            var placeFlagHasRatio = reader.ReadBoolBit();
            var placeFlagHasColorTransform = reader.ReadBoolBit();
            var placeFlagHasMatrix = reader.ReadBoolBit();
            var placeFlagHasCharacter = reader.ReadBoolBit();
            PlaceFlagMove = reader.ReadBoolBit();
            reader.ReadBits(1);
            var placeFlagOpaqueBackground = reader.ReadBoolBit();
            var placeFlagHasVisible = reader.ReadBoolBit();
            PlaceFlagHasImage = reader.ReadBoolBit();
            var placeFlagHasClassName = reader.ReadBoolBit();
            var placeFlagHasCacheAsBitmap = reader.ReadBoolBit();
            var placeFlagHasBlendMode = reader.ReadBoolBit();
            var placeFlagHasFilterList = reader.ReadBoolBit();
            Depth = reader.ReadUI16();
            if (placeFlagHasClassName)
            {
                ClassName = reader.ReadString();
            }
            if (placeFlagHasCharacter)
            {
                CharacterId = reader.ReadUI16();
            }
            if (placeFlagHasMatrix)
            {
                Matrix = MatrixStruct.CreateFromStream(reader);
            }
            if (placeFlagHasColorTransform)
            {
                ColorTransform = CXformWithAlphaStruct.CreateFromStream(reader);
            }
            if (placeFlagHasRatio)
            {
                Ratio = reader.ReadUI16();
            }
            if (placeFlagHasName)
            {
                Name = reader.ReadString();
            }
            if (placeFlagHasClipDepth)
            {
                ClipDepth = reader.ReadUI16();
            }
            if (placeFlagHasFilterList)
            {
                SurfaceFilterList = FilterListStruct.CreateFromStream(reader);
            }
            if (placeFlagHasBlendMode)
            {
                BlendMode = (BlendMode) reader.ReadUI8();
            }
            if (placeFlagHasCacheAsBitmap)
            {
                BitmapCache = reader.ReadUI8();
            }
            if (placeFlagHasVisible)
            {
                Visible = reader.ReadUI8();
            }
            if (placeFlagOpaqueBackground)
            {
                BackgroundColor = RgbaStruct.CreateFromStream(reader);
            }
            if (placeFlagHasClipActions)
            {
                ClipActions = ClipActionsStruct.CreateFromStream(reader, swfVersion);
            }
        }
        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            var placeFlagHasClipActions = ClipActions != null;
            var placeFlagHasClipDepth = _clipDepth.HasValue;
            var placeFlagHasName = !string.IsNullOrEmpty(Name);
            var placeFlagHasRatio = _ratio.HasValue;
            var placeFlagHasColorTransform = ColorTransform != null;
            var placeFlagHasMatrix = Matrix != null;
            var placeFlagHasCharacter = _characterId.HasValue;
            var placeFlagOpaqueBackground = BackgroundColor != null;
            var placeFlagHasVisible = _visible.HasValue;
            var placeFlagHasClassName = !string.IsNullOrEmpty(ClassName);
            var placeFlagHasCacheAsBitmap = _bitmapCache.HasValue;
            var placeFlagHasBlendMode = _blendMode.HasValue;
            var placeFlagHasFilterList = SurfaceFilterList != null;

            writer.WriteBoolBit(placeFlagHasClipActions);
            writer.WriteBoolBit(placeFlagHasClipDepth);
            writer.WriteBoolBit(placeFlagHasName);
            writer.WriteBoolBit(placeFlagHasRatio);
            writer.WriteBoolBit(placeFlagHasColorTransform);
            writer.WriteBoolBit(placeFlagHasMatrix);
            writer.WriteBoolBit(placeFlagHasCharacter);
            writer.WriteBoolBit(PlaceFlagMove);
            writer.WriteBits(1, 0);
            writer.WriteBoolBit(placeFlagOpaqueBackground);
            writer.WriteBoolBit(placeFlagHasVisible);
            writer.WriteBoolBit(PlaceFlagHasImage);
            writer.WriteBoolBit(placeFlagHasClassName);
            writer.WriteBoolBit(placeFlagHasCacheAsBitmap);
            writer.WriteBoolBit(placeFlagHasBlendMode);
            writer.WriteBoolBit(placeFlagHasFilterList);
            writer.WriteUI16(Depth);
            if (placeFlagHasClassName)
            {
                writer.WriteString(ClassName, swfVersion);
            }
            if (placeFlagHasCharacter)
            {
                writer.WriteUI16(_characterId.Value);
            }
            if (placeFlagHasMatrix)
            {
                Matrix.ToStream(writer);
            }
            if (placeFlagHasColorTransform)
            {
                ColorTransform.ToStream(writer);
            }
            if (placeFlagHasRatio)
            {
                writer.WriteUI16(_ratio.Value);
            }
            if (placeFlagHasName)
            {
                writer.WriteString(Name, swfVersion);
            }
            if (placeFlagHasClipDepth)
            {
                writer.WriteUI16(_clipDepth.Value);
            }
            if (placeFlagHasFilterList)
            {
                SurfaceFilterList.ToStream(writer);
            }
            if (placeFlagHasBlendMode)
            {
                writer.WriteUI8((byte)_blendMode.Value);
            }
            if (placeFlagHasCacheAsBitmap)
            {
                writer.WriteUI8(_bitmapCache.Value);
            }
            if (placeFlagHasVisible)
            {
                writer.WriteUI8(_visible.Value);
            }
            if (placeFlagOpaqueBackground)
            {
                BackgroundColor.ToStream(writer);
            }
            if (placeFlagHasClipActions)
            {
                ClipActions.ToStream(writer, swfVersion);
            }
        }
    }
}
