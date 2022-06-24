using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Modele;
using System.Windows.Data;

namespace Appli.converters
{
    class Etat2StringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "f":
                    if ((values[0] is TimeSpan? || values[0] is null) && values[1] is uint)
                    {
                        TimeSpan? etatFilm = values[0] as TimeSpan?;
                        if (etatFilm.HasValue) return $"{etatFilm.Value:hh\\hmm}";
                        if ((uint)values[1] == 0) return "Non commencé";
                        return "Terminé";
                    }
                    break;
                case "l":
                    if ((values[0] is int? || values[0] is null) && values[1] is uint)
                    {
                        int? etatLivre = values[0] as int?;
                        if (etatLivre.HasValue) return $"page {etatLivre}";
                        if ((uint)values[1] == 0) return "Non commencé";
                        return "Terminé";
                    }
                    break;
                case "s":
                    if ((values[0] is Serie.EtatSerie || values[0] is null) && values[1] is uint)
                    {
                        Serie.EtatSerie etatSerie = values[0] as Serie.EtatSerie;
                        if (!(etatSerie is null)) return $"Saison {etatSerie.Saison}\nÉpisode {etatSerie.Episode}";
                        if ((uint)values[1] == 0) return "Non commencé";
                        return "Terminé";
                    }
                    break;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
