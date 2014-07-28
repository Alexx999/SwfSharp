using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Xml.Serialization;
using SwfSharp.ShapeRecords;
using SwfSharp.Tags;

namespace SwfViewer
{
    [ValueConversion(typeof(SwfTag), typeof(String))]
    class TagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var builder = new StringBuilder();
            var xws = new XmlWriterSettings {OmitXmlDeclaration = true, Indent = true, Encoding = Encoding.UTF8, NamespaceHandling = NamespaceHandling.OmitDuplicates};
            var xtw = XmlWriter.Create(builder, xws);
            Assembly myAssembly = typeof(SwfTag).GetTypeInfo().Assembly;
            var additionalClasses = myAssembly.DefinedTypes.AsParallel()
                .Where(t => t.IsSubclassOf(typeof (SwfTag))).Select(t => t.AsType()).ToArray();
            var ser = new XmlSerializer(value.GetType(), additionalClasses);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });
            ser.Serialize(xtw, value, namespaces);
            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported");
        }
    }
}
