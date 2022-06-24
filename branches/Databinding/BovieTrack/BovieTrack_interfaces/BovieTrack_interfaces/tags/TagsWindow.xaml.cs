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

namespace Appli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TagsWindow : Window
    {
        public Manager Manager => (App.Current as App).Manager;


        public TagsWindow()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void Associer_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel parent = (sender as Button).Parent as WrapPanel;
            string tag = (parent.Children[0] as TextBlock).Text;
            if (MessageBox.Show($"Associer le tag \"{tag}\" à {Manager.ElementSelectionne.Nom} ?", "Confirmation de suppression du tag", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Manager.AssocierTag(Manager.ElementSelectionne, tag);
            }
        }

        private void Enlever_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel parent = (sender as Button).Parent as WrapPanel;
            string tag = (parent.Children[0] as TextBlock).Text;
            if (MessageBox.Show($"Enlever le tag \"{tag}\" de {Manager.ElementSelectionne.Nom} ?", "Confirmation de suppression du tag", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Manager.EnleverTag(Manager.ElementSelectionne, tag);
            }
        }
        

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel parent = (sender as Button).Parent as WrapPanel;
            string tag = (parent.Children[0] as TextBlock).Text;

            int nbAssociations = Manager.CompterAssociationsTag(tag);

            if (MessageBox.Show($"Supprimer le tag \"{tag}\" ?\n ( Associé {nbAssociations} fois )", "Confirmation de suppression du tag", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Manager.SupprimerUnTag(tag);
            }
        }

        private void Save_Tag(object sender, RoutedEventArgs e)
        {
            string tag = nouveauTag.Text;
            try
            {
                bool result = Manager.AjouterNouveauTag(tag);
                if (!result)
                {
                    MessageBox.Show($"Le tag {tag} existe déjà", "Tag non créé", MessageBoxButton.OK);
                }
                else Manager.AssocierTag(Manager.ElementSelectionne, tag);
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"Le tag ne peux pas être vide", "Tag non créé", MessageBoxButton.OK);
            }
        }

        private void Quitter(object sender, RoutedEventArgs e)
            => Close();

    }
}
