using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Modele;


namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour UCSerieView.xaml
    /// </summary>
    public partial class UCSerieView : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;
        public Serie Serie => DataContext as Serie;

        public UCSerieView()
        {
            InitializeComponent();
            DataContext = new Serie("_", null, null, null, 0, 1, null, null, new float?[] { null }, new string[] { null });

        }

        public event EventHandler<EventArgs> GoBack;

        void OnGoBack() => GoBack?.Invoke(this, new EventArgs());

        private void Go_Back_Button(object sender, RoutedEventArgs e) => OnGoBack();

        private void Modifier_Button_Click(object sender, RoutedEventArgs e)
        {
            EditionOeuvre Edition = new EditionOeuvre(ModeEdition.ModificationDeSerie);
            Edition.Serie_Click(sender, null);
            Edition.ShowDialog();
            // On regarde si l'élément modifié appartient à la sélection SI oui on affiche sinon retour à l'affichage global
            Manager.ActualiseTrie();
            Manager.ElementSelectionne = Serie;
            if (!Manager.collectionAffichee.Contains(Manager.ElementSelectionne))
            {
                OnGoBack();
            }
            else
            {
                // On utilise cette méthode pour refaire le binding (pour les avis)
                ComboboxSaisons_SelectionChanged(sender, null);
            }


        }
        private void Supprimer_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Supprimer {Serie.Nom} ?", "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Manager.SupprimerOeuvre(Serie);
                Manager.ActualiseTrie();
                OnGoBack();
            }
        }


        private void ComboboxSaisons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Comme on change de saison il faut changer le binding du commentaire et de la note pour qu'ils correspondent à la saison sélectionnée

            Binding noteBinding = new Binding($"ListeAvis[{ComboboxSaisons.SelectedIndex}].Note");
            noteBinding.Source = Serie;
            noteBinding.StringFormat = "{0} / 10";

            noteBinding.NotifyOnTargetUpdated = true;
            BindingOperations.SetBinding(note, TextBlock.TextProperty, noteBinding);



            Binding commentaireBinding = new Binding($"ListeAvis[{ComboboxSaisons.SelectedIndex}].Commentaire");
            commentaireBinding.Source = Serie;
            commentaireBinding.NotifyOnTargetUpdated = true;
            BindingOperations.SetBinding(serieComment, TextBlock.TextProperty, commentaireBinding);

            Note_TargetUpdated(sender, null);
            Commentaire_TargetUpdated(sender, null);
        }


        private void NbSaisons_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            ComboboxSaisons.Items.Clear();

            for (int i = 1 ; i <= Serie.NbSaisons ; i++)
            {
                ComboboxSaisons.Items.Add($"Saison {i}");
            }
            ComboboxSaisons.SelectedIndex = 0;
        }
        

        private void Note_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (ComboboxSaisons.HasItems)
            {
                //  Affichage de la note
                if (Serie.ListeAvis[ComboboxSaisons.SelectedIndex].Note.HasValue)
                {
                    infoNote.Text = "Note :";
                    note.Visibility = Visibility.Visible;
                }
                else
                {
                    infoNote.Text = " Pas de note";
                    note.Visibility = Visibility.Collapsed;
                }
            }
         
        }

        private void Commentaire_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (ComboboxSaisons.HasItems)
            {

                //  Affichage du commentaire si null
                if (Serie.ListeAvis[ComboboxSaisons.SelectedIndex].Commentaire == null)
                {
                    commentaire.Height = new GridLength(0);
                    infoCommentaire.Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    commentaire.Height = new GridLength(1, GridUnitType.Star);
                    infoCommentaire.Height = new GridLength(0);
                }
            }
        }
          


        private void DateFini_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            // Affichage dateFini
            if (Serie.DateFini.HasValue) dateFini.Visibility = Visibility.Visible;
            else dateFini.Visibility = Visibility.Collapsed;
        }

        private void DatePrevue_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

            // Affichage infoDatePrevue
            if (Serie.DatePrevue.HasValue)
            {
                infoDatePrevue.Text = "Planifié pour";
                datePrevue.Visibility = Visibility.Visible;
            }
            else
            {
                infoDatePrevue.Text = "Non planifié";
                datePrevue.Visibility = Visibility.Collapsed;
            }
        }

        private void Tags_Button_Click(object sender, RoutedEventArgs e)
        {
            var tagsWindow = new TagsWindow();
            tagsWindow.ShowDialog();
        }
    }
}
