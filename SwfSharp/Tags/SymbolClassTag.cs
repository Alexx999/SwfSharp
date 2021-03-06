﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class SymbolClassTag : SwfTag
    {
        [XmlElement("Symbol")]
        public List<Symbol> Symbols { get; set; }

        public SymbolClassTag() : this(0)
        {
        }

        public SymbolClassTag(int size)
            : base(TagType.SymbolClass, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            var count = reader.ReadUI16();
            Symbols = new List<Symbol>(count);
            for (int i = 0; i < count; i++)
            {
                Symbols.Add(new Symbol(reader.ReadUI16(), reader.ReadString()));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteUI16((ushort) Symbols.Count);
            foreach (var symbol in Symbols)
            {
                writer.WriteUI16(symbol.TagId);
                writer.WriteString(symbol.Name, swfVersion);
            }
        }

        [Serializable]
        public class Symbol
        {
            [XmlAttribute]
            public ushort TagId { get; set; }
            [XmlAttribute]
            public string Name { get; set; }

            private Symbol()
            {}

            public Symbol(ushort tagId, string name)
            {
                TagId = tagId;
                Name = name;
            }
        }
    }
}
