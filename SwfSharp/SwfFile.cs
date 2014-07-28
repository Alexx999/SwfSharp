using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Ionic.Zlib;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp
{
    [Serializable]
    public class SwfFile : IDisposable
    {
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
        private List<SwfTag> _tags;
        private SwfHeader _header;
        private Stream _uncompStream;

        private SwfFile()
        {
            _tags = new List<SwfTag>();
            _header = new SwfHeader();
        }

        public static SwfFile FromFile(string path)
        {
            return FromFile(path, true);
        }

        public static SwfFile FromFile(string path, bool decode)
        {
            var stream = File.OpenRead(path);
            return FromStream(stream, decode);
        }

        public static SwfFile FromStream(Stream stream)
        {
            return FromStream(stream, true);
        }

        public static SwfFile FromStream(Stream stream, bool decode)
        {
            var result = new SwfFile();
            result.InitFromStream(stream, decode);
            return result;
        }

        public void ToFile(string path)
        {
            using (var stream = File.Create(path))
            {
                ToStream(stream, false);
            }
        }
        public void ToStream(Stream stream)
        {
            ToStream(stream, false);
        }

        public void ToStream(Stream stream, bool keepOpen)
        {
            var dataStream = GetDataStream();
            _header.FileSize = (uint) (dataStream.Length + 8);
            using (var writer = new BitWriter(stream, keepOpen))
            {
                _header.ToUncompressedStream(writer);
                dataStream.CopyTo(stream);
            }
        }

        private Stream GetDataStream()
        {
            if (_uncompStream != null)
            {
                return _uncompStream;
            }
            var ms = new MemoryStream();
            using (var writer = new BitWriter(ms, true))
            {
                _header.ToStream(writer);
                var tagMs = new MemoryStream();
                foreach (var tag in Tags)
                {
                    TagFactory.WriteTag(writer, tag, _header.Version, tagMs);
                }
            }
            ms.Position = 0;
            return ms;
        }

        private void InitFromStream(Stream stream, bool decode)
        {
            var reader = new BitReader(stream, true);
            _header.FromCompressedStream(reader);
            _uncompStream = GetDecompressedStream(stream);

            if (decode) Decode();
        }

        public void Decode()
        {
            var reader = new BitReader(_uncompStream);
            _header.FromStream(reader);
            while (_uncompStream.Position < _uncompStream.Length)
            {
                var tag = TagFactory.ReadTag(reader, _header.Version);
                Tags.Add(tag);
            }
            _uncompStream.Close();
            _uncompStream = null;
        }

        private Stream GetDecompressedStream(Stream stream)
        {
            switch (_header.Compression)
            {
                case SwfFileCompression.None:
                {
                    return stream;
                }
                case SwfFileCompression.Zlib:
                {
                    return GeZlibDecodeStream(stream);
                }
                case SwfFileCompression.Lzma:
                {
                    
                    return GetLzmaDecodeStream(stream);
                }
                default:
                {
                    throw new FormatException("Data is not an Adobe SWF file");
                }
            }
        }

        private Stream GetLzmaDecodeStream(Stream source)
        {
            var coder = new SevenZip.Compression.LZMA.Decoder();
            source.Seek(4, SeekOrigin.Current);
            var properties = new byte[5];
            source.Read(properties, 0, 5);
            var result = new MemoryStream((int) _header.FileSize - 8);
            coder.SetDecoderProperties(properties);
            long compressedSize = source.Length - source.Position;
            coder.Code(source, result, compressedSize, _header.FileSize - 8, null);
            source.Dispose();
            result.Position = 0;
            return result;
        }
        private Stream GeZlibDecodeStream(Stream source)
        {
            var result = new MemoryStream((int)_header.FileSize - 8);
            using (var zlib = new ZlibStream(source, CompressionMode.Decompress, false))
            {
                zlib.CopyTo(result);
            }
            result.Position = 0;
            return result;
        }

        public IList<SwfTag> Tags
        {
            get { return _tags; }
        }

        public SwfHeader Header
        {
            get { return _header; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            var stream = _uncompStream;
            if (stream != null && disposing)
            {
                stream.Dispose();
            }
            _uncompStream = null;
            GC.SuppressFinalize(this);
        }
    }

    public enum SwfFileCompression
    {
        None = 'F',
        Zlib = 'C',
        Lzma = 'Z'
    }
}
