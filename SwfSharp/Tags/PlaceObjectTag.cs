using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class PlaceObjectTag : SwfTag
    {
        [XmlAttribute]
        public ushort CharacterId { get; set; }
        [XmlAttribute]
        public ushort Depth { get; set; }
        public MatrixStruct Matrix { get; set; }
        public CXformStruct ColorTransform { get; set; }

        public PlaceObjectTag() : this(0)
        {
        }

        public PlaceObjectTag(int size)
            : base(TagType.PlaceObject, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            CharacterId = reader.ReadUI16();
            Depth = reader.ReadUI16();
            Matrix = MatrixStruct.CreateFromStream(reader);
            if (!reader.AtTagEnd())
            {
                ColorTransform = CXformStruct.CreateFromStream(reader);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(CharacterId);
            writer.WriteUI16(Depth);
            Matrix.ToStream(writer);
            if (ColorTransform != null)
            {
                ColorTransform.ToStream(writer);
            }
        }
    }
}
