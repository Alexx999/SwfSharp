using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class InstanceInfo
    {
        [XmlElement]
        public QName Name { get; set; }
        [XmlElement]
        public QName SuperName { get; set; }
        [XmlAttribute]
        public InstanceFlags Flags { get; set; }
        [XmlElement]
        public NamespaceInfo ProtectedNs { get; set; }
        [XmlArrayItem("Interface")]
        public List<MultinameInfo> Interfaces { get; set; }
        [XmlElement]
        public MethodInfo IInit { get; set; }
        [XmlArrayItem("TraitSlot", typeof(TraitSlot))]
        [XmlArrayItem("TraitMethod", typeof(TraitMethod))]
        [XmlArrayItem("TraitClass", typeof(TraitClass))]
        [XmlArrayItem("TraitFunction", typeof(TraitFunction))]
        public List<TraitsInfo> Traits { get; set; }

        private void FromStream(BitReader reader, CpoolInfo cpool, IList<MethodInfo> methods, IList<MetadataInfo> metadata)
        {
            var multinames = cpool.ActualMultinames;
            var namespaces = cpool.ActualNamespaces;
            Name = (QName) multinames[reader.ReadEncodedS32()];
            SuperName = (QName) multinames[reader.ReadEncodedS32()];
            Flags = (InstanceFlags) reader.ReadUI8();
            if ((Flags & InstanceFlags.ProtectedNs) != 0)
            {
                ProtectedNs = namespaces[reader.ReadEncodedS32()];
            }
            var interfaceCount = reader.ReadEncodedS32();
            Interfaces = new List<MultinameInfo>(interfaceCount);
            for (int i = 0; i < interfaceCount; i++)
            {
                Interfaces.Add(multinames[reader.ReadEncodedS32()]);
            }
            IInit = methods[reader.ReadEncodedS32()];
            var traitCount = reader.ReadEncodedS32();
            Traits = new List<TraitsInfo>(traitCount);
            for (int i = 0; i < traitCount; i++)
            {
                Traits.Add(TraitsInfo.CreateFromStream(reader, cpool, metadata));
            }
        }

        internal static InstanceInfo CreateFromStream(BitReader reader, CpoolInfo cpool, IList<MethodInfo> methods, IList<MetadataInfo> metadata)
        {
            var result = new InstanceInfo();
            result.FromStream(reader, cpool, methods, metadata);
            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool, IList<MethodInfo> methods, IList<MetadataInfo> metadata)
        {
            var multinames = cpool.ActualMultinames;
            var namespaces = cpool.ActualNamespaces;
            writer.WriteEncodedS32(multinames.IndexOf(Name));
            writer.WriteEncodedS32(multinames.IndexOf(SuperName));
            writer.WriteUI8((byte)Flags);
            if ((Flags & InstanceFlags.ProtectedNs) != 0)
            {
                writer.WriteEncodedS32(namespaces.IndexOf(ProtectedNs));
            }
            writer.WriteEncodedS32(Interfaces.Count);
            foreach (var multinameInfo in Interfaces)
            {
                writer.WriteEncodedS32(multinames.IndexOf(multinameInfo));
            }
            writer.WriteEncodedS32(methods.IndexOf(IInit));
            writer.WriteEncodedS32(Traits.Count);
            foreach (var trait in Traits)
            {
                trait.ToStream(writer, cpool, metadata);
            }
        }
    }
}
