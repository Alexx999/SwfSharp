using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SwfSharp.Tags;

namespace SwfViewer
{
    [ValueConversion(typeof(TagType), typeof(String))]
    class TagTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            TagType result;
            Enum.TryParse(value as String, true, out result);
            return result;
        }
    }
}
