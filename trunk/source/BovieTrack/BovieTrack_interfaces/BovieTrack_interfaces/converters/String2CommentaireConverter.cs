using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Appli.converters
{
    class String2CommentaireConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string commentaire = value as string;

            if (string.IsNullOrWhiteSpace(commentaire)) return null;
            return commentaire;
        }
    }
}
