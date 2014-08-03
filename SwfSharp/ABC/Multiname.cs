using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class Multiname : MultinameInfo
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public NsSet NsSet { get; set; }

        private Multiname()
            : base(MultinameKind.Multiname)
        {
        }

        public Multiname(MultinameKind kind)
            : base(kind)
        {
        }

        private void FromStream(BitReader reader, IList<string> strings, IList<NsSet> nsSets)
        {
            Name = strings[reader.ReadEncodedS32()];
            NsSet = nsSets[reader.ReadEncodedS32()];
        }

        internal static Multiname CreateFromStream(BitReader reader, MultinameKind kind, IList<string> strings, IList<NsSet> nsSets)
        {
            var result = new Multiname(kind);
            result.FromStream(reader, strings, nsSets);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            base.ToStream(writer, strings, namespaces, nsSets, multinames);
            writer.WriteEncodedS32(strings.IndexOf(Name));
            writer.WriteEncodedS32(nsSets.IndexOf(NsSet));
        }
    }
}