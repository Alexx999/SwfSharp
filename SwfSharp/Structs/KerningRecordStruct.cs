using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class KerningRecordStruct
    {
        public ushort FontKerningCode1 { get; set; }
        public ushort FontKerningCode2 { get; set; }
        public short FontKerningAdjustment { get; set; }

        private void FromStream(BitReader reader, bool wide)
        {
            FontKerningCode1 = wide ? reader.ReadUI16() : reader.ReadUI8();
            FontKerningCode2 = wide ? reader.ReadUI16() : reader.ReadUI8();
            FontKerningAdjustment = reader.ReadSI16();
        }

        internal static KerningRecordStruct CreateFromStream(BitReader reader, bool wide)
        {
            var result = new KerningRecordStruct();

            result.FromStream(reader, wide);

            return result;
        }
    }
}
