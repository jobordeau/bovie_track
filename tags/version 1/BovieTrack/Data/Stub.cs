using Modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Data
{
    public class Stub : IPersistanceManager
    {
        private IReadOnlyDictionary<int, Oeuvre> CreerCollection()
        {
            CollectionManager collection = new CollectionManager();

            collection.AjouterElement(new Film("Ratatouille", "ratatouille.jpg", new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), null, 10f, "C'est cool"));
            collection.AjouterElement(new Film("Ratatouille 2", "ratatouille.jpg", new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), 4.3f, "Vraiment pas ouf"));
            collection.AjouterElement(new Film("Ratatouille 3", "ratatouille.jpg", null, new DateTime(2025, 8, 16), 0, new TimeSpan(3, 15, 0), null, null, null));
            collection.AjouterElement(new Film("Ratatouille 4", "ratatouille.jpg", null, null, 0, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), null, null));
            collection.AjouterElement(new Serie("Stranger Things", "strangerthings.png", new DateTime(2019, 8, 8), null, 1, 3, 2, null, new float?[] { 7.3f, 6f, 8f}, new string[] { "Sympa", "Bien ", "Très bien"}));
            collection.AjouterElement(new Serie("Stranger Things 2", "strangerthings.png", new DateTime(2019, 8, 8), null, 1, 3, 2, 15, new float?[] { 7.4f, 2.1f, null}, new string[] { "Pas mal", "Bof", null}));
            collection.AjouterElement(new Serie("Stranger Things 3", "strangerthings.png", null, new DateTime(2025, 8, 16), 0, 3, 2, null, new float?[] { 8f, 8f, 8f }, new string[] { null, null, null}));
            collection.AjouterElement(new Serie("Stranger Things 4", "strangerthings.png", null, null, 0, 3, 2, 15, new float?[] { 8f, 8f, 8f}, new string[] { null, null, null }));
            collection.AjouterElement(new Livre("Le Hobbit", "hobbit.jpg", new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit 2", "hobbit.jpg", new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", 138, null, null));
            collection.AjouterElement(new Livre("Le Hobbit 3", "hobbit.jpg", null, new DateTime(2025, 8, 16), 0, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit 4", "hobbit.jpg", null, null, 0, 310, "J.R.R Tolkien", 138, null, null));

            return collection.collection;
        }

        private static IReadOnlyCollection<string> CreerTags()
        {
            TagsManager TagsMrg = new TagsManager();

            TagsMrg.Ajouter("Tag 1");
            TagsMrg.Ajouter("Tag 2");
            TagsMrg.Ajouter("Tag 3");
            TagsMrg.Ajouter("Tag 4");
            TagsMrg.Ajouter("Tag 5");
            TagsMrg.Ajouter("Tag 6");
            TagsMrg.Ajouter("Tag 7");
            TagsMrg.Ajouter("Tag 8");
            TagsMrg.Ajouter("Tag 9");


            return TagsMrg.Tags;
        }

        public (Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID) ChargeDonees()
        {
            Dictionary<int, Oeuvre> collection =(Dictionary<int, Oeuvre>) CreerCollection();
            SortedSet<string> tags = (SortedSet<string>)CreerTags();
            int lastID = collection.Count;
            return (collection, tags, lastID);
        }

        public void SauvegardeDonnees(Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID)
        {
            Debug.WriteLine("Sauvegarde demandée");
        }
    }
}
