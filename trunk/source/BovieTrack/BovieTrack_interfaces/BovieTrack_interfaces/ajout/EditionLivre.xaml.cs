using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Modele;


namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour Livre.xaml
    /// </summary>
    public partial class EditionLivre : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;
        public Livre Livre { get; private set; } = new Livre(null, App.defaultImage, null, null, 0, 0, null, null, null, null as string);

        public EditionLivre(object sender)
        {
            InitializeComponent();
            EditionOeuvre main = sender as EditionOeuvre;
            if (main.Mode == ModeEdition.Creation)

            {
                main.Saved += Ajout_Livre;
                //Adaptation du menu
                nonTermineRadio.IsChecked = true;
                nonCommenceRadio.IsChecked = true;
                nonRecommenceRadio.IsChecked = true;
            }

            if (main.Mode == ModeEdition.ModificationDeLivre)
            {
                main.Saved += Modif_Livre;
                CopieLivre(Livre, Manager.ElementSelectionne as Livre);
                // Les types références copiés dans Livre risque de changer involontairement ce de ElementSelectionne donc :
                if (Livre.Avis != null) Livre.Avis = new Avis(Livre.Avis.Note, Livre.Avis.Commentaire);

                // Adaptation du menu
                if (Livre.NbFini == 0)
                {
                    nonTermineRadio.IsChecked = true;
                    if (Livre.Etat.HasValue) commenceRadio.IsChecked = true;
                    else nonCommenceRadio.IsChecked = true;
                }
                else
                {
                    termineRadio.IsChecked = true;
                    if (Livre.Etat.HasValue) recommenceRadio.IsChecked = true;
                    else nonRecommenceRadio.IsChecked = true;
                }
                if (Livre.DatePrevue.HasValue)
                {
                    planifieCheck.IsChecked = true;
                    Planifie.Opacity = 1;
                    Planifie.IsEnabled = true;
                }
            }
            DataContext = Livre;
        }

        private void CopieLivre(Livre oldL, Livre newL)
        {
            Type typeLivre = typeof(Livre);
            var livreProperties = typeLivre.GetProperties();
            foreach (var property in livreProperties.Where(ppty => ppty.CanWrite))
            {
                property.SetValue(oldL, property.GetValue(newL));
            }
        }


        private void Charge_Livre()
        {
            if (!uint.TryParse(saisieNbPages.Text, out uint nbpages)) Livre.NbPages = 0;

            // Cas selon la sélection //
            if (nonTermineRadio.IsChecked.Value)
            {
                Livre.NbFini = 0;
                if (!commenceRadio.IsChecked.Value)
                {
                    Livre.Etat = null;
                }
            }
            else if (termineRadio.IsChecked.Value)
            {

                if (uint.TryParse(saisieNbFini.Text, out uint nbfini)) Livre.NbFini = nbfini;
                else Livre.NbFini = 1;

                if (!recommenceRadio.IsChecked.Value)
                {
                    Livre.Etat = null;
                }
            }

            if (nonTermineRadio.IsChecked.Value) Livre.DateFini = null;

            if (!Planifie.IsEnabled) Livre.DatePrevue = null;

            //------------------//
        }

        private void Ajout_Livre(object sender, EventArgs e)
        {
            Charge_Livre();

            var verif = ValidateurOeuvre.VerifLivreCree(Livre);

            if (verif.Count() == 0)
            {
                Manager.AjouterOeuvre(Livre);
                MessageBox.Show("Livre créé !", "Consulte la collection", MessageBoxButton.OK);
                var parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show(ValidateurOeuvre.MessageInvalide(verif));
            }
        }

        private void Modif_Livre(object sender, EventArgs e)
        {
            Charge_Livre();

            var verif = ValidateurOeuvre.VerifLivreCree(Livre);

            if (verif.Count() == 0)
            {
                CopieLivre(Manager.ElementSelectionne as Livre, Livre);
                MessageBox.Show("Livre modifié !", "Consulte la collection", MessageBoxButton.OK);
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
                Livre.Image = newName;
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
