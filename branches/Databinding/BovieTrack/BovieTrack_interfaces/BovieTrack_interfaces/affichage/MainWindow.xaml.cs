using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager Manager => (App.Current as App).Manager;
        public Oeuvre Selectionne => Manager.ElementSelectionne; 
        public UCGlobalCollection GlobalCollection { get; private set; } = new UCGlobalCollection();
        public UCSideCollection SideCollection { get; private set; } = new UCSideCollection();

        public enum ViewType : byte
        {
            Film,
            Livre,
            Serie
        }

        public UCFilmView UCFilm { get; private set; } = new UCFilmView();
        public UCBookView UCLivre { get; private set; } = new UCBookView();
        public UCSerieView UCSerie { get; private set; } = new UCSerieView();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = Manager;
            GlobalCollection.ElementSelected += Collection_ElementSelected;
            mainContent.Content = GlobalCollection;
            SideCollection.ElementSelected += Collection_ElementSelected ;
        }

        private void Collection_ElementSelected(object sender, EventArgs e)
        {
            if(sender is UCGlobalCollection)
            {
                mainContent.Content = null;
                sideContent.Content = SideCollection;
                sideColumn.Width = new GridLength(150);
            }

            if (Selectionne is Film)
            {
                UCFilm.DataContext = Selectionne as Film;
                UCFilm.GoBack += ReturnToCollection;
                mainContent.Content = UCFilm;
            }
            if (Selectionne is Livre)
            {
                UCLivre.DataContext = Selectionne as Livre;
                UCLivre.GoBack += ReturnToCollection;
                mainContent.Content = UCLivre;
            }
            if (Selectionne is Serie)
            {
                UCSerie.DataContext = Selectionne as Serie;
                UCSerie.GoBack += ReturnToCollection;
                UCSerie.ComboboxSaisons.SelectedIndex = 0; // On affiche par défaut la saison 1 de la série en premier
                mainContent.Content = UCSerie;
            }

        }

        public void ReturnToCollection(object sender, EventArgs e)
        {
            mainContent.Content = GlobalCollection;
            sideContent.Content = null;
            sideColumn.Width = new GridLength(0);
            Manager.ElementSelectionne = null;
        }

        private void View_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = GlobalCollection;
            sideColumn.Width = new GridLength(0);
            sideContent.Content = null;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Manager.ActualiseTrie();
            // On regarde si l'élément sélectionné appartient à la sélection SI non : retour à l'affichage global
            if (!Manager.collectionAffichee.Contains(Manager.ElementSelectionne))
            {
                ReturnToCollection(sender, null);
            }
        }


        private void Tag_Checked(object sender, RoutedEventArgs e)
            => Manager.CollectionMgr.selectionTag(((sender as CheckBox).Content as TextBlock).Text as string);
        

        private void Tag_Unchecked(object sender, RoutedEventArgs e)
            => Manager.CollectionMgr.deselectionTag(((sender as CheckBox).Content as TextBlock).Text as string);


        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int ID = Manager.CollectionMgr.LastID;
            EditionOeuvre Edition = new EditionOeuvre(ModeEdition.Creation);
            Edition.ShowDialog();
            Manager.ActualiseTrie();
            // On regarde si un élément est crée et s'il appartient à la sélection SI oui alors on affiche SINON retour à l'affichage global
            if (Manager.CollectionMgr.LastID == ID + 1 && Manager.collectionAffichee.Contains(Manager.GetOeuvre(ID + 1)))
            {
                Manager.ElementSelectionne = Manager.GetOeuvre(ID + 1);
            }
            else ReturnToCollection(sender, null);
        }
    }
}
