using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class ActionRecordStruct
    {
        public byte ActionCode { get; set; }
        public ushort Length { get; set; }
        public byte[] Data { get; set; }
        public int Size { get; set; }

        private void FromStream(BitReader reader)
        {
            Size = 1;
            ActionCode = reader.ReadUI8();
            if (ActionCode < 0x80) return;
            Length = reader.ReadUI16();
            Size = Length + 3;
            Data = reader.ReadBytes(Length);
        }

        internal static ActionRecordStruct CreateFromStream(BitReader reader)
        {
            var result = new ActionRecordStruct();

            result.FromStream(reader);

            return result;
        }
    }
}
