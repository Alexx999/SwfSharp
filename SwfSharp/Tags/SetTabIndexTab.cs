using System;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class SetTabIndexTab : SwfTag
    {
        [XmlAttribute]
        public ushort Depth { get; set; }
        [XmlAttribute]
        public ushort TabIndex { get; set; }

        public SetTabIndexTab() : this(0)
        {
        }

        public SetTabIndexTab(int size)
            : base(TagType.SetTabIndex, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Depth = reader.ReadUI16();
            TabIndex = reader.ReadUI16();
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(Depth);
            writer.WriteUI16(TabIndex);
        }
    }
}
