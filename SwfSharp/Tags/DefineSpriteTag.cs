using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineSpriteTag : SwfTag
    {
        [XmlAttribute]
        public ushort SpriteID { get; set; }
        [XmlAttribute]
        public ushort FrameCount { get; set; }
        [XmlArrayItem("CSMTextSettings", typeof(CSMTextSettingsTag))]
        [XmlArrayItem("DebugID", typeof(DebugIDTag))]
        [XmlArrayItem("DefineBinaryData", typeof(DefineBinaryDataTag))]
        [XmlArrayItem("DefineBitsJPEG2", typeof(DefineBitsJPEG2Tag))]
        [XmlArrayItem("DefineBitsJPEG3", typeof(DefineBitsJPEG3Tag))]
        [XmlArrayItem("DefineBitsJPEG4", typeof(DefineBitsJPEG4Tag))]
        [XmlArrayItem("DefineBitsLossless", typeof(DefineBitsLosslessTag))]
        [XmlArrayItem("DefineBitsLossless2", typeof(DefineBitsLossless2Tag))]
        [XmlArrayItem("DefineBits", typeof(DefineBitsTag))]
        [XmlArrayItem("DefineButton", typeof(DefineButtonTag))]
        [XmlArrayItem("DefineButton2", typeof(DefineButton2Tag))]
        [XmlArrayItem("DefineButtonCxform", typeof(DefineButtonCxformTag))]
        [XmlArrayItem("DefineButtonSound", typeof(DefineButtonSoundTag))]
        [XmlArrayItem("DefineEditText", typeof(DefineEditTextTag))]
        [XmlArrayItem("DefineFont", typeof(DefineFontTag))]
        [XmlArrayItem("DefineFont2", typeof(DefineFont2Tag))]
        [XmlArrayItem("DefineFont3", typeof(DefineFont3Tag))]
        [XmlArrayItem("DefineFont4", typeof(DefineFont4Tag))]
        [XmlArrayItem("DefineFontAlignZones", typeof(DefineFontAlignZonesTag))]
        [XmlArrayItem("DefineFontInfo", typeof(DefineFontInfoTag))]
        [XmlArrayItem("DefineFontInfo2", typeof(DefineFontInfo2Tag))]
        [XmlArrayItem("DefineFontName", typeof(DefineFontNameTag))]
        [XmlArrayItem("DefineMorphShape", typeof(DefineMorphShapeTag))]
        [XmlArrayItem("DefineMorphShape2", typeof(DefineMorphShape2Tag))]
        [XmlArrayItem("DefineScalingGrid", typeof(DefineScalingGridTag))]
        [XmlArrayItem("DefineSceneAndFrameLabelData", typeof(DefineSceneAndFrameLabelDataTag))]
        [XmlArrayItem("DefineShape", typeof(DefineShapeTag))]
        [XmlArrayItem("DefineShape2", typeof(DefineShape2Tag))]
        [XmlArrayItem("DefineShape3", typeof(DefineShape3Tag))]
        [XmlArrayItem("DefineShape4", typeof(DefineShape4Tag))]
        [XmlArrayItem("DefineSound", typeof(DefineSoundTag))]
        [XmlArrayItem("DefineSprite", typeof(DefineSpriteTag))]
        [XmlArrayItem("DefineText", typeof(DefineTextTag))]
        [XmlArrayItem("DefineText2", typeof(DefineText2Tag))]
        [XmlArrayItem("DefineVideoStream", typeof(DefineVideoStreamTag))]
        [XmlArrayItem("DoABC", typeof(DoABCTag))]
        [XmlArrayItem("DoAction", typeof(DoActionTag))]
        [XmlArrayItem("DoInitAction", typeof(DoInitActionTag))]
        [XmlArrayItem("EnableDebugger", typeof(EnableDebuggerTag))]
        [XmlArrayItem("EnableDebugger2", typeof(EnableDebugger2Tag))]
        [XmlArrayItem("EnableTelemetry", typeof(EnableTelemetryTag))]
        [XmlArrayItem("End", typeof(EndTag))]
        [XmlArrayItem("ExportAssets", typeof(ExportAssetsTag))]
        [XmlArrayItem("FileAttributes", typeof(FileAttributesTag))]
        [XmlArrayItem("FrameLabel", typeof(FrameLabelTag))]
        [XmlArrayItem("ImportAssets", typeof(ImportAssetsTag))]
        [XmlArrayItem("ImportAssets2", typeof(ImportAssets2Tag))]
        [XmlArrayItem("JPEGTables", typeof(JPEGTablesTag))]
        [XmlArrayItem("Metadata", typeof(MetadataTag))]
        [XmlArrayItem("PlaceObject", typeof(PlaceObjectTag))]
        [XmlArrayItem("PlaceObject2", typeof(PlaceObject2Tag))]
        [XmlArrayItem("PlaceObject3", typeof(PlaceObject3Tag))]
        [XmlArrayItem("ProductInfo", typeof(ProductInfoTag))]
        [XmlArrayItem("Protect", typeof(ProtectTag))]
        [XmlArrayItem("RemoveObject", typeof(RemoveObjectTag))]
        [XmlArrayItem("RemoveObject2", typeof(RemoveObject2Tag))]
        [XmlArrayItem("ScriptLimits", typeof(ScriptLimitsTag))]
        [XmlArrayItem("SetBackgroundColor", typeof(SetBackgroundColorTag))]
        [XmlArrayItem("SetTabIndex", typeof(SetTabIndexTab))]
        [XmlArrayItem("ShowFrame", typeof(ShowFrameTag))]
        [XmlArrayItem("SoundStreamBlock", typeof(SoundStreamBlockTag))]
        [XmlArrayItem("SoundStreamHead", typeof(SoundStreamHeadTag))]
        [XmlArrayItem("SoundStreamHead2", typeof(SoundStreamHead2Tag))]
        [XmlArrayItem("StartSound", typeof(StartSoundTag))]
        [XmlArrayItem("StartSound2", typeof(StartSound2Tag))]
        [XmlArrayItem("SymbolClass", typeof(SymbolClassTag))]
        [XmlArrayItem("Unknown", typeof(UnknownTag))]
        [XmlArrayItem("VideoFrame", typeof(VideoFrameTag))]
        public List<SwfTag> ControlTags { get; set; }

        public DefineSpriteTag() : this(0)
        {
        }

        public DefineSpriteTag(int size)
            : base(TagType.DefineSprite, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            SpriteID = reader.ReadUI16();
            FrameCount = reader.ReadUI16();
            ControlTags = new List<SwfTag>();
            SwfTag tag;
            do
            {
                tag = TagFactory.ReadTag(reader, swfVersion);
                ControlTags.Add(tag);
            } while (tag.TagType != TagType.End);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(SpriteID);
            writer.WriteUI16(FrameCount);
            var ms = new MemoryStream();
            foreach (var tag in ControlTags)
            {
                TagFactory.WriteTag(writer, tag, swfVersion, ms);
            }
        }
    }
}
