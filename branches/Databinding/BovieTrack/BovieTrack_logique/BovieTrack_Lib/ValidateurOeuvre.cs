using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public static class ValidateurOeuvre
    {

        private static List<Invalide> VerifOeuvre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini)
        {
            List<Invalide> erreurs = new List<Invalide>();

            if (string.IsNullOrWhiteSpace(nom)) erreurs.Add(Invalide.nom);

            if (dateFini.HasValue && dateFini.Value > DateTime.Now) erreurs.Add(Invalide.dateFini);
            if (datePrevue.HasValue && dateFini.HasValue && datePrevue.Value < dateFini.Value) erreurs.Add(Invalide.datePrevue);

            return erreurs;
        }

        public static IEnumerable<Invalide> VerifFilm(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            List<Invalide> erreurs = VerifOeuvre(nom, dateFini, datePrevue, nbFini);

            if (etat.HasValue && etat.Value > duree) erreurs.Add(Invalide.etat);

            if (note.HasValue && (note.Value < 0 || note.Value > 10)) erreurs.Add(Invalide.avis);

            return erreurs;
        }

        public static IEnumerable<Invalide> VerifFilmCree(Film film)
        {
            return VerifFilm(film.Nom, film.DateFini, film.DatePrevue, film.NbFini, film.Duree, film.Etat, film.Avis.Note, film.Avis.Commentaire);
        }


        public static IEnumerable<Invalide> VerifLivre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            List<Invalide> erreurs = VerifOeuvre(nom, dateFini, datePrevue, nbFini);

            if (string.IsNullOrWhiteSpace(auteur)) erreurs.Add(Invalide.auteur);

            if (etat.HasValue && (etat.Value < 0 || etat.Value > nbPages)) erreurs.Add(Invalide.etat);

            if (note.HasValue && (note.Value < 0 || note.Value > 10)) erreurs.Add(Invalide.avis);

            return erreurs;
        }
        public static IEnumerable<Invalide> VerifLivreCree(Livre livre)
        {
            return VerifLivre(livre.Nom, livre.DateFini, livre.DatePrevue, livre.NbFini, livre.NbPages, livre.Auteur, livre.Etat, livre.Avis.Note, livre.Avis.Commentaire);
        }


        public static IEnumerable<Invalide> VerifSerie(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            List<Invalide> erreurs = VerifOeuvre(nom, dateFini, datePrevue, nbFini);

            if (nbSaisons == 0) erreurs.Add(Invalide.duree);

            if (etatSaison.HasValue && etatEpisode.HasValue && (etatSaison.Value > nbSaisons || etatSaison < 1 || etatEpisode < 1 )) erreurs.Add(Invalide.etat);

            if (notes.Length != nbSaisons || commentaires.Length != nbSaisons) erreurs.Add(Invalide.avis);
            else
            {
                foreach (float? note in notes)
                {
                    if (note.HasValue && (note.Value < 0 || note.Value > 10))  erreurs.Add(Invalide.avis);
                }
            }
            return erreurs;
        }

        public static IEnumerable<Invalide> VerifSerieCreee(Serie serie)
        {
            int? etatSaison = null, etatEpisode = null;
            if(serie.Etat != null)
            {
                etatSaison = serie.Etat.Saison;
                etatEpisode = serie.Etat.Episode;
            }

            return VerifSerie(serie.Nom, serie.DateFini, serie.DatePrevue, serie.NbFini, serie.NbSaisons, etatSaison, etatEpisode, serie.ListeAvis.Select(avis => avis.Note).ToArray(), serie.ListeAvis.Select(avis => avis.Commentaire).ToArray());
        }


        public static string MessageInvalide(IEnumerable<Invalide> erreurs)
        {
            string msg = "Impossible de créer l'oeuvre :";
            foreach(Invalide i in erreurs)
            {
                if (i == Invalide.nom) msg += "\n- Nom invalide";
                if (i == Invalide.duree) msg += "\n- Longueur de l'élément invalide";
                if (i == Invalide.dateFini) msg += "\n- Date de dernier visionnage/lecture invalide";
                if (i == Invalide.datePrevue) msg += "\n- Date planifiée invalide";
                if (i == Invalide.etat) msg += "\n- État de la progression invalide";
                if (i == Invalide.avis) msg += "\n- Avis (Note(s) et commentaire(s)) invalides";
                if (i == Invalide.auteur) msg += "\n- Auteur invalide";
            }
            return msg + "\n";
        }


    }
}
