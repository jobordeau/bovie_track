using Modele;
using System;
using System.Windows.Controls;


namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour UCGlobalCollection.xaml
    /// </summary>
    public partial class UCGlobalCollection : UserControl
    {
        public Manager Manager => (App.Current as App).Manager;

        public event EventHandler<EventArgs> ElementSelected;
        
        void OnElementSelected() => ElementSelected?.Invoke(this, new EventArgs());

        public UCGlobalCollection()
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
