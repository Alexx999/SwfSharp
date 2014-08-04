using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineFontAlignZonesTag : SwfTag
    {
        [XmlAttribute]
        public ushort FontID { get; set; }
        [XmlAttribute]
        public CSMTableHint CSMTableHint { get; set; }
        [XmlElement("ZoneRecord")]
        public List<ZoneRecordStruct> ZoneTable { get; set; }

        public DefineFontAlignZonesTag() : this(0)
        {
        }

        public DefineFontAlignZonesTag(int size)
            : base(TagType.DefineFontAlignZones, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            FontID = reader.ReadUI16();
            CSMTableHint = (CSMTableHint) reader.ReadBits(2);
            reader.ReadBits(6);
            ZoneTable = new List<ZoneRecordStruct>();
            while (reader.TagBytesRemaining != 0)
            {
                ZoneTable.Add(ZoneRecordStruct.CreateFromStream(reader));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(FontID);
            writer.WriteBits(2, (uint) CSMTableHint);
            writer.WriteBits(6, 0);
            foreach (var zone in ZoneTable)
            {
                zone.ToStream(writer);
            }
        }
    }
}
