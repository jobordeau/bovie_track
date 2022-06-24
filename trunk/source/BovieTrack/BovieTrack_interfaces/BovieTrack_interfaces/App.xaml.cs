using Data;
using DataContractPersistance;
using Modele;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Appli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Manager Manager { get; private set; }

        // On définit le chemin d'enregistrement des images ici
        public static string imagesPath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "..\\images\\");
        // Image donnée par défaut aux oeuvres
        public static string defaultImage { get; } = Path.Combine(imagesPath, "DefaultImage.png");
        /// <summary>
        /// Constructeur de App
        /// </summary>
        public App()
        {
            Manager = new Manager(new DataContractPersJSON());

            try { Manager.ChargeDonnees(); }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("Pas de fichier de données.");
            }           
                          
            Manager.ActualiseTrie();
        }
    }
}
