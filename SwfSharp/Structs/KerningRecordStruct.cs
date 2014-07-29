using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    [Serializable]
    public class KerningRecordStruct
    {
        [XmlAttribute]
        public ushort FontKerningCode1 { get; set; }
        [XmlAttribute]
        public ushort FontKerningCode2 { get; set; }
        [XmlAttribute]
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

        internal void ToStream(BitWriter writer, bool wide)
        {
            if (wide)
            {
                writer.WriteUI16(FontKerningCode1);
                writer.WriteUI16(FontKerningCode2);
            }
            else
            {
                writer.WriteUI8((byte) FontKerningCode1);
                writer.WriteUI8((byte) FontKerningCode2);
            }
            writer.WriteSI16(FontKerningAdjustment);
        }
    }
}
