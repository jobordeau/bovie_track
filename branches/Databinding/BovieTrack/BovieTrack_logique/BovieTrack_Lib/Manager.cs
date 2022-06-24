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

        public ReadOnlyObservableCollection<Oeuvre> collectionAffichee => CollectionMgr.selection;
        public ReadOnlyObservableCollection<string> collectionTags => TagsMgr.Tags;

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

        public Manager()
        {
            CollectionMgr = new CollectionManager();
            TagsMgr = new TagsManager();
        }

        public Manager(CollectionManager collectionMgr, TagsManager tagsMgr)
        {
            if(collectionMgr is null || tagsMgr is null)
            {
                throw new ArgumentNullException("Les arguments ne doivent pas être null");
            }
            CollectionMgr = collectionMgr;
            TagsMgr = tagsMgr;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        // Méthodes sur les oeuvres
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

        public IEnumerable<Invalide> ModifierFilm(Film oeuvre, string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifFilm(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajFilm(nom, image, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            return null;
        }

        public IEnumerable<Invalide> ModifierSerie(Serie oeuvre, string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifSerie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajSerie(nom, image, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            return null;
        }

        public IEnumerable<Invalide> ModifierLivre(Livre oeuvre, string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifLivre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajLivre(nom, image, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
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
