using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineFontAlignZonesTag : SwfTag
    {
        public ushort FontID { get; set; }
        public CSMTableHint CSMTableHint { get; set; }
        public IList<ZoneRecordStruct> ZoneTable { get; set; }

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
