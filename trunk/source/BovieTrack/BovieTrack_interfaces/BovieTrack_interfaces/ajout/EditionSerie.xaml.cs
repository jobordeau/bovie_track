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
using Modele;
using Appli.converters;
using System.IO;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour Serie.xaml
    /// </summary>
    public partial class EditionSerie : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;
        public Serie Serie { get; private set; } = new Serie(null, App.defaultImage, null, null, 0, 1, null, null, new float?[] { null }, new string[] { null });

        public EditionSerie(object sender)
        {
            InitializeComponent();
            EditionOeuvre main = sender as EditionOeuvre;
            if (main.Mode == ModeEdition.Creation)
            {
                main.Saved += Ajout_Serie;
                //Adaptation du menu
                nonTermineRadio.IsChecked = true;
                nonCommenceRadio.IsChecked = true;
                nonRecommenceRadio.IsChecked = true;
            }

            if (main.Mode == ModeEdition.ModificationDeSerie)
            {
                main.Saved += Modif_Serie;
                CopieSerie(Serie, Manager.ElementSelectionne as Serie);
                // Les types références copiés dans Livre risque de changer involontairement ce de ElementSelectionne donc :
                List<Avis> newAvis = new List<Avis>();
                foreach (Avis a in Serie.ListeAvis)
                {
                    if (a != null) newAvis.Add(new Avis(a.Note, a.Commentaire));
                    else newAvis.Add(null);
                }
                Serie.ListeAvis = newAvis;


                // Adaptation du menu
                if (Serie.NbFini == 0)
                {
                    nonTermineRadio.IsChecked = true;
                    if (Serie.Etat != null) commenceRadio.IsChecked = true;
                    else nonCommenceRadio.IsChecked = true;
                }
                else
                {
                    termineRadio.IsChecked = true;
                    if (Serie.Etat != null) recommenceRadio.IsChecked = true;
                    else nonRecommenceRadio.IsChecked = true;
                }

                if (Serie.DatePrevue.HasValue)
                {
                    planifieCheck.IsChecked = true;
                    Planifie.Opacity = 1;
                    Planifie.IsEnabled = true;
                }
                //if (Serie.Etat is null ) Serie.Etat = new Serie.EtatSerie(0, 0); // Pour que le binding fonctionne et que l'on puisse mettre un état

            }
            DataContext = Serie;
        }


        private void CopieSerie(Serie oldS, Serie newS)
        {
            Type typeSerie = typeof(Serie);
            var serieProperties = typeSerie.GetProperties();
            foreach (var property in serieProperties.Where(ppty => ppty.CanWrite))
            {
                property.SetValue(oldS, property.GetValue(newS));
            }
        }

        private void Charge_Serie()
        {

            if (int.TryParse(saisieEtatSaison.Text, out int etatSaison) &&
                int.TryParse(saisieEtatEpisode.Text, out int etatEpisode)) Serie.Etat = new Serie.EtatSerie(etatSaison, etatEpisode);

            // Cas selon la sélection //
                if (nonTermineRadio.IsChecked.Value)
            {
                Serie.NbFini = 0;
                if (nonCommenceRadio.IsChecked.Value)
                {
                    Serie.Etat = null;
                }

            }
            else if (termineRadio.IsChecked.Value)
            {
                if (uint.TryParse(saisieNbFini.Text, out uint nbfini)) Serie.NbFini = nbfini;
                else Serie.NbFini = 1;

                if (nonRecommenceRadio.IsChecked.Value)
                {
                    Serie.Etat = null;
                }
            }
            if (nonTermineRadio.IsChecked.Value) Serie.DateFini = null;

            if (!Planifie.IsEnabled) Serie.DatePrevue = null;

            //------------------//
        }

        private void Ajout_Serie(object sender, EventArgs e)
        {
            Charge_Serie();

            var verif = ValidateurOeuvre.VerifSerieCreee(Serie);

            if (verif.Count() == 0)
            {
                Manager.AjouterOeuvre(Serie);
                MessageBox.Show("Série créée !", "(Consulte la collection)", MessageBoxButton.OK);
                var parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show(ValidateurOeuvre.MessageInvalide(verif));
            }
            
        }

        private void Modif_Serie(object sender, EventArgs e)
        {
            Charge_Serie();

            var verif = ValidateurOeuvre.VerifSerieCreee(Serie);

            if (verif.Count() == 0)
            {
                CopieSerie(Manager.ElementSelectionne as Serie, Serie);
                MessageBox.Show("Série modifiée !", "(Consulte la collection)", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(ValidateurOeuvre.MessageInvalide(verif));
            }

            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }


        private void ComboboxSaisonsModif_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comme on change de saison il faut changer le binding du commentaire et de la note pour qu'ils correspondent à la saison sélectionnée

            Binding noteBinding = new Binding($"ListeAvis[{comboboxSaisonsModif.SelectedIndex}].Note");
            noteBinding.Source = Serie;
            noteBinding.Converter = new String2NoteConverter();
            BindingOperations.SetBinding(textBoxNote, TextBox.TextProperty, noteBinding);

            Binding commentaireBinding = new Binding($"ListeAvis[{comboboxSaisonsModif.SelectedIndex}].Commentaire");
            commentaireBinding.Source = Serie;
            commentaireBinding.Converter = new String2CommentaireConverter();
            BindingOperations.SetBinding(textBoxComment, TextBox.TextProperty, commentaireBinding);
        }

        private void NbSaisonsModif_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {


            comboboxSaisonsModif.Items.Clear();
            while(Serie.ListeAvis.Count > Serie.NbSaisons) 
            {
                // Dans le cas où on supprime des saisons il faut supprimer les avis inutiles
                Serie.ListeAvis.RemoveAt(Serie.ListeAvis.Count-1);
            }
            for (int i = 1; i <= Serie.NbSaisons; i++)
            {
                comboboxSaisonsModif.Items.Add($"Saison {i}");
                if (Serie.ListeAvis.Count < i)
                {
                    Serie.ListeAvis.Add(new Avis(null, null));
                }
            }
            comboboxSaisonsModif.SelectedIndex = 0;

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

                while (File.Exists(System.IO.Path.Combine(App.imagesPath, newName)))
                {
                    int fin = newName.LastIndexOf('.');
                    // On vérifi que l'image n'est pas déjà numérotée
                    if (newName.Contains("_"))
                    {
                        string essai = $"{newName.Remove(fin)}";

                        string numero = essai.Substring(essai.LastIndexOf('_') + 1);

                        if (uint.TryParse(numero, out uint nb))
                        {
                            newName = $"{newName.Remove(fin - 1)}{nb + 1}{fi.Extension}";
                        }
                        else newName = $"{newName.Remove(fin)}_1{fi.Extension}";
                    }
                    else newName = $"{newName.Remove(fin)}_1{fi.Extension}";

                }

                File.Copy(dialog.FileName, System.IO.Path.Combine(App.imagesPath, newName));
                Serie.Image = newName;
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
