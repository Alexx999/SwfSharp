﻿using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineBitsLosslessTag : SwfTag
    {
        private byte? _bitmapColorTableSize;

        [XmlAttribute]
        public ushort CharacterID { get; set; }
        [XmlAttribute]
        public BitmapFormatType BitmapFormat { get; set; }
        [XmlAttribute]
        public ushort BitmapWidth { get; set; }
        [XmlAttribute]
        public ushort BitmapHeight { get; set; }

        [XmlAttribute]
        public byte BitmapColorTableSize
        {
            get { return _bitmapColorTableSize.GetValueOrDefault(); }
            set { _bitmapColorTableSize = value; }
        }

        [XmlIgnore]
        public bool BitmapColorTableSizeSpecified
        {
            get { return _bitmapColorTableSize.HasValue; }
        }

        [XmlElement]
        public byte[] ZlibBitmapData { get; set; }
        /*
        public ColorMapDataStruct ColorMapData { get; set; }
        public BitmapDataStruct BitmapData { get; set; }
         */

        public DefineBitsLosslessTag() : this(0)
        {
        }

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
                _bitmapColorTableSize = reader.ReadUI8();
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

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterID);
            writer.WriteUI8((byte)BitmapFormat);
            writer.WriteUI16(BitmapWidth);
            writer.WriteUI16(BitmapHeight);
            if (BitmapFormat == BitmapFormatType.Colormap8)
            {
                writer.WriteUI8(_bitmapColorTableSize.GetValueOrDefault());
            }
            writer.WriteBytes(ZlibBitmapData);
        }

        public enum BitmapFormatType : byte
        {
            Colormap8 = 3,
            RGB15 = 4,
            RGB24 = 5
        }
    }
}
