﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwfSharp;
using SwfSharp.Tags;

namespace SwfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles("./").Where(f => f.EndsWith(".swf"));

            var swfs = new ConcurrentDictionary<string, SwfFile>();

            //files.AsParallel().ForAll(f => swfs.AddOrUpdate(f, SwfFile.FromFile(f), (s, file) => file));

            foreach (var file in files)
            {
                swfs[file] = SwfFile.FromFile(file);
            }

            foreach (var swfFile in swfs)
            {
                swfFile.Value.ToFile(swfFile.Key + "unp");
            }
        }

        private static SwfTag GetTag(TagType type, int size)
        {
            switch (type)
            {
                case TagType.End:
                {
                    return new EndTag(size);
                }
                case TagType.ShowFrame:
                {
                    return new ShowFrameTag(size);
                }
                case TagType.DefineShape:
                {
                    return new DefineShapeTag(size);
                }
                case TagType.PlaceObject:
                {
                    return new PlaceObjectTag(size);
                }
                case TagType.RemoveObject:
                {
                    return new RemoveObjectTag(size);
                }
                case TagType.DefineBits:
                {
                    return new DefineBitsTag(size);
                }
                case TagType.DefineButton:
                {
                    return new DefineButtonTag(size);
                }
                case TagType.JPEGTables:
                {
                    return new JPEGTablesTag(size);
                }
                case TagType.SetBackgroundColor:
                {
                    return new SetBackgroundColorTag(size);
                }
                case TagType.DefineFont:
                {
                    return new DefineFontTag(size);
                }
                case TagType.DefineText:
                {
                    return new DefineTextTag(size);
                }
                case TagType.DoAction:
                {
                    return new DoActionTag(size);
                }
                case TagType.DefineFontInfo:
                {
                    return new DefineFontInfoTag(size);
                }
                case TagType.DefineSound:
                {
                    return new DefineSoundTag(size);
                }
                case TagType.StartSound:
                {
                    return new StartSoundTag(size);
                }
                case TagType.DefineButtonSound:
                {
                    return new DefineButtonSoundTag(size);
                }
                case TagType.SoundStreamHead:
                {
                    return new SoundStreamHeadTag(size);
                }
                case TagType.SoundStreamBlock:
                {
                    return new SoundStreamBlockTag(size);
                }
                case TagType.DefineBitsLossless:
                {
                    return new DefineBitsLosslessTag(size);
                }
                case TagType.DefineBitsJPEG2:
                {
                    return new DefineBitsJPEG2Tag(size);
                }
                case TagType.DefineShape2:
                {
                    return new DefineShape2Tag(size);
                }
                case TagType.DefineButtonCxform:
                {
                    return new DefineButtonCxformTag(size);
                }
                case TagType.Protect:
                {
                    return new ProtectTag(size);
                }
                case TagType.PlaceObject2:
                {
                    return new PlaceObject2Tag(size);
                }
                case TagType.RemoveObject2:
                {
                    return new RemoveObject2Tag(size);
                }
                case TagType.DefineShape3:
                {
                    return new DefineShape3Tag(size);
                }
                case TagType.DefineText2:
                {
                    return new DefineText2Tag(size);
                }
                case TagType.DefineButton2:
                {
                    return new DefineButton2Tag(size);
                }
                case TagType.DefineBitsJPEG3:
                {
                    return new DefineBitsJPEG3Tag(size);
                }
                case TagType.DefineBitsLossless2:
                {
                    return new DefineBitsLossless2Tag(size);
                }
                case TagType.DefineEditText:
                {
                    return new DefineEditTextTag(size);
                }
                case TagType.DefineSprite:
                {
                    return new DefineSpriteTag(size);
                }
                case TagType.ProductInfo:
                {
                    return new ProductInfoTag(size);
                }
                case TagType.FrameLabel:
                {
                    return new FrameLabelTag(size);
                }
                case TagType.SoundStreamHead2:
                {
                    return new SoundStreamHead2Tag(size);
                }
                case TagType.DefineMorphShape:
                {
                    return new DefineMorphShapeTag(size);
                }
                case TagType.DefineFont2:
                {
                    return new DefineFont2Tag(size);
                }
                case TagType.ExportAssets:
                {
                    return new ExportAssetsTag(size);
                }
                case TagType.ImportAssets:
                {
                    return new ImportAssetsTag(size);
                }
                case TagType.EnableDebugger:
                {
                    return new EnableDebuggerTag(size);
                }
                case TagType.DoInitAction:
                {
                    return new DoInitActionTag(size);
                }
                case TagType.DefineVideoStream:
                {
                    return new DefineVideoStreamTag(size);
                }
                case TagType.VideoFrame:
                {
                    return new VideoFrameTag(size);
                }
                case TagType.DefineFontInfo2:
                {
                    return new DefineFontInfo2Tag(size);
                }
                case TagType.DebugID:
                {
                    return new DebugIDTag(size);
                }
                case TagType.EnableDebugger2:
                {
                    return new EnableDebugger2Tag(size);
                }
                case TagType.ScriptLimits:
                {
                    return new ScriptLimitsTag(size);
                }
                case TagType.SetTabIndex:
                {
                    return new SetTabIndexTab(size);
                }
                case TagType.FileAttributes:
                {
                    return new FileAttributesTag(size);
                }
                case TagType.PlaceObject3:
                {
                    return new PlaceObject3Tag(size);
                }
                case TagType.ImportAssets2:
                {
                    return new ImportAssets2Tag(size);
                }
                case TagType.DefineFontAlignZones:
                {
                    return new DefineFontAlignZonesTag(size);
                }
                case TagType.CSMTextSetting:
                {
                    return new CSMTextSettingsTag(size);
                }
                case TagType.DefineFont3:
                {
                    return new DefineFont3Tag(size);
                }
                case TagType.SymbolClass:
                {
                    return new SymbolClassTag(size);
                }
                case TagType.Metadata:
                {
                    return new MetadataTag(size);
                }
                case TagType.DefineScalingGrid:
                {
                    return new DefineScalingGridTag(size);
                }
                case TagType.DoABC:
                {
                    return new DoABCTag(size);
                }
                case TagType.DefineShape4:
                {
                    return new DefineShape4Tag(size);
                }
                case TagType.DefineMorphShape2:
                {
                    return new DefineMorphShape2Tag(size);
                }
                case TagType.DefineSceneAndFrameLabelData:
                {
                    return new DefineSceneAndFrameLabelDataTag(size);
                }
                case TagType.DefineBinaryData:
                {
                    return new DefineBinaryDataTag(size);
                }
                case TagType.DefineFontName:
                {
                    return new DefineFontNameTag(size);
                }
                case TagType.StartSound2:
                {
                    return new StartSound2Tag(size);
                }
                case TagType.DefineBitsJPEG4:
                {
                    return new DefineBitsJPEG4Tag(size);
                }
                case TagType.DefineFont4:
                {
                    return new DefineFont4Tag(size);
                }
                case TagType.EnableTelemetry:
                {
                    return new EnableTelemetryTag(size);
                }
                default:
                {
                    return new UnknownTag(type, size);
                }
            }
        }
    }
}
