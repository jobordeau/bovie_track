using System;
using Modele;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.VisualBasic;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour UCSideCollection.xaml
    /// </summary>
    public partial class UCSideCollection : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;

        public event EventHandler<EventArgs> ElementSelected;

        void OnElementSelected() => ElementSelected?.Invoke(this, new EventArgs());

        public UCSideCollection()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnElementSelected();
        }
    }
}
