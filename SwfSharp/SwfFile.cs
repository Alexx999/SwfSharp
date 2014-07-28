using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp
{
    public class SwfFile : IDisposable
    {
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
