using Modele;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
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
using Appli.converters;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour Film.xaml
    /// </summary>
    public partial class EditionFilm : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;
        public Film Film { get; private set; } = new Film(null, App.defaultImage, null, null, 0, new TimeSpan(0, 0, 0), null, null, null);

        public EditionFilm(object sender)
        {
            InitializeComponent();
            EditionOeuvre main = sender as EditionOeuvre;
            if (main.Mode == ModeEdition.Creation)
            {
                main.Saved += Ajout_Film;
                //Adaptation du menu
                nonTermineRadio.IsChecked = true;
                nonCommenceRadio.IsChecked = true;
                nonRecommenceRadio.IsChecked = true;
            }

            if (main.Mode == ModeEdition.ModificationDeFilm)
            {
                main.Saved += Modif_Film;
                CopieFilm(Film, Manager.ElementSelectionne as Film);

                // Adaptation du menu
                if (Film.NbFini == 0)
                {
                    nonTermineRadio.IsChecked = true;
                    if (Film.Etat.HasValue) commenceRadio.IsChecked = true;
                    else nonCommenceRadio.IsChecked = true;
                }
                else
                {
                    termineRadio.IsChecked = true;
                    if (Film.Etat.HasValue) recommenceRadio.IsChecked = true;
                    else nonRecommenceRadio.IsChecked = true;
                }
                if (Film.DatePrevue.HasValue)
                {
                    planifieCheck.IsChecked = true;
                    Planifie.Opacity = 1;
                    Planifie.IsEnabled = true;
                }
            }

            DataContext = Film;
        }



        private void CopieFilm(Film oldF, Film newF)
        {
            Type typeFilm = typeof(Film);
            var filmProperties = typeFilm.GetProperties();
            foreach(var property in filmProperties.Where(ppty => ppty.CanWrite))
            {
                property.SetValue(oldF, property.GetValue(newF));
            }
        }


        private void Charge_Film() // Permet d'effectuer les chargements dans Film des champs qui ne peuvent être chargé par binding
        {
            if (int.TryParse(saisieHeureDuree.Text, out int dureeHeure)
             && int.TryParse(saisieMinuteDuree.Text, out int dureeMinute))
            {
                Film.Duree = new TimeSpan(dureeHeure, dureeMinute, 0);
            }
            else Film.Duree = new TimeSpan(0, 0, 0);

            // Cas selon la sélection //
            if (nonTermineRadio.IsChecked.Value)
            {
                Film.NbFini = 0;
                if (commenceRadio.IsChecked.Value)
                {
                    if (int.TryParse(saisieEtatHeure.Text, out int etatHeure)
                        && int.TryParse(saisieEtatMinute.Text, out int etatMinute))
                    {
                        Film.Etat = new TimeSpan(etatHeure, etatMinute, 0);
                    }
                    else Film.Etat = new TimeSpan(0, 0, 0);
                }
                else Film.Etat = null;
            }
            else if (termineRadio.IsChecked.Value)
            {

                if (uint.TryParse(saisieNbFini.Text, out uint nbfini)) Film.NbFini = nbfini;
                else Film.NbFini = 1;

                if (recommenceRadio.IsChecked.Value)
                {
                    if (int.TryParse(saisieEtatHeure.Text, out int etatHeure)
                        && int.TryParse(saisieEtatMinute.Text, out int etatMinute))
                    {
                        Film.Etat = new TimeSpan(etatHeure, etatMinute, 0);
                    }
                    else Film.Etat = new TimeSpan(0, 0, 0);
                }
                else Film.Etat = null;
            }
            if (nonTermineRadio.IsChecked.Value) Film.DateFini = null;

            if (!Planifie.IsEnabled) Film.DatePrevue = null;

            //------------------//
        }

        private void Ajout_Film(object sender, EventArgs e)
        {
            Charge_Film();

            var verif = ValidateurOeuvre.VerifFilmCree(Film);

            if (verif.Count() == 0)
            {
                Manager.AjouterOeuvre(Film);
                MessageBox.Show("Film créé !", "(Consultez la collection)", MessageBoxButton.OK);
                var parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show(ValidateurOeuvre.MessageInvalide(verif));
            }
        }

        private void Modif_Film(object sender, EventArgs e)
        {
            Charge_Film();

            var verif = ValidateurOeuvre.VerifFilmCree(Film);

            if (verif.Count() == 0)
            {
                CopieFilm(Manager.ElementSelectionne as Film, Film);
                MessageBox.Show("Film modifié !", "(Consultez la collection)", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(ValidateurOeuvre.MessageInvalide(verif));
            }

            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }



        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:\Users\Public\Pictures";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg | .gif | .png";
            dialog.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif"; // Filter files by extension 

            bool? result = dialog.ShowDialog();

            // Ouverture du document 
            if (result == true)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                string newName = fi.Name;
                
                int i = 0;
                while(File.Exists(System.IO.Path.Combine(App.imagesPath, newName)))
                {
                    i++;
                    newName = $"{newName.Remove(newName.LastIndexOf('.'))}-{i}{fi.Extension}";
                }
                
                File.Copy(dialog.FileName, System.IO.Path.Combine(App.imagesPath, newName));
                Film.Image = newName;
            }
        }
        private void Termine_Checked(object sender, RoutedEventArgs e)
        {
            progress.SetValue(Grid.RowProperty, 4);
            NonTermine.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Hidden;
            commenceRadio.IsChecked = false;
            nonRecommenceRadio.IsChecked = true;
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
            nonCommenceRadio.IsChecked = true;
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
