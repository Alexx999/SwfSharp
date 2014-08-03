using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class TypeName : MultinameInfo
    {
        private int _nameIndex;
        private List<int> _typeIndices;
        private IList<MultinameInfo> _multinames;
        private List<MultinameInfo> _types;
        private QName _name;

        [XmlElement]
        public QName Name
        {
            get { return _name ?? (_name = (QName) _multinames[_nameIndex]); }
            set { _name = value; }
        }

        [XmlElement("Type", typeof(QName), Namespace = "QName")]
        [XmlElement("Type", typeof(TypeName), Namespace = "TypeName")]
        public List<MultinameInfo> Types
        {
            get { return _types ?? (_types = GetTypes()); }
            set { _types = value; }
        }

        private List<MultinameInfo> GetTypes()
        {
            var result = new List<MultinameInfo>(_typeIndices.Count);
            foreach (int i in _typeIndices)
            {
                result.Add(_multinames[i]);
            }
            return result;
        }

        public TypeName()
            : base(MultinameKind.TypeName)
        {
        }

        private void FromStream(BitReader reader, IList<MultinameInfo> multinames)
        {
            _multinames = multinames;
            _nameIndex = reader.ReadEncodedS32();
            var count = reader.ReadEncodedS32();
            _typeIndices = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                _typeIndices.Add(reader.ReadEncodedS32());
            }
        }

        internal static TypeName CreateFromStream(BitReader reader, IList<MultinameInfo> multinames)
        {
            var result = new TypeName();
            result.FromStream(reader, multinames);
            return result;
        }

        internal override void ToStream(BitWriter writer, IList<string> strings, IList<NamespaceInfo> namespaces, IList<NsSet> nsSets, IList<MultinameInfo> multinames)
        {
            base.ToStream(writer, strings, namespaces, nsSets, multinames);
            writer.WriteEncodedS32(multinames.IndexOf(Name));
            writer.WriteEncodedS32(Types.Count);
            foreach (var type in Types)
            {
                writer.WriteEncodedS32(multinames.IndexOf(type));
            }
        }
    }
}