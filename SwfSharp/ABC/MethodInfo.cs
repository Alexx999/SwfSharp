using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class MethodInfo
    {
        [XmlElement("ReturnType", typeof(QName), Namespace = "QName")]
        [XmlElement("ReturnType", typeof(TypeName), Namespace = "TypeName")]
        public MultinameInfo ReturnType { get; set; }
        [XmlArrayItem("Param", typeof(QName), Namespace = "QName")]
        [XmlArrayItem("Param", typeof(TypeName), Namespace = "TypeName")]
        public List<MultinameInfo> Paramethers { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public MethodFlags Flags { get; set; }
        [XmlArrayItem("Option")]
        public OptionInfo Options { get; set; }
        [XmlArrayItem("Name")]
        public List<string> ParamNames { get; set; }

        private void FromStream(BitReader reader, CpoolInfo cpool)
        {
            var multinames = cpool.ActualMultinames;
            var strings = cpool.ActualStrings;
            var paramCount = reader.ReadEncodedS32();
            ReturnType = multinames[reader.ReadEncodedS32()];
            Paramethers = new List<MultinameInfo>(paramCount);
            for (int i = 0; i < paramCount; i++)
            {
                Paramethers.Add(multinames[reader.ReadEncodedS32()]);
            }
            Name = strings[reader.ReadEncodedS32()];
            Flags = (MethodFlags) reader.ReadUI8();
            if ((Flags & MethodFlags.HasOptional) != 0)
            {
                Options = OptionInfo.CreateFromStream(reader, cpool);
            }
            if ((Flags & MethodFlags.HasParamNames) != 0)
            {
                ParamNames = new List<string>(paramCount);
                for (int i = 0; i < paramCount; i++)
                {
                    ParamNames.Add(strings[reader.ReadEncodedS32()]);
                }
            }
        }

        internal static MethodInfo CreateFromStream(BitReader reader, CpoolInfo cpool)
        {
            var result = new MethodInfo();
            result.FromStream(reader, cpool);
            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool)
        {
            var multinames = cpool.ActualMultinames;
            var strings = cpool.ActualStrings;
            writer.WriteEncodedS32(Paramethers.Count);
            writer.WriteEncodedS32(multinames.IndexOf(ReturnType));
            foreach (var paramether in Paramethers)
            {
                writer.WriteEncodedS32(multinames.IndexOf(paramether));
            }
            writer.WriteEncodedS32(strings.IndexOf(Name));
            writer.WriteUI8((byte)Flags);
            if ((Flags & MethodFlags.HasOptional) != 0)
            {
                Options.ToStream(writer, cpool);
            }
            if ((Flags & MethodFlags.HasParamNames) != 0)
            {
                foreach (var name in ParamNames)
                {
                    writer.WriteEncodedS32(strings.IndexOf(name));
                }
            }
        }

        [Flags]
        public enum MethodFlags : byte
        {
            NeedArguments = 0x01,
            NeedActivation = 0x02,
            NeedRest = 0x04,
            HasOptional = 0x08,
            SetDxns = 0x40,
            HasParamNames = 0x80
        }
    }

    [Serializable]
    public class OptionInfo : List<OptionDetail>
    {
        public OptionInfo()
        {
        }

        public OptionInfo(int capacity)
            : base(capacity)
        {
        }

        private void FromStream(BitReader reader, int len, CpoolInfo cpool)
        {
            for (int i = 0; i < len; i++)
            {
                Add(OptionDetail.CreateFromStream(reader, cpool));
            }
        }

        internal static OptionInfo CreateFromStream(BitReader reader, CpoolInfo cpool)
        {
            int len = reader.ReadEncodedS32();

            var result = new OptionInfo(len);

            result.FromStream(reader, len, cpool);

            return result;
        }
        internal void ToStream(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(Count);
            foreach (var option in this)
            {
                option.ToStream(writer, cpool);
            }
        }
    }

    [Serializable]
    public class OptionDetail
    {
        [XmlAttribute]
        public ConstantKind Kind { get; set; }
        [XmlAttribute]
        public int ConstIndex { get; set; }

        private void FromStream(BitReader reader, CpoolInfo cpool)
        {
            ConstIndex = reader.ReadEncodedS32();
            Kind = (ConstantKind) reader.ReadUI8();
        }

        internal static OptionDetail CreateFromStream(BitReader reader, CpoolInfo cpool)
        {
            var result = new OptionDetail();

            result.FromStream(reader, cpool);

            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool)
        {
            writer.WriteEncodedS32(ConstIndex);
            writer.WriteUI8((byte) Kind);
        }
    }

    public enum ConstantKind
    {
        Int = 0x03,
        UInt = 0x04,
        Double = 0x06,
        Utf8 = 0x01,
        True = 0x0B,
        False = 0x0A,
        Null = 0x0C,
        Undefined = 0x00,
        Namespace = 0x08,
        PackageNamespace = 0x16,
        PackageInternalNs = 0x17,
        ProtectedNamespace = 0x18,
        ExplicitNamespace = 0x19,
        StaticProtectedNs = 0x1A,
        PrivateNs = 0x05
    }
}
