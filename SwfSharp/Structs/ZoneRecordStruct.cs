using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ZoneRecordStruct
    {
        public List<ZoneDataStruct> ZoneData { get; set; }
        public bool ZoneMaskY { get; set; }
        public bool ZoneMaskX { get; set; }

        private void FromStream(BitReader reader)
        {
            var numZoneData = reader.ReadUI8();
            ZoneData = new List<ZoneDataStruct>(numZoneData);
            for (int i = 0; i < numZoneData; i++)
            {
                ZoneData.Add(ZoneDataStruct.CreateFromStream(reader));
            }
            reader.ReadBits(6);
            ZoneMaskY = reader.ReadBoolBit();
            ZoneMaskX = reader.ReadBoolBit();
        }

        internal static ZoneRecordStruct CreateFromStream(BitReader reader)
        {
            var result = new ZoneRecordStruct();

            result.FromStream(reader);

            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            writer.WriteUI8((byte) ZoneData.Count);
            foreach (var zoneDataStruct in ZoneData)
            {
                zoneDataStruct.ToStream(writer);
            }
            writer.WriteBits(6, 0);
            writer.WriteBoolBit(ZoneMaskY);
            writer.WriteBoolBit(ZoneMaskX);
        }
    }
}
