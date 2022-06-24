using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Modele
{
    public class Manager : INotifyPropertyChanged       
    {
        public CollectionManager CollectionMgr { get; private set; }
        public TagsManager TagsMgr { get; private set; }

        //Dépendance vers le gestionnaire de la persistance
        public IPersistanceManager Persistance { get; private set; }

        //Collections utilisées pour l'affichage dans les interfaces
        public ReadOnlyObservableCollection<Oeuvre> collectionAffichee => CollectionMgr.selection;
        public ReadOnlyObservableCollection<string> collectionTags => TagsMgr.Tags;

        //Element sélectioné par l'utilisateur sur l'interface
        private Oeuvre elementSelectionne;
        public Oeuvre ElementSelectionne { 
            get => elementSelectionne;
            set
            {
                if(elementSelectionne != value)
                {
                    elementSelectionne = value;
                    OnPropertyChanged(nameof(ElementSelectionne));
                }
            }
        }

        /// <summary>
        /// Constructeur de Manager
        /// </summary>
        /// <param name="persistance"> Type de persistance utilisé pour charger et sauvegarder les données</param>
        public Manager(IPersistanceManager persistance)
        {
            Persistance = persistance;
            CollectionMgr = new CollectionManager();
            TagsMgr = new TagsManager();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        // Méthodes sur les oeuvres

         /// <summary>
         /// Créé un film ou retourne une liste d'erreurs s'il est invalide
         /// </summary>
         /// Prend les paramètres du constructeur de Film
         /// <returns> Liste d'erreurs </returns>
        public IEnumerable<Invalide> AjouterFilm(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifFilm(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            if(erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Film(nom, image, dateFini, datePrevue, nbFini, duree, etat, note, commentaire));
            return null;
        }

        /// <summary>
        /// Créé une série ou retourne une liste d'erreurs si elle est invalide
        /// </summary>
        /// Prend les paramètres du constructeur de Serie
        /// <returns> Liste d'erreurs </returns>
        public IEnumerable<Invalide> AjouterSerie(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifSerie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Serie(nom, image, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires));
            return null;
        }

        /// <summary>
        /// Créé un livre ou retourne une liste d'erreurs s'il est invalide
        /// </summary>
        /// Prend les paramètres du constructeur de Livre
        /// <returns> Liste d'erreurs </returns>
        public IEnumerable<Invalide> AjouterLivre(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifLivre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Livre(nom, image, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire));
            return null;
        }


        // Méthodes sur les tags

        public int CompterAssociationsTag(string tag) => CollectionMgr.AssociationTag(tag);

        public bool AssocierTag(Oeuvre oeuvre, string tag) => TagsMgr.Associer(oeuvre, tag);

        public bool EnleverTag(Oeuvre oeuvre, string tag) => TagsManager.Enlever(oeuvre, tag);

        public bool AjouterNouveauTag(string tag) => TagsMgr.Ajouter(tag);

        public bool SupprimerUnTag(string tag) => TagsMgr.Supprimer(CollectionMgr, tag);


        // Méthodes sur la collection
        public Oeuvre GetOeuvre(int id) => CollectionMgr.GetOeuvre(id);

        public void AjouterOeuvre(Oeuvre o) => CollectionMgr.AjouterElement(o);

        public bool SupprimerOeuvre(Oeuvre oeuvre) => CollectionMgr.RetirerElement(oeuvre);

        public void ActualiseTrie() => CollectionMgr.SelectionCollection();


        //Méthode pour la persistance

        public void ChargeDonnees()
        {
            var donnees = Persistance.ChargeDonnees();
            foreach (KeyValuePair<int, Oeuvre> o in donnees.collection)
            {
                CollectionMgr.AjouterElement(o.Value);
            }

            foreach (string tag in donnees.tags)
            {
                TagsMgr.Ajouter(tag);
            }
            CollectionMgr.LastID = donnees.lastID;
        }

        public void SauvegardeDonnees()
        {
            Persistance.SauvegardeDonnees(CollectionMgr.collection as Dictionary<int, Oeuvre>, TagsMgr.Tags.ToList(), CollectionMgr.LastID);
        }

        // Affichage pour test
        public override string ToString()
        {
            string str = "\n------COLLECTION-------";
            str+= CollectionMgr;
            str += "\n-----------------------";
            str += $"\n{TagsMgr}";
            ;
            return str;
        }
    }
}
