using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Modele
{
    public class TagsManager
    {
        private SortedSet<string> tags = new SortedSet<string>(); // Collection des tags dans l'ordre alphabétique
        public IReadOnlyCollection<string> Tags => tags; // Collection de tags transmise pour affichage

        public bool Ajouter(string tag) // Créer un nouveau tag pouvant être utilisé
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                throw new ArgumentException("Erreur : Le tag ne peut pas être vide");
            }
            return tags.Add(tag);
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
            oeuvre.Tags.Sort();
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
