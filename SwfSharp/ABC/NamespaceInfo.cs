using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class NamespaceInfo
    {
        public const string UndefinedNsname = "*";
        public static readonly NamespaceInfo Undefined = new NamespaceInfo { Kind = NamespaceKind.Namespace, Name = UndefinedNsname };

        [XmlAttribute]
        public NamespaceKind Kind { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        private void FromStream(BitReader reader, IList<string> strings)
        {
            Kind = (NamespaceKind) reader.ReadUI8();
            Name = strings[reader.ReadEncodedS32()];
        }

        internal static NamespaceInfo CreateFromStream(BitReader reader, IList<string> strings)
        {
            var result = new NamespaceInfo();
            result.FromStream(reader, strings);
            return result;
        }

        internal void ToStream(BitWriter writer, IList<string> strings)
        {
            writer.WriteUI8((byte) Kind);
            writer.WriteEncodedS32(strings.IndexOf(Name));
        }
    }
}
