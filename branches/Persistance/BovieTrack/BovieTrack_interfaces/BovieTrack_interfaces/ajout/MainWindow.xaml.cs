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

namespace Interface_Ajouter_Element_Collection
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Film_Click(object sender, RoutedEventArgs e)
        {
            if (film.IsChecked == true)
            {
                contentControl.Content = new Film();
                livreBorder.Visibility = Visibility.Hidden;
                serieBorder.Visibility = Visibility.Hidden;
                sauvegarder.Visibility = Visibility.Visible;

                filmBorder.IsEnabled = false;
            }
            else
            {
                Retour_Click(sender, e);
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (contentControl.Content is Film | contentControl.Content is Livre | contentControl.Content is Serie)
            {
                contentControl.Content = new Menu();
                livreBorder.Visibility = Visibility.Visible;
                serieBorder.Visibility = Visibility.Visible;
                filmBorder.Visibility = Visibility.Visible;
                sauvegarder.Visibility = Visibility.Hidden;

                film.IsChecked = false;
                serie.IsChecked = false;
                livre.IsChecked = false;

                filmBorder.IsEnabled = true;
                serieBorder.IsEnabled = true;
                livreBorder.IsEnabled = true;

            }
        }

        private void Serie_Click(object sender, RoutedEventArgs e)
        {
            if (serie.IsChecked == true)
            {
                contentControl.Content = new Serie();


                livreBorder.Visibility = Visibility.Hidden;
                filmBorder.Visibility = Visibility.Hidden;
                sauvegarder.Visibility = Visibility.Visible;

                serieBorder.IsEnabled = false;
            }
            else
            {
                Retour_Click(sender, e);
            }
        }

        private void Livre_Click(object sender, RoutedEventArgs e)
        {
            if (livre.IsChecked == true)
            {
                contentControl.Content = new Livre();


                serieBorder.Visibility = Visibility.Hidden;
                filmBorder.Visibility = Visibility.Hidden;
                sauvegarder.Visibility = Visibility.Visible;

                livreBorder.IsEnabled = false;
            }
            else
            {
                Retour_Click(sender, e);
            }
            
        }
    }
}
