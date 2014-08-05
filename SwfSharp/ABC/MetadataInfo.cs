using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.ABC
{
    [Serializable]
    public class MetadataInfo
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement("Item")]
        public List<ItemInfo> Items { get; set; }

        private void FromStream(BitReader reader, CpoolInfo cpool)
        {
            var strings = new CpoolList<string>(null, cpool.Strings);
            Name = strings[reader.ReadEncodedS32()];
            var itemCount = reader.ReadEncodedS32();
            Items = new List<ItemInfo>(itemCount);
            for (int i = 0; i < itemCount; i++)
            {
                Items.Add(new ItemInfo(strings[reader.ReadEncodedS32()], strings[reader.ReadEncodedS32()]));
            }
        }

        internal static MetadataInfo CreateFromStream(BitReader reader, CpoolInfo cpool)
        {
            var result = new MetadataInfo();
            result.FromStream(reader, cpool);
            return result;
        }

        internal void ToStream(BitWriter writer, CpoolInfo cpool)
        {
            var strings = new CpoolList<string>(null, cpool.Strings);
            writer.WriteEncodedS32(strings.IndexOf(Name));
            writer.WriteEncodedS32(Items.Count);
            foreach (var item in Items)
            {
                writer.WriteEncodedS32(strings.IndexOf(item.Key));
                writer.WriteEncodedS32(strings.IndexOf(item.Value));
            }
        }

        [Serializable]
        public class ItemInfo
        {
            [XmlAttribute]
            public string Key { get; set; }
            [XmlAttribute]
            public string Value { get; set; }

            private ItemInfo()
            {}

            public ItemInfo(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
