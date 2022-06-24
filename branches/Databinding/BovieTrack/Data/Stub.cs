using Modele;
using System;

namespace Data
{
    public static class Stub
    {
        public static CollectionManager CreerCollection()
        {
            CollectionManager collection = new CollectionManager();

            collection.AjouterElement(new Film("Ratatouille", "ratatouille.jpg", new DateTime(2018, 8, 12), new DateTime(2022, 5, 13), 5, new TimeSpan(2, 15, 0), null, 10f, "C'est cool"));
            collection.AjouterElement(new Film("La Reine des Neiges", "reine_des_neiges.jpg", new DateTime(2017, 6, 5), null, 1, new TimeSpan(1, 9, 54), new TimeSpan(0, 9, 45), 4.3f, "Vraiment pas ouf"));
            collection.AjouterElement(new Film("Le Monde de Némo", "nemo.jpg", new DateTime(2017, 12, 27), new DateTime(2055, 1, 6), 0, new TimeSpan(3, 10, 0), null, null, null));

            collection.AjouterElement(new Serie("Stranger Things", "strangerthings.png", new DateTime(2013, 8, 12), new DateTime(2020, 7, 3), 5, 4, 1, 3, new float?[] { 7.3f, 6f, 8f, null }, new string[] { "Sympa", "Bien ", "Très bien", null }));
            collection.AjouterElement(new Serie("Prison Break", "prison_break.jpg", new DateTime(2014, 8, 22), new DateTime(2020, 9, 3), 2, 5, null, null, new float?[] { 7.4f, 2.1f, null, null, null }, new string[] { "Pas mal", "Bof", null, null, null }));
            collection.AjouterElement(new Serie("Games of Thrones", "game_of_thrones.png", new DateTime(2013, 8, 12), null, 1, 8, null, null, new float?[] { 8f, 8f, 8f, 9f, 9f, 7f, 8f, 7f }, new string[] { null, null, null, null, null, null, null, null }));

            collection.AjouterElement(new Livre("Harry Potter", "harry_potter.jpg", new DateTime(2013, 8, 12), new DateTime(2021, 1, 30), 11, 500, "JK Rolling", 120, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", "hobbit.jpg", new DateTime(2013, 8, 12), null, 2, 450, "Tolkien", null, null, "J'adore"));
            collection.AjouterElement(new Livre("Les Misérables", "miserables.jpg", new DateTime(2014, 8, 12), new DateTime(2020, 4, 8), 0, 300, "Victor Hugo", null, 8.5f, null));

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

        public static CollectionManager CreerCollectionTest() // Utile pour les tests unitaires
        {
            CollectionManager collection = new CollectionManager();

            collection.AjouterElement(new Film("Titanic", null, new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), null, null, null));
            collection.AjouterElement(new Film("Titanic", null, new DateTime(2018, 8, 4), null, 2, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), null, null));
            collection.AjouterElement(new Film("Titanic", null, null, new DateTime(2025, 8, 16), 0, new TimeSpan(3, 15, 0), null, null, null));
            collection.AjouterElement(new Film("Titanic", null, null, null, 0, new TimeSpan(3, 15, 0), new TimeSpan(1, 21, 0), null, null));
            collection.AjouterElement(new Serie("Stranger Things", null, new DateTime(2019, 8, 8), null, 1, 4, 2, null, new float?[] { 7.3f, 6f, 8f, null }, new string[] { "Sympa", "Bien ", "Très bien", null }));
            collection.AjouterElement(new Serie("Stranger Things", null, new DateTime(2019, 8, 8), null, 1, 4, 2, 15, new float?[] { 7.3f, 6f, 8f, null }, new string[] { "Sympa", "Bien ", "Très bien", null }));
            collection.AjouterElement(new Serie("Stranger Things", null, null, new DateTime(2025, 8, 16), 0, 4, 2, null, new float?[] { 7.3f, 6f, 8f, null }, new string[] { "Sympa", "Bien ", "Très bien", null }));
            collection.AjouterElement(new Serie("Stranger Things", null, null, null, 0, 4, 2, 15, new float?[] { 7.3f, 6f, 8f, null }, new string[] { "Sympa", "Bien ", "Très bien", null }));
            collection.AjouterElement(new Livre("Le Hobbit", null, new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", null, new DateTime(2018, 8, 16), null, 2, 310, "J.R.R Tolkien", 138, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", null, null, new DateTime(2025, 8, 16), 0, 310, "J.R.R Tolkien", null, null, null));
            collection.AjouterElement(new Livre("Le Hobbit", null, null, null, 0, 310, "J.R.R Tolkien", 138, null, null));

            return collection;
        }
    }
}
