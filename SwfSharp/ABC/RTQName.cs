using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class RTQName : MultinameInfo
    {
        [XmlAttribute]
        public string Name { get; set; }

        private RTQName()
            : base(MultinameKind.RTQName)
        {}

        public RTQName(MultinameKind kind)
            : base(kind)
        {
        }

        private void FromStream(BitReader reader, IList<string> strings)
        {
            Name = strings[reader.ReadEncodedS32()];
        }

        internal static RTQName CreateFromStream(BitReader reader, MultinameKind kind, IList<string> strings)
        {
            var result = new RTQName(kind);
            result.FromStream(reader, strings);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            base.ToStream(writer, strings, namespaces, nsSets, multinames);
            writer.WriteEncodedS32(strings.IndexOf(Name));
        }
    }
}