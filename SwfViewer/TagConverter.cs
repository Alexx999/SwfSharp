using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.AvalonEdit.Document;
using SwfSharp.ShapeRecords;
using SwfSharp.Tags;

namespace SwfViewer
{
    [ValueConversion(typeof(SwfTag), typeof(String))]
    class TagConverter : IValueConverter
    {
        private static ConcurrentDictionary<Type, XmlSerializer> _serializerCache = new ConcurrentDictionary<Type, XmlSerializer>();
        private static Type[] _additionalTypes;
        private static XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var stopwatch = Stopwatch.StartNew();
            var builder = new StringBuilder();
            var xws = new XmlWriterSettings {OmitXmlDeclaration = true, Indent = true, Encoding = Encoding.UTF8, NamespaceHandling = NamespaceHandling.OmitDuplicates};
            var xtw = XmlWriter.Create(builder, xws);
            var ser = _serializerCache.GetOrAdd(value.GetType(), SerializerFactory);
            ser.Serialize(xtw, value, _namespaces);
            var str = builder.ToString();
            Debug.WriteLine("Tag serialized in " + stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + "ms");
            if (targetType == typeof (TextDocument))
            {
                return new TextDocument(str);
            }
            return str;
        }

        private XmlSerializer SerializerFactory(Type type)
        {
            if (_additionalTypes == null)
            {
                Assembly myAssembly = typeof (SwfTag).GetTypeInfo().Assembly;
                _additionalTypes = myAssembly.DefinedTypes.AsParallel()
                    .Where(t => t.IsSubclassOf(typeof (SwfTag))).Select(t => t.AsType()).ToArray();
            }
            return new XmlSerializer(type, _additionalTypes);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported");
        }
    }
}
