using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Modele
{
    public class Manager
    {
        public CollectionManager CollectionMgr { get; private set; }
        public TagsManager TagsMgr { get; private set; }

        public IPersistanceManager Persistance { get; /*private*/ set; }

        public IReadOnlyCollection<Oeuvre> collectionAffichee => CollectionMgr.selection;
        public IReadOnlyCollection<string> collectionTags => TagsMgr.Tags;

        public Manager(IPersistanceManager persistance)
        {
            Persistance = persistance;
            CollectionMgr = new CollectionManager();
            TagsMgr = new TagsManager();
        }

        // Méthodes sur les oeuvres
        public IEnumerable<Invalide> AjouterFilm(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifFilm(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            if(erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Film(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire));
            return null;
        }

        public IEnumerable<Invalide> AjouterSerie(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifSerie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Serie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires));
            return null;
        }

        public IEnumerable<Invalide> AjouterLivre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifLivre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            CollectionMgr.AjouterElement(new Livre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire));
            return null;
        }

        public IEnumerable<Invalide> ModifierFilm(Film oeuvre, string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifFilm(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajFilm(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
            return null;
        }

        public IEnumerable<Invalide> ModifierSerie(Serie oeuvre, string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifSerie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajSerie(nom, dateFini, datePrevue, nbFini, nbSaisons, etatSaison, etatEpisode, notes, commentaires);
            return null;
        }

        public IEnumerable<Invalide> ModifierLivre(Livre oeuvre, string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            IEnumerable<Invalide> erreurs = ValidateurOeuvre.VerifLivre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
            if (erreurs.Count() != 0)
            {
                return erreurs;
            }
            oeuvre.MajLivre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);
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

        public bool SupprimerOeuvre(Oeuvre oeuvre) => CollectionMgr.RetirerElement(oeuvre);

        public void ActualiseTrie() => CollectionMgr.SelectionCollection();

        //Méthode pour la persistance

        public void ChargeDonnees()
        {
            var donnees = Persistance.ChargeDonees();
            foreach(KeyValuePair<int, Oeuvre> o in donnees.collection)
            {
                CollectionMgr.AjouterElement(o.Value);
            }

            foreach(string tag in donnees.tags)
            {
                TagsMgr.Ajouter(tag);
            }
            CollectionMgr.LastID = donnees.lastID;
        }

        public void SauvegardeDonnees()
        {
            Persistance.SauvegardeDonnees((Dictionary<int, Oeuvre>)CollectionMgr.collection, (SortedSet<string>)TagsMgr.Tags, CollectionMgr.LastID);
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
