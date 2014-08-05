using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class CpoolInfo
    {
        [XmlArrayItem("Int")]
        public List<int> Integers { get; set; }

        internal CpoolList<int> ActualIntegers { get; set; }

        [XmlIgnore]
        public bool IntegersSpecified
        {
            get { return Integers.Count > 0; }
        }

        [XmlArrayItem("Uint")]
        public List<uint> UIntegers { get; set; }

        internal CpoolList<uint> ActualUIntegers { get; set; }

        [XmlIgnore]
        public bool UIntegersSpecified
        {
            get { return UIntegers.Count > 0; }
        }

        [XmlArrayItem("Double")]
        public List<double> Doubles { get; set; }

        internal CpoolList<double> ActualDoubles { get; set; }

        [XmlIgnore]
        public bool DoublesSpecified
        {
            get { return Doubles.Count > 0; }
        }

        [XmlArrayItem("String")]
        public List<string> Strings { get; set; }

        internal CpoolList<string> ActualStrings { get; set; }
        internal CpoolList<string> NamespaceStrings { get; set; }

        [XmlIgnore]
        public bool StringsSpecified
        {
            get { return Strings.Count > 0; }
        }

        [XmlArrayItem("Namespace")]
        public List<NamespaceInfo> Namespaces { get; set; }

        internal IList<NamespaceInfo> ActualNamespaces { get; set; }

        [XmlIgnore]
        public bool NamespacesSpecified
        {
            get { return Namespaces.Count > 0; }
        }

        [XmlArrayItem("NsSet")]
        public List<NsSet> NsSets { get; set; }

        internal CpoolList<NsSet> ActualNsSets { get; set; }

        [XmlIgnore]
        public bool NsSetsSpecified
        {
            get { return NsSets.Count > 0; }
        }

        [XmlArrayItem("QName", typeof(QName))]
        [XmlArrayItem("RTQName", typeof(RTQName))]
        [XmlArrayItem("RTQNameL", typeof(RTQNameL))]
        [XmlArrayItem("Multiname", typeof(Multiname))]
        [XmlArrayItem("MultinameL", typeof(MultinameL))]
        [XmlArrayItem("TypeName", typeof(TypeName))]
        public List<MultinameInfo> Multinames { get; set; }

        internal IList<MultinameInfo> ActualMultinames { get; set; }

        [XmlIgnore]
        public bool MultinamesSpecified
        {
            get { return Multinames.Count > 0; }
        }

        private void FromStream(BitReader reader)
        {
            var intCount = reader.ReadEncodedU32();
            if (intCount > 0) intCount--;
            Integers = new List<int>((int) intCount);
            for (int i = 0; i < intCount; i++)
            {
                Integers.Add(reader.ReadEncodedS32());
            }
            ActualIntegers = new CpoolList<int>(0, Integers);

            var uintCount = reader.ReadEncodedU32();
            if (uintCount > 0) uintCount--;
            UIntegers = new List<uint>((int) uintCount);
            for (int i = 0; i < uintCount; i++)
            {
                UIntegers.Add(reader.ReadEncodedU32());
            }
            ActualUIntegers = new CpoolList<uint>(0, UIntegers);

            var doubleCount = reader.ReadEncodedU32();
            if (doubleCount > 0) doubleCount--;
            Doubles = new List<double>((int) doubleCount);
            for (int i = 0; i < doubleCount; i++)
            {
                Doubles.Add(reader.ReadDouble());
            }
            ActualDoubles = new CpoolList<double>(double.NaN, Doubles);

            var stringCount = reader.ReadEncodedU32();
            if (stringCount > 0) stringCount--;
            Strings = new List<string>((int) stringCount);
            for (int i = 0; i < stringCount; i++)
            {
                Strings.Add(reader.ReadABCString());
            }
            ActualStrings = new CpoolList<string>(string.Empty, Strings);
            NamespaceStrings = new CpoolList<string>(NamespaceInfo.UndefinedNsname, Strings);

            var namespaceCount = reader.ReadEncodedU32();
            if (namespaceCount > 0) namespaceCount--;
            Namespaces = new List<NamespaceInfo>((int) namespaceCount);
            for (int i = 0; i < namespaceCount; i++)
            {
                Namespaces.Add(NamespaceInfo.CreateFromStream(reader, NamespaceStrings));
            }
            var defaultNamespace = NamespaceInfo.Undefined;
            ActualNamespaces = new CpoolList<NamespaceInfo>(defaultNamespace, Namespaces);

            var nsSetCount = reader.ReadEncodedU32();
            if (nsSetCount > 0) nsSetCount--;
            NsSets = new List<NsSet>((int) nsSetCount);
            for (int i = 0; i < nsSetCount; i++)
            {
                NsSets.Add(NsSet.CreateFromStream(reader, ActualNamespaces));
            }
            ActualNsSets = new CpoolList<NsSet>(null, NsSets);

            var multinameCount = reader.ReadEncodedU32();
            if (multinameCount > 0) multinameCount--;
            Multinames = new List<MultinameInfo>((int)multinameCount);
            ActualMultinames = new CpoolList<MultinameInfo>(null, Multinames);
            for (int i = 0; i < multinameCount; i++)
            {
                Multinames.Add(MultinameInfo.CreateFromStream(reader, NamespaceStrings, ActualNamespaces, ActualNsSets, ActualMultinames));
            }
        }

        internal static CpoolInfo CreateFromStream(BitReader reader)
        {
            var result = new CpoolInfo();
            result.FromStream(reader);
            return result;
        }

        internal void ToStream(BitWriter writer)
        {
            var intCount = Integers.Count;
            if (intCount > 0) intCount++;
            writer.WriteEncodedS32(intCount);
            foreach (var i in Integers)
            {
                writer.WriteEncodedS32(i);
            }

            var uintCount = UIntegers.Count;
            if (uintCount > 0) uintCount++;
            writer.WriteEncodedS32(uintCount);
            foreach (var ui in UIntegers)
            {
                writer.WriteEncodedU32(ui);
            }

            var doubleCount = Doubles.Count;
            if (doubleCount > 0) doubleCount++;
            writer.WriteEncodedS32(doubleCount);
            foreach (var d in Doubles)
            {
                writer.WriteDouble(d);
            }

            var stringCount = Strings.Count;
            if (stringCount > 0) stringCount++;
            writer.WriteEncodedS32(stringCount);
            foreach (var s in Strings)
            {
                writer.WriteABCString(s);
            }

            var namespaceCount = Namespaces.Count;
            if (namespaceCount > 0) namespaceCount++;
            writer.WriteEncodedS32(namespaceCount);
            foreach (var ns in Namespaces)
            {
                ns.ToStream(writer, NamespaceStrings);
            }

            var nsSetCount = NsSets.Count;
            if (nsSetCount > 0) nsSetCount++;
            writer.WriteEncodedS32(nsSetCount);
            foreach (var nsSet in NsSets)
            {
                nsSet.ToStream(writer, ActualNamespaces);
            }

            var multinameCount = Multinames.Count;
            if (multinameCount > 0) multinameCount++;
            writer.WriteEncodedS32(multinameCount);
            foreach (var multiname in Multinames)
            {
                multiname.ToStream(writer, NamespaceStrings, ActualNamespaces, ActualNsSets, ActualMultinames);
            }
        }
    }
}
