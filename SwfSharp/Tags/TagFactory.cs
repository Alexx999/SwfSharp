﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Annotations;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    internal static class TagFactory
    {
        private const ushort SizeMask = 0xFFFF >> 10;

        [NotNull]
        public static SwfTag ReadTag([NotNull]BitReader reader, byte swfVersion)
        {
            reader.Align();
            var tagCodeAndLength = reader.ReadUI16();
            var type = (TagType)(tagCodeAndLength >> 6);
            var size = tagCodeAndLength & SizeMask;
            if (size == SizeMask)
            {
                size = reader.ReadSI32();
            }
            var tag = GetTag(type, size);
            reader.BeginReadTag(size);
            tag.FromStream(reader, swfVersion);
            reader.EndReadTag();
            return tag;
        }

        [NotNull]
        private static SwfTag GetTag(TagType type, int size)
        {
            switch (type)
            {
                case TagType.End:
                {
                    return new EndTag(type, size);
                }
                case TagType.ShowFrame:
                {
                    return new ShowFrameTag(type, size);
                }
                case TagType.DefineShape:
                {
                    return new DefineShapeTag(type, size);
                }
                case TagType.PlaceObject:
                {
                    return new PlaceObjectTag(type, size);
                }
                case TagType.RemoveObject:
                {
                    return new RemoveObjectTag(type, size);
                }
                case TagType.DefineBits:
                {
                    return new DefineBitsTag(type, size);
                }
                case TagType.JPEGTables:
                {
                    return new JPEGTablesTag(type, size);
                }
                case TagType.SetBackgroundColor:
                {
                    return new SetBackgroundColorTag(type, size);
                }
                case TagType.DefineFont:
                {
                    return new DefineFontTag(type, size);
                }
                case TagType.DefineText:
                {
                    return new DefineTextTag(type, size);
                }
                case TagType.DoAction:
                {
                    return new DoActionTag(type, size);
                }
                case TagType.DefineFontInfo:
                {
                    return new DefineFontInfoTag(type, size);
                }
                case TagType.DefineSound:
                {
                    return new DefineSoundTag(type, size);
                }
                case TagType.StartSound:
                {
                    return new StartSoundTag(type, size);
                }
                case TagType.SoundStreamHead:
                {
                    return new SoundStreamHeadTag(type, size);
                }
                case TagType.SoundStreamBlock:
                {
                    return new SoundStreamBlockTag(type, size);
                }
                case TagType.DefineBitsLossless:
                {
                    return new DefineBitsLosslessTag(type, size);
                }
                case TagType.DefineBitsJPEG2:
                {
                    return new DefineBitsJPEG2Tag(type, size);
                }
                case TagType.DefineShape2:
                {
                    return new DefineShape2Tag(type, size);
                }
                case TagType.Protect:
                {
                    return new ProtectTag(type, size);
                }
                case TagType.PlaceObject2:
                {
                    return new PlaceObject2Tag(type, size);
                }
                case TagType.RemoveObject2:
                {
                    return new RemoveObject2Tag(type, size);
                }
                case TagType.DefineShape3:
                {
                    return new DefineShape3Tag(type, size);
                }
                case TagType.DefineText2:
                {
                    return new DefineText2Tag(type, size);
                }
                case TagType.DefineBitsJPEG3:
                {
                    return new DefineBitsJPEG3Tag(type, size);
                }
                case TagType.DefineBitsLossless2:
                {
                    return new DefineBitsLossless2Tag(type, size);
                }
                case TagType.DefineEditText:
                {
                    return new DefineEditTextTag(type, size);
                }
                case TagType.DefineSprite:
                {
                    return new DefineSpriteTag(type, size);
                }
                case TagType.ProductInfo:
                {
                    return new ProductInfoTag(type, size);
                }
                case TagType.FrameLabel:
                {
                    return new FrameLabelTag(type, size);
                }
                case TagType.SoundStreamHead2:
                {
                    return new SoundStreamHead2Tag(type, size);
                }
                case TagType.DefineFont2:
                {
                    return new DefineFont2Tag(type, size);
                }
                case TagType.ExportAssets:
                {
                    return new ExportAssetsTag(type, size);
                }
                case TagType.EnableDebugger:
                {
                    return new EnableDebuggerTag(type, size);
                }
                case TagType.DoInitAction:
                {
                    return new DoInitActionTag(type, size);
                }
                case TagType.DefineFontInfo2:
                {
                    return new DefineFontInfo2Tag(type, size);
                }
                case TagType.EnableDebugger2:
                {
                    return new EnableDebugger2Tag(type, size);
                }
                case TagType.ScriptLimits:
                {
                    return new ScriptLimitsTag(type, size);
                }
                case TagType.FileAttributes:
                {
                    return new FileAttributesTag(type, size);
                }
                case TagType.DefineFont3:
                {
                    return new DefineFont3Tag(type, size);
                }
                case TagType.SymbolClass:
                {
                    return new SymbolClassTag(type, size);
                }
                case TagType.Metadata:
                {
                    return new MetadataTag(type, size);
                }
                case TagType.DoABC:
                {
                    return new DoABCTag(type, size);
                }
                case TagType.DefineShape4:
                {
                    return new DefineShape4Tag(type, size);
                }
                case TagType.DefineBinaryData:
                {
                    return new DefineBinaryDataTag(type, size);
                }
                case TagType.StartSound2:
                {
                    return new StartSound2Tag(type, size);
                }
                case TagType.EnableTelemetry:
                {
                    return  new EnableTelemetryTag(type, size);
                }
                default:
                {
                    return new UnknownTag(type, size);
                }
            }
        }
    }
}
