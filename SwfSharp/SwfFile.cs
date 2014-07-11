using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp
{
    public class SwfFile
    {
        private IList<SwfTag> _tags;
        private SwfHeader _header;

        private SwfFile()
        {
            _tags = new List<SwfTag>();
            _header = new SwfHeader();
        }

        public static SwfFile FromFile(string path)
        {
            var stream = File.OpenRead(path);
            return FromStream(stream);
        }

        public static SwfFile FromStream(Stream stream)
        {
            var result = new SwfFile();
            result.InitFromStream(stream);
            return result;
        }

        private void InitFromStream(Stream stream)
        {
            var reader = new BitReader(stream);
            _header.FromCompressedStream(reader);
            stream = GetDecompressedStream(stream);
            reader = new BitReader(stream);
            _header.FromStream(reader);
            while (stream.Position < stream.Length)
            {
                var tag = TagFactory.ReadTag(reader);
                Tags.Add(tag);
            }
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
    }

    public enum SwfFileCompression
    {
        None = 'F',
        Zlib = 'C',
        Lzma = 'Z'
    }
}
