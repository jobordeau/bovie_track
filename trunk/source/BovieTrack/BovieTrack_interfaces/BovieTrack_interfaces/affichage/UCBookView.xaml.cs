using System;
using System.Windows;
using System.Windows.Controls;
using Modele;


namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour UCBookView.xaml
    /// </summary>
    public partial class UCBookView : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;
        public Livre Livre => DataContext as Livre;

        public UCBookView()
        {
            InitializeComponent();
            DataContext = new Livre("_", null, null, null, 0, 1, "_", null, null, null as string);
        }
        public event EventHandler<EventArgs> GoBack;

        void OnGoBack() => GoBack?.Invoke(this, new EventArgs());

        private void Go_Back_Button(object sender, RoutedEventArgs e) => OnGoBack();

        private void Modifier_Button_Click(object sender, RoutedEventArgs e)
        {
            EditionOeuvre Edition = new EditionOeuvre(ModeEdition.ModificationDeLivre);
            Edition.Livre_Click(sender, null);
            Edition.ShowDialog();
            // On regarde si l'élément modifié appartient à la sélection SI oui on affiche sinon retour à l'affichage global
            Manager.ActualiseTrie();
            Manager.ElementSelectionne = Livre;
            if (!Manager.collectionAffichee.Contains(Manager.ElementSelectionne))
            {
                OnGoBack();
            }
        }
        private void Supprimer_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Supprimer {Livre.Nom} ?", "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Manager.SupprimerOeuvre(Livre);
                Manager.ActualiseTrie();
                OnGoBack();
            }
        }

        private void Note_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            //  Affichage de la note
            if (Livre.Avis.Note.HasValue)
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

        private void Commentaire_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            //  Affichage du commentaire si null
            if (Livre.Avis.Commentaire == null)
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

        private void DateFini_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            // Affichage dateFini
            if (Livre.DateFini.HasValue) dateFini.Visibility = Visibility.Visible;
            else dateFini.Visibility = Visibility.Collapsed;
        }

        private void DatePrevue_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            // Affichage infoDatePrevue
            if (Livre.DatePrevue.HasValue)
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
