using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class MultinameL : MultinameInfo
    {
        [XmlElement]
        public NsSet NsSet { get; set; }

        private MultinameL()
            : base(MultinameKind.MultinameL)
        {}

        public MultinameL(MultinameKind kind)
            : base(kind)
        {
        }

        private void FromStream(BitReader reader, IList<NsSet> nsSets)
        {
            NsSet = nsSets[reader.ReadEncodedS32()];
        }

        internal static MultinameL CreateFromStream(BitReader reader, MultinameKind kind, IList<NsSet> nsSets)
        {
            var result = new MultinameL(kind);
            result.FromStream(reader, nsSets);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            base.ToStream(writer, strings, namespaces, nsSets, multinames);
            writer.WriteEncodedS32(nsSets.IndexOf(NsSet));
        }
    }
}