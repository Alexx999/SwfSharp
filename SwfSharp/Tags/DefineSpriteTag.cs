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
        [XmlElement("CSMTextSettings", typeof(CSMTextSettingsTag))]
        [XmlElement("DebugID", typeof(DebugIDTag))]
        [XmlElement("DefineBinaryData", typeof(DefineBinaryDataTag))]
        [XmlElement("DefineBitsJPEG2", typeof(DefineBitsJPEG2Tag))]
        [XmlElement("DefineBitsJPEG3", typeof(DefineBitsJPEG3Tag))]
        [XmlElement("DefineBitsJPEG4", typeof(DefineBitsJPEG4Tag))]
        [XmlElement("DefineBitsLossless", typeof(DefineBitsLosslessTag))]
        [XmlElement("DefineBitsLossless2", typeof(DefineBitsLossless2Tag))]
        [XmlElement("DefineBits", typeof(DefineBitsTag))]
        [XmlElement("DefineButton", typeof(DefineButtonTag))]
        [XmlElement("DefineButton2", typeof(DefineButton2Tag))]
        [XmlElement("DefineButtonCxform", typeof(DefineButtonCxformTag))]
        [XmlElement("DefineButtonSound", typeof(DefineButtonSoundTag))]
        [XmlElement("DefineEditText", typeof(DefineEditTextTag))]
        [XmlElement("DefineFont", typeof(DefineFontTag))]
        [XmlElement("DefineFont2", typeof(DefineFont2Tag))]
        [XmlElement("DefineFont3", typeof(DefineFont3Tag))]
        [XmlElement("DefineFont4", typeof(DefineFont4Tag))]
        [XmlElement("DefineFontAlignZones", typeof(DefineFontAlignZonesTag))]
        [XmlElement("DefineFontInfo", typeof(DefineFontInfoTag))]
        [XmlElement("DefineFontInfo2", typeof(DefineFontInfo2Tag))]
        [XmlElement("DefineFontName", typeof(DefineFontNameTag))]
        [XmlElement("DefineMorphShape", typeof(DefineMorphShapeTag))]
        [XmlElement("DefineMorphShape2", typeof(DefineMorphShape2Tag))]
        [XmlElement("DefineScalingGrid", typeof(DefineScalingGridTag))]
        [XmlElement("DefineSceneAndFrameLabelData", typeof(DefineSceneAndFrameLabelDataTag))]
        [XmlElement("DefineShape", typeof(DefineShapeTag))]
        [XmlElement("DefineShape2", typeof(DefineShape2Tag))]
        [XmlElement("DefineShape3", typeof(DefineShape3Tag))]
        [XmlElement("DefineShape4", typeof(DefineShape4Tag))]
        [XmlElement("DefineSound", typeof(DefineSoundTag))]
        [XmlElement("DefineSprite", typeof(DefineSpriteTag))]
        [XmlElement("DefineText", typeof(DefineTextTag))]
        [XmlElement("DefineText2", typeof(DefineText2Tag))]
        [XmlElement("DefineVideoStream", typeof(DefineVideoStreamTag))]
        [XmlElement("DoABC", typeof(DoABCTag))]
        [XmlElement("DoABC2", typeof(DoABC2Tag))]
        [XmlElement("DoAction", typeof(DoActionTag))]
        [XmlElement("DoInitAction", typeof(DoInitActionTag))]
        [XmlElement("EnableDebugger", typeof(EnableDebuggerTag))]
        [XmlElement("EnableDebugger2", typeof(EnableDebugger2Tag))]
        [XmlElement("EnableTelemetry", typeof(EnableTelemetryTag))]
        [XmlElement("End", typeof(EndTag))]
        [XmlElement("ExportAssets", typeof(ExportAssetsTag))]
        [XmlElement("FileAttributes", typeof(FileAttributesTag))]
        [XmlElement("FrameLabel", typeof(FrameLabelTag))]
        [XmlElement("ImportAssets", typeof(ImportAssetsTag))]
        [XmlElement("ImportAssets2", typeof(ImportAssets2Tag))]
        [XmlElement("JPEGTables", typeof(JPEGTablesTag))]
        [XmlElement("Metadata", typeof(MetadataTag))]
        [XmlElement("NameCharacter", typeof(NameCharacterTag))]
        [XmlElement("PlaceObject", typeof(PlaceObjectTag))]
        [XmlElement("PlaceObject2", typeof(PlaceObject2Tag))]
        [XmlElement("PlaceObject3", typeof(PlaceObject3Tag))]
        [XmlElement("ProductInfo", typeof(ProductInfoTag))]
        [XmlElement("Protect", typeof(ProtectTag))]
        [XmlElement("RemoveObject", typeof(RemoveObjectTag))]
        [XmlElement("RemoveObject2", typeof(RemoveObject2Tag))]
        [XmlElement("ScriptLimits", typeof(ScriptLimitsTag))]
        [XmlElement("SetBackgroundColor", typeof(SetBackgroundColorTag))]
        [XmlElement("SetTabIndex", typeof(SetTabIndexTab))]
        [XmlElement("ShowFrame", typeof(ShowFrameTag))]
        [XmlElement("SoundStreamBlock", typeof(SoundStreamBlockTag))]
        [XmlElement("SoundStreamHead", typeof(SoundStreamHeadTag))]
        [XmlElement("SoundStreamHead2", typeof(SoundStreamHead2Tag))]
        [XmlElement("StartSound", typeof(StartSoundTag))]
        [XmlElement("StartSound2", typeof(StartSound2Tag))]
        [XmlElement("SymbolClass", typeof(SymbolClassTag))]
        [XmlElement("Unknown", typeof(UnknownTag))]
        [XmlElement("VideoFrame", typeof(VideoFrameTag))]
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
