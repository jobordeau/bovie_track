using Data;
using DataContractPersistance;
using Modele;
using System.Windows;

namespace Appli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Manager Manager { get; private set; } = new Manager(new Stub());

        public App() : base()
        {
            Manager.ChargeDonnees();
            Manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            Manager.ActualiseTrie();
        }
    }
}
