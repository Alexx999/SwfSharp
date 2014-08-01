using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class CpoolInfo
    {
        [XmlArrayItem("Int")]
        public List<int> Integers { get; set; }

        [XmlIgnore]
        public bool IntegersSpecified
        {
            get { return Integers.Count > 0; }
        }

        [XmlArrayItem("Uint")]
        public List<uint> UIntegers { get; set; }

        [XmlIgnore]
        public bool UIntegersSpecified
        {
            get { return UIntegers.Count > 0; }
        }

        [XmlArrayItem("Double")]
        public List<double> Doubles { get; set; }

        [XmlIgnore]
        public bool DoublesSpecified
        {
            get { return Doubles.Count > 0; }
        }

        [XmlArrayItem("String")]
        public List<string> Strings { get; set; }

        [XmlIgnore]
        public bool StringsSpecified
        {
            get { return Strings.Count > 0; }
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
            var uintCount = reader.ReadEncodedU32();
            if (uintCount > 0) uintCount--;
            UIntegers = new List<uint>((int)uintCount);
            for (int i = 0; i < uintCount; i++)
            {
                UIntegers.Add(reader.ReadEncodedU32());
            }
            var doubleCount = reader.ReadEncodedU32();
            if (doubleCount > 0) doubleCount--;
            Doubles = new List<double>((int)doubleCount);
            for (int i = 0; i < doubleCount; i++)
            {
                Doubles.Add(reader.ReadDouble());
            }
            var stringCount = reader.ReadEncodedU32();
            if (stringCount > 0) stringCount--;
            Strings = new List<string>((int)stringCount);
            for (int i = 0; i < stringCount; i++)
            {
                Strings.Add(reader.ReadString(reader.ReadEncodedS32()));
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
        }
    }
}
