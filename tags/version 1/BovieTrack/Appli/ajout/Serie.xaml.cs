using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour Serie.xaml
    /// </summary>
    public partial class EditionSerie : UserControl
    {
        public EditionSerie()
        {
            InitializeComponent();
        }

        private void changeImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Image";
            dialog.DefaultExt = ".jpg | .gif | .png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                icone_film.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }

        private void Termine_Checked(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Visibility.Hidden;
            Reboot_recommence.IsChecked = false;

            LastView.Visibility = Visibility.Visible;
            Vu.Visibility = Visibility.Visible;
            Recommence.Visibility = Visibility.Visible;

        }
        private void NonTermine_Checked(object sender, RoutedEventArgs e)
        {
            progress.SetValue(Grid.RowProperty, 1);
            LastView.Visibility = Visibility.Hidden;
            Vu.Visibility = Visibility.Hidden;
            Recommence.Visibility = Visibility.Hidden;

            progress.Visibility = Visibility.Visible;
        }

        private void Commence_Checked(object sender, RoutedEventArgs e)
        {
            progress.SetValue(Grid.RowProperty, 4);
            progress.Visibility = Visibility.Visible;

        }

        private void NonCommence_Checked(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Visibility.Hidden;
        }

        private void Planifie_Click(object sender, RoutedEventArgs e)
        {
            if (Planifie.IsEnabled == true)
            {
                Planifie.Opacity = 0.5;
                Planifie.IsEnabled = false;
            }
            else
            {
                Planifie.Opacity = 1;
                Planifie.IsEnabled = true;
            }


        }


    }
}
