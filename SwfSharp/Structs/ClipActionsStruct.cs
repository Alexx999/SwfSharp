using System.Collections.Generic;
using System.IO;
using System.Linq;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ClipActionsStruct
    {
        public ClipEventFlagsStruct AllEventFlags { get; set; }
        public IList<ClipActionRecordStruct> ClipActionRecords { get; set; }

        private void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadUI16();
            AllEventFlags = ClipEventFlagsStruct.CreateFromStream(reader, swfVersion);
            var endFlagSize = (swfVersion >= 6) ? 4 : 2;
            var bytes = reader.ReadBytes(endFlagSize);
            if (bytes.Any(b => b != 0))
            {
                reader.Seek(-endFlagSize, SeekOrigin.Current);
            }

        }

        internal static ClipActionsStruct CreateFromStream(BitReader reader, byte swfVersion)
        {
            var result = new ClipActionsStruct();

            result.FromStream(reader, swfVersion);

            return result;
        }
    }
}