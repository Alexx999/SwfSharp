using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class SetTabIndexTab : SwfTag
    {
        public ushort Depth { get; set; }
        public ushort TabIndex { get; set; }

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
            throw new NotImplementedException();
        }
    }
}
