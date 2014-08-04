using System;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineButtonCxformTag : SwfTag
    {
        [XmlAttribute]
        public ushort ButtonId { get; set; }
        [XmlElement]
        public CXformStruct ButtonColorTransform { get; set; }

        public DefineButtonCxformTag() : this(0)
        {
        }

        public DefineButtonCxformTag(int size)
            : base(TagType.DefineButtonCxform, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            ButtonColorTransform = CXformStruct.CreateFromStream(reader);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ButtonId);
            ButtonColorTransform.ToStream(writer);
        }
    }
}
