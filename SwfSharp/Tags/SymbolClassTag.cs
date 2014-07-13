using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class SymbolClassTag : SwfTag
    {
        public IList<Symbol> Symbols { get; set; }

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


        public class Symbol
        {
            public Symbol(ushort tagId, string name)
            {
                TagId = tagId;
                Name = name;
            }

            public ushort TagId { get; set; }
            public string Name { get; set; }
        }
    }
}
