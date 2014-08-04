using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class ClipActionsStruct
    {
        [XmlElement]
        public ClipEventFlagsStruct AllEventFlags { get; set; }
        [XmlArrayItem("ClipActionRecord")]
        public List<ClipActionRecordStruct> ClipActionRecords { get; set; }

        private void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadUI16();
            AllEventFlags = ClipEventFlagsStruct.CreateFromStream(reader, swfVersion);
            var endFlagSize = (swfVersion >= 6) ? 4 : 2;
            ClipActionRecords = new List<ClipActionRecordStruct>();
            var bytes = reader.ReadBytes(endFlagSize);
            while (!AllZero(bytes))
            {
                reader.Seek(-endFlagSize, SeekOrigin.Current);
                ClipActionRecords.Add(ClipActionRecordStruct.CreateFromStream(reader, swfVersion));
                bytes = reader.ReadBytes(endFlagSize);
            }

        }

        private bool AllZero(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if(bytes[i] != 0) return false;
            }
            return true;
        }

        internal static ClipActionsStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipActionsStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }

        internal void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(0);
            AllEventFlags.ToStream(writer, swfVersion);
            var endFlagSize = (swfVersion >= 6) ? 4 : 2;
            foreach (var record in ClipActionRecords)
            {
                record.ToStream(writer, swfVersion);
            }
            writer.WriteBytes(new byte[endFlagSize]);
        }
    }
}