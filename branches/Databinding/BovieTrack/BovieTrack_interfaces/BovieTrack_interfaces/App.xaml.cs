using Data;
using Modele;
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
        public static string defaultImage { get; } = Path.Combine(imagesPath, "DefaultImage.png");
        /// <summary>
        /// Constructeur de App
        /// </summary>
        public App()
        {
            Manager = new Manager(Stub.CreerCollection(), Stub.CreerTags());
            Manager.ActualiseTrie();

            Manager.AssocierTag(Manager.GetOeuvre(1), "Tag 4");
            Manager.AssocierTag(Manager.GetOeuvre(1), "Tag 3");
            Manager.AssocierTag(Manager.GetOeuvre(1), "Tag 2");
            Manager.AssocierTag(Manager.GetOeuvre(1), "Tag 1");
            Manager.AssocierTag(Manager.GetOeuvre(1), "Tag 5");
            Manager.AssocierTag(Manager.GetOeuvre(3), "Tag 3");
            Manager.AssocierTag(Manager.GetOeuvre(5), "Tag 2");
            Manager.AssocierTag(Manager.GetOeuvre(5), "Tag 5");



        }
    }
}
