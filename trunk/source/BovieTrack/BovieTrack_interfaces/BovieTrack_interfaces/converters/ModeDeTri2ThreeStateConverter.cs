using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Modele;
using System.Windows.Data;

namespace Appli.converters
{
    class ModeDeTri2ThreeStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ModeDeTri mode)
            {
                if (mode == ModeDeTri.DateDecroissante) return true ;
                if (mode == ModeDeTri.DateCroissante) return false;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool state )
            {
                if (state) return ModeDeTri.DateDecroissante;
                if (!state) return ModeDeTri.DateCroissante;
            }
            return ModeDeTri.Alphabetique;
        }
    }
}
