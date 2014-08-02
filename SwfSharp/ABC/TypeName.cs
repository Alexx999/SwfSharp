using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class TypeName : MultinameInfo
    {
        [XmlAttribute]
        public string NameBase { get; set; }
        [XmlAttribute]
        public string NameParameter { get; set; }

        public TypeName()
            : base(MultinameKind.TypeName)
        {
        }

        private void FromStream(BitReader reader, IList<string> strings)
        {
            NameBase = strings[reader.ReadEncodedS32()];
            var count = reader.ReadEncodedS32();
            NameParameter = strings[reader.ReadEncodedS32()];
        }

        internal static TypeName CreateFromStream(BitReader reader, IList<string> strings)
        {
            var result = new TypeName();
            result.FromStream(reader, strings);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets)
        {
            base.ToStream(writer, strings, namespaces, nsSets);
            writer.WriteEncodedS32(strings.IndexOf(NameBase));
            writer.WriteEncodedU32(1);
            writer.WriteEncodedS32(strings.IndexOf(NameParameter));
        }
    }
}