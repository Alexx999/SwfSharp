using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class QName : MultinameInfo
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public NamespaceInfo Namespace { get; set; }

        private QName()
            : base(MultinameKind.QName)
        {}

        public QName(MultinameKind kind)
            : base(kind)
        {
        }

        private void FromStream(BitReader reader, IList<string> strings, IList<NamespaceInfo> namespaces)
        {
            Namespace = namespaces[reader.ReadEncodedS32()];
            Name = strings[reader.ReadEncodedS32()];
        }

        internal static QName CreateFromStream(BitReader reader, MultinameKind kind, IList<string> strings, IList<NamespaceInfo> namespaces)
        {
            var result = new QName(kind);
            result.FromStream(reader, strings, namespaces);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            base.ToStream(writer, strings, namespaces, nsSets, multinames);
            writer.WriteEncodedS32(namespaces.IndexOf(Namespace));
            writer.WriteEncodedS32(strings.IndexOf(Name));
        }
    }
}