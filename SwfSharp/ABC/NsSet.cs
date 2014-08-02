using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class NsSet
    {
        [XmlElement("Namespace")]
        public List<NamespaceInfo> Namespaces { get; set; }

        private void FromStream(BitReader reader, IList<NamespaceInfo> namespaces)
        {
            var count = reader.ReadEncodedS32();
            Namespaces = new List<NamespaceInfo>(count);
            for (int i = 0; i < count; i++)
            {
                Namespaces.Add(namespaces[reader.ReadEncodedS32()]);
            }
        }

        internal static NsSet CreateFromStream(BitReader reader, IList<NamespaceInfo> namespaces)
        {
            var result = new NsSet();
            result.FromStream(reader, namespaces);
            return result;
        }

        internal void ToStream(BitWriter writer, IList<NamespaceInfo> namespaces)
        {
            writer.WriteEncodedS32(Namespaces.Count);
            foreach (var ns in Namespaces)
            {
                writer.WriteEncodedS32(namespaces.IndexOf(ns));
            }
        }
    }
}