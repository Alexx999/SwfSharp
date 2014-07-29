using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineButtonTag : DoActionTag
    {
        [XmlAttribute]
        public ushort ButtonId { get; set; }
        [XmlElement("ButtonRecord")]
        public List<ButtonRecordStruct> Characters { get; set; }

        public DefineButtonTag() : this(0)
        {
        }

        public DefineButtonTag(int size)
            : base(TagType.DefineButton, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            Characters = new List<ButtonRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Characters.Add(ButtonRecordStruct.CreateFromStream(reader, TagType, swfVersion));
                nextFlag = reader.ReadUI8();
            }
            base.FromStream(reader, swfVersion);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ButtonId);
            foreach (var character in Characters)
            {
                character.ToStream(writer, TagType, swfVersion);
            }
            writer.WriteUI8(0);
            base.ToStream(writer, swfVersion);
        }
    }
}
