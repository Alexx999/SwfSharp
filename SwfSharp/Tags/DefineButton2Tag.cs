using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineButton2Tag : SwfTag
    {
        [XmlAttribute]
        public ushort ButtonId { get; set; }
        [XmlAttribute]
        public bool TrackAsMenu { get; set; }
        [XmlArrayItem("ButtonRecord")]
        public List<ButtonRecordStruct> Characters { get; set; }
        [XmlArrayItem("ButtonCondAction")]
        public List<ButtonCondActionStruct> Actions { get; set; }

        public DefineButton2Tag() : this(0)
        {
        }

        public DefineButton2Tag(int size)
            : base(TagType.DefineButton2, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            ButtonId = reader.ReadUI16();
            reader.ReadBits(7);
            TrackAsMenu = reader.ReadBoolBit();
            var actionOffset = reader.ReadUI16();
            Characters = new List<ButtonRecordStruct>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Characters.Add(ButtonRecordStruct.CreateFromStream(reader, TagType, swfVersion));
                nextFlag = reader.ReadUI8();
            }
            if(actionOffset == 0) return;
            Actions = new List<ButtonCondActionStruct>();
            ButtonCondActionStruct lastAction;
            do
            {
                lastAction = ButtonCondActionStruct.CreateFromStream(reader);
                Actions.Add(lastAction);

            } while (lastAction.CondActionSize > 0);
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16(ButtonId);
            writer.WriteBits(7, 0);
            writer.WriteBoolBit(TrackAsMenu);
            var ms = new MemoryStream();
            using (var charWriter = new BitWriter(ms, true))
            {
                foreach (var character in Characters)
                {
                    character.ToStream(charWriter, TagType, swfVersion);
                }
                charWriter.WriteUI8(0);
            }
            ushort actionOffset;
            if (Actions == null || Actions.Count == 0)
            {
                actionOffset = 0;
            }
            else
            {
                actionOffset = (ushort)(ms.Position + 2);
            }
            writer.WriteUI16(actionOffset);
            writer.WriteBytes(ms.GetBuffer(), 0, (int) ms.Position);
            if (actionOffset == 0) return;
            foreach (var action in Actions)
            {
                action.ToStream(writer, swfVersion);
            }
        }
    }
}
