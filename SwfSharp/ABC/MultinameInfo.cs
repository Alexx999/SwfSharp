using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public abstract class MultinameInfo
    {
        [XmlAttribute]
        public MultinameKind Kind { get; set; }
        protected MultinameInfo(MultinameKind kind)
        {
            Kind = kind;
        }

        internal static MultinameInfo CreateFromStream(BitReader reader, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            MultinameInfo result;
            var kind = (MultinameKind)reader.ReadUI8();
            switch (kind)
            {
                case MultinameKind.QName:
                case MultinameKind.QNameA:
                {
                    result = QName.CreateFromStream(reader, kind, strings, namespaces);
                    break;
                }
                case MultinameKind.RTQName:
                case MultinameKind.RTQNameA:
                {
                    result = RTQName.CreateFromStream(reader, kind, strings);
                    break;
                }
                case MultinameKind.RTQNameL:
                case MultinameKind.RTQNameLA:
                {
                    result = RTQNameL.CreateFromStream(reader, kind);
                    break;
                }
                case MultinameKind.Multiname:
                case MultinameKind.MultinameA:
                {
                    result = Multiname.CreateFromStream(reader, kind, strings, nsSets);
                    break;
                }
                case MultinameKind.MultinameL:
                case MultinameKind.MultinameLA:
                {
                    result = MultinameL.CreateFromStream(reader, kind, nsSets);
                    break;
                }
                case MultinameKind.TypeName:
                {
                    result = TypeName.CreateFromStream(reader, multinames);
                    break;
                }
                default:
                {
                    throw new InvalidDataException("Bad Multiname Kind");
                }
            }
            return result;
        }

        internal virtual void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            writer.WriteUI8((byte)Kind);
        }
    }
}