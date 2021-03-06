﻿using System;
using System.Xml.Serialization;
using SwfSharp.Tags;
using SwfSharp.Utils;

namespace SwfSharp.ShapeRecords
{
    [XmlInclude(typeof(EndShapeRecord))]
    [XmlInclude(typeof(StyleChangeRecord))]
    [XmlInclude(typeof(StraightEdgeRecord))]
    [XmlInclude(typeof(CurvedEdgeRecord))]
    [Serializable]
    public abstract class ShapeRecord
    {
        [XmlIgnore]
        public ShapeRecordType RecordType { get; set; }

        public ShapeRecord(ShapeRecordType recordType)
        {
            RecordType = recordType;
        }

        internal abstract void ToStream(BitWriter writer, ref byte numFillBits, ref byte numLineBits, TagType tagType);
    }
}
