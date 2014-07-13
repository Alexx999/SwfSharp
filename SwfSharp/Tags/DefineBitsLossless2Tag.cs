using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zlib;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineBitsLossless2Tag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public BitmapFormatType BitmapFormat { get; set; }
        public ushort BitmapWidth { get; set; }
        public ushort BitmapHeight { get; set; }
        public byte BitmapColorTableSize { get; set; }
        public byte[] ZlibBitmapData { get; set; }
        /*
        public AlphaColorMapDataStruct ColorMapData { get; set; }
        public AlphaBitmapDataStruct BitmapData { get; set; }
         */

        public DefineBitsLossless2Tag(int size)
            : base(TagType.DefineBitsLossless2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            BitmapFormat = (BitmapFormatType)reader.ReadUI8();
            BitmapWidth = reader.ReadUI16();
            BitmapHeight = reader.ReadUI16();
            if (BitmapFormat == BitmapFormatType.Colormap8)
            {
                BitmapColorTableSize = reader.ReadUI8();
            }
            ZlibBitmapData = reader.ReadBytes((int) reader.TagBytesRemaining);
            /*
            var memoryStream = new MemoryStream(ZlibBitmapData);
            MemoryStream unpackStream;
            using (var zlib = new ZlibStream(memoryStream, CompressionMode.Decompress, false))
            {
                unpackStream = new MemoryStream();
                zlib.CopyTo(unpackStream);
            }
            unpackStream.Position = 0;
            var bitReader = new BitReader(unpackStream, false);
            if (BitmapFormat == BitmapFormatType.Colormap8)
            {
                ColorMapData = AlphaColorMapDataStruct.CreateFromStream(bitReader, BitmapColorTableSize, (int)unpackStream.Length);
            }
            else
            {
                BitmapData = AlphaBitmapDataStruct.CreateFromStream(bitReader, BitmapWidth, BitmapHeight);
            }*/
        }

        public enum BitmapFormatType : byte
        {
            Colormap8 = 3,
            ARGB32 = 5
        }
    }
}
