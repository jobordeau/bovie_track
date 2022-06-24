using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Modele
{
    [DataContract]
    public class TagsManager
    {
        private ObservableCollection<string> tags = new ObservableCollection<string>(); // Collection des tags dans l'ordre alphabétique
        [DataMember(EmitDefaultValue = false)]
        public ReadOnlyObservableCollection<string> Tags; // Collection de tags transmise pour affichage

        public TagsManager()
        {
            Tags = new ReadOnlyObservableCollection<string>(tags);
        }

        /// <summary>
        /// Ajoute un nouveau tag pouvant être utilisé aux tags existants 
        /// </summary>
        /// <param name="tag">Nouveau tag</param>
        public bool Ajouter(string tag) // 
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
        /// <summary>
        /// Supprime un tag des tags existants, il est supprimer de toutes les oeuvres auxquelles il était associé
        /// </summary>
        /// <param name="collection">Collection dans laquelle on souhaite supprimer ce tag</param>
        /// <param name="tag">Tag à supprimer</param>
        public bool Supprimer(CollectionManager collection, string tag) 
        {
            if (!tags.Remove(tag))
            {
                return false;
            }
            collection.SuppressionGlobaleTag(tag);
            return true;
        }
        /// <summary>
        /// Associe un tag des tags existant à un Oeuvre
        /// </summary>
        /// <param name="oeuvre">Oeuvre à laquell on associe ce tag</param>
        /// <param name="tag">Tag à associer</param>
        public bool Associer(Oeuvre oeuvre, string tag) 
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
        /// <summary>
        /// Enlève un tag associé à une Oeuvre
        /// </summary>
        /// <param name="oeuvre">Oeuvre à laquelle on enlève le tag</param>
        /// <param name="tag">Tag à associer</param>
        public static bool Enlever(Oeuvre oeuvre, string tag) => oeuvre.Tags.Remove(tag); 
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
