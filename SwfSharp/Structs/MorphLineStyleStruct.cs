using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Structs
{
    public class MorphLineStyleStruct
    {
        public ushort StartWidth { get; set; }
        public ushort EndWidth { get; set; }
        public RgbaStruct StartColor { get; set; }
        public RgbaStruct EndColor { get; set; }

        private void FromStream(BitReader reader)
        {
            StartWidth = reader.ReadUI16();
            EndWidth = reader.ReadUI16();
            StartColor = RgbaStruct.CreateFromStream(reader);
            EndColor = RgbaStruct.CreateFromStream(reader);
        }

        internal static MorphLineStyleStruct CreateFromStream(BitReader reader)
        {
            var result = new MorphLineStyleStruct();

            result.FromStream(reader);

            return result;
        }

        internal virtual void ToStream(BitWriter writer)
        {
            writer.WriteUI16(StartWidth);
            writer.WriteUI16(EndWidth);
            StartColor.ToStream(writer);
            EndColor.ToStream(writer);
        }
    }
}
