using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class ScriptLimitsTag : SwfTag
    {
        public ushort MaxRecursionDepth { get; set; }
        public ushort ScriptTimeoutSeconds { get; set; }

        public ScriptLimitsTag(int size)
            : base(TagType.ScriptLimits, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            MaxRecursionDepth = reader.ReadUI16();
            ScriptTimeoutSeconds = reader.ReadUI16();
        }
    }
}
