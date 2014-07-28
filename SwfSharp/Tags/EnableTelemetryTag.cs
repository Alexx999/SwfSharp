using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class EnableTelemetryTag : SwfTag
    {
        public byte[] PasswordHash { get; set; }

        public EnableTelemetryTag() : this(0)
        {
        }

        public EnableTelemetryTag(int size)
            : base(TagType.EnableTelemetry, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            reader.ReadUI16();
            if (Size > 2)
            {
                PasswordHash = reader.ReadBytes(32);
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(0);
            if (PasswordHash != null)
            {
                writer.WriteBytes(PasswordHash);
            }
        }
    }
}
