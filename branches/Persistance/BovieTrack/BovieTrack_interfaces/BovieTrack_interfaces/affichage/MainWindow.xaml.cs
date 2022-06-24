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

namespace Bovie_Track
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void View_Film_Checked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new UCFilmView();
            sideContent.Content = new UCSideCollection();
            sideColumn.Width = new GridLength(150);
        }

        private void View_Book_Checked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new UCBookView();
            sideContent.Content = new UCSideCollection();
            sideColumn.Width = new GridLength(150);
        }

        private void View_Serie_Checked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new UCSerieView();
            sideContent.Content = new UCSideCollection();
            sideColumn.Width = new GridLength(150);
        }

        private void View_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new UCGlobalCollection();
            sideColumn.Width = new GridLength(0);
            sideContent.Content = null;
        }



    }
}
