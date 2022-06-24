using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Appli.converters
{
    class String2NoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (float.TryParse(value as string, style, culture, out float note))
            {
                return note;
            }
            return null;
        }
    }
}
