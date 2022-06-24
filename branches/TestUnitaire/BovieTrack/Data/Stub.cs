using Modele;
using System;

namespace Data
{
    public class Stub
    {
        public static CollectionManager CreerCollection()
        {
            CollectionManager collection = new CollectionManager();

            collection.AjouterElement(new Film("Titanic", new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), null, 10f, "C'est cool"));
            collection.AjouterElement(new Film("Titanic", new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), 4.3f, "Vraiment pas ouf"));
            collection.AjouterElement(new Film("Titanic", null, new DateTime(2025, 8, 16), 0, new TimeSpan(3, 15, 0), null, null, null));
            collection.AjouterElement(new Film("Titanic", null, null, 0, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), null, null));
            collection.AjouterElement(new Serie("Stranger Things", new DateTime(2019, 8, 8), null, 1, 3, 2, null, new float?[] { 7.3f, 6f, 8f}, new string[] { "Sympa", "Bien ", "Très bien"}));
            collection.AjouterElement(new Serie("Stranger Things", new DateTime(2019, 8, 8), null, 1, 3, 2, 15, new float?[] { 7.4f, 2.1f, null}, new string[] { "Pas mal", "Bof", null}));
            collection.AjouterElement(new Serie("Stranger Things", null, new DateTime(2025, 8, 16), 0, 3, 2, null, new float?[] { 8f, 8f, 8f }, new string[] { null, null, null}));
            collection.AjouterElement(new Serie("Stranger Things", null, null, 0, 3, 2, 15, new float?[] { 8f, 8f, 8f}, new string[] { null, null, null }));
            collection.AjouterElement(new Livre("Le Hobbit", new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", 138, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", null, new DateTime(2025, 8, 16), 0, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", null, null, 0, 310, "J.R.R Tolkien", 138, null, null));

            return collection;
        }

        public static TagsManager CreerTags()
        {
            TagsManager Tags = new TagsManager();

            Tags.Ajouter("Tag 1");
            Tags.Ajouter("Tag 2");
            Tags.Ajouter("Tag 3");
            Tags.Ajouter("Tag 4");
            Tags.Ajouter("Tag 5");
            Tags.Ajouter("Tag 6");
            Tags.Ajouter("Tag 7");
            Tags.Ajouter("Tag 8");
            Tags.Ajouter("Tag 9");


            return Tags;
        }
    }
}
