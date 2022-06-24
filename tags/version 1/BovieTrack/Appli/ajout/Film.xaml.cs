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
    /// Logique d'interaction pour Film.xaml
    /// </summary>
    public partial class EditionFilm : UserControl
    {
        // Champs du film 
        // (on ne peut pas utiliser une instance de Film directement car des exceptions risques d'empêcher la création)
        public string nom { get; private set; }
        public string image { get; private set; }
        public DateTime? dateFini { get; private set; }
        public DateTime? datePrevue { get; private set; }
        public uint nbFini { get; private set; }
        public TimeSpan duree { get; private set; }
        public TimeSpan? etat { get; private set; }
        public float? note { get; private set; }
        public string commentaire { get; private set; }


        public EditionFilm()
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
                saisieIcone_film.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }


        private void Termine_Checked(object sender, RoutedEventArgs e)
        {
            progress.SetValue(Grid.RowProperty, 4);
            NonTermine.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Hidden;
            Reboot_Commence.IsChecked = false;
            Reboot_recommence.IsChecked = false;

            LastView.Visibility = Visibility.Visible;
            Vu.Visibility = Visibility.Visible;
            Recommence.Visibility = Visibility.Visible;
        }
        private void NonTermine_Checked(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Visibility.Hidden;
            progress.SetValue(Grid.RowProperty, 2);
            LastView.Visibility = Visibility.Hidden;
            Vu.Visibility = Visibility.Hidden;
            Recommence.Visibility = Visibility.Hidden;

            NonTermine.Visibility = Visibility.Visible;
        }
        private void Commence_Checked(object sender, RoutedEventArgs e)
        {
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
