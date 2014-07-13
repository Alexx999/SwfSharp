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
    public class DefineBitsLosslessTag : SwfTag
    {
        public ushort CharacterID { get; set; }
        public BitmapFormatType BitmapFormat { get; set; }
        public ushort BitmapWidth { get; set; }
        public ushort BitmapHeight { get; set; }
        public byte BitmapColorTableSize { get; set; }
        public byte[] ZlibBitmapData { get; set; }
        /*
        public ColorMapDataStruct ColorMapData { get; set; }
        public BitmapDataStruct BitmapData { get; set; }
         */

        public DefineBitsLosslessTag(int size)
            : base(TagType.DefineBitsLossless, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterID = reader.ReadUI16();
            BitmapFormat = (BitmapFormatType) reader.ReadUI8();
            BitmapWidth = reader.ReadUI16();
            BitmapHeight = reader.ReadUI16();
            if (BitmapFormat == BitmapFormatType.Colormap8)
            {
                BitmapColorTableSize = reader.ReadUI8();
            }
            ZlibBitmapData = reader.ReadBytes((int)reader.TagBytesRemaining);
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
                ColorMapData = ColorMapDataStruct.CreateFromStream(bitReader, BitmapColorTableSize, (int) unpackStream.Length);
            }
            else
            {
                BitmapData = BitmapDataStruct.CreateFromStream(bitReader, BitmapFormat, BitmapWidth, BitmapHeight);
            }*/
        }

        public enum BitmapFormatType : byte
        {
            Colormap8 = 3,
            RGB15 = 4,
            RGB24 = 5
        }
    }
}
