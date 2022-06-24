using Modele;
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

        }


        private void View_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = GlobalCollection;
            sideColumn.Width = new GridLength(0);
            sideContent.Content = null;
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderButton.IsChecked == true)
            {
                Manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
                Manager.ActualiseTrie();
            }
            else if(orderButton.IsChecked == false)
            {
                Manager.CollectionMgr.mode = ModeDeTri.DateCroissante;
                Manager.ActualiseTrie();
            }
            else
            {
                Manager.CollectionMgr.mode = ModeDeTri.DateDecroissante;
                Manager.ActualiseTrie();
            }
                
        }
    }
}
