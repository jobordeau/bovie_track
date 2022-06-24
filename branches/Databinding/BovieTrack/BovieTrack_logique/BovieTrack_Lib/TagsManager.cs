using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Modele
{
    public class TagsManager
    {
        private ObservableCollection<string> tags = new ObservableCollection<string>(); // Collection des tags dans l'ordre alphabétique
        public ReadOnlyObservableCollection<string> Tags; // Collection de tags transmise pour affichage

        public TagsManager()
        {
            Tags = new ReadOnlyObservableCollection<string>(tags);
        }

        public bool Ajouter(string tag) // Créer un nouveau tag pouvant être utilisé
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                throw new ArgumentException("Erreur : Le tag ne peut pas être vide");
            }

            if (tags.Contains(tag)) return false;

            tags.Add(tag);

            // Comme une observableCollection n'a pas de méthode Sort on utilise OrderBy() à partir d'un enumerable
            var tagsTries = tags.OrderBy(k => k).ToList();

            tags.Clear();
            foreach (string t in tagsTries)
            {
                tags.Add(t);
            }

            return true;
        }

        public bool Supprimer(CollectionManager collection, string tag) // Supprime un tag existant
        {
            if (!tags.Remove(tag))
            {
                return false;
            }
            collection.SuppressionGlobaleTag(tag);
            return true;
        }

        public bool Associer(Oeuvre oeuvre, string tag) // Associe un tag des tags existant à un Oeuvre
        {
            if (!tags.Contains(tag))
            {
                throw new ArgumentException("Erreur : Ce tag n'existe pas");
            }
            if (oeuvre.Tags.Contains(tag))
            {
                return false;
            }
            oeuvre.Tags.Add(tag);

            // Comme une observableCollection n'a pas de méthode Sort on utilise OrderBy() à partir d'un enumerable
            var tagsTries = oeuvre.Tags.OrderBy(k => k).ToList();

            oeuvre.Tags.Clear();
            foreach(string t in tagsTries)
            {
                oeuvre.Tags.Add(t);
            }
            
            return true;
        }

        public static bool Enlever(Oeuvre oeuvre, string tag) => oeuvre.Tags.Remove(tag); // Enlève un tag associé à une Oeuvre

        public override string ToString()
        {
            string str = "Tags existants:";
            foreach (string tag in tags)
            {
                str += $"\n{tag}";
            }
            return str;
        }
    }
}
