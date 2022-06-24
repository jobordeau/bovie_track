using System;
using System.Windows;
using Modele;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class EditionOeuvre : Window
    {
        public ModeEdition Mode { get; private set; } // Permet de connaître l'état actuel de l'application ( départ non défini )

        public EditionOeuvre(ModeEdition mode)
        {
            InitializeComponent();
            Mode = mode;
            if (Mode == ModeEdition.ModificationDeFilm) film.IsChecked = true;
            if (Mode == ModeEdition.ModificationDeLivre) livre.IsChecked = true;
            if (Mode == ModeEdition.ModificationDeSerie) serie.IsChecked = true;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (contentControl.Content is EditionFilm | contentControl.Content is EditionLivre | contentControl.Content is EditionSerie)
            {
                if((Mode & ModeEdition.Modification) == ModeEdition.Modification)
                {
                    Close();
                }

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
            else
            {
                Close();
            }
        }

        public void Film_Click(object sender, RoutedEventArgs e)
        {
            if (film.IsChecked == true)
            {
                contentControl.Content = new EditionFilm(this);
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

        public void Serie_Click(object sender, RoutedEventArgs e)
        {
            if (serie.IsChecked == true)
            {
                contentControl.Content = new EditionSerie(this);
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

        public void Livre_Click(object sender, RoutedEventArgs e)
        {
            if (livre.IsChecked == true)
            {
                contentControl.Content = new EditionLivre(this);
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


        public event EventHandler<EventArgs> Saved;
        void OnSaved() => Saved?.Invoke(this, new EventArgs());
        private void Saved_Button(object sender, RoutedEventArgs e) => OnSaved();

    }
}
