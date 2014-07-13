using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    class DefineFontAlignZonesTag : SwfTag
    {
        public ushort FontID { get; set; }
        public CSMTableHint CSMTableHint { get; set; }
        public IList<ZoneRecordStruct> ZoneTable { get; set; } 

        public DefineFontAlignZonesTag(TagType tagType, int size) : base(tagType, size)
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
    }
}
