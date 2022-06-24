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

            if (note.HasValue && note < 0 || note > 10) erreurs.Add(Invalide.avis);

            return erreurs;
        }

        public static IEnumerable<Invalide> VerifLivre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            List<Invalide> erreurs = VerifOeuvre(nom, dateFini, datePrevue, nbFini);

            if (string.IsNullOrWhiteSpace(auteur)) erreurs.Add(Invalide.auteur);

            if (etat.HasValue && (etat.Value < 0 || etat.Value > nbPages)) erreurs.Add(Invalide.etat);

            if (note.HasValue && note < 0 || note > 10) erreurs.Add(Invalide.avis);

            return erreurs;
        }

        public static IEnumerable<Invalide> VerifSerie(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {
            List<Invalide> erreurs = VerifOeuvre(nom, dateFini, datePrevue, nbFini);

            if (etatSaison.HasValue && etatEpisode.HasValue && (etatSaison.Value > nbSaisons || etatSaison < 1 || etatEpisode < 1 )) erreurs.Add(Invalide.etat);

            if (notes.Length != nbSaisons || commentaires.Length != nbSaisons) erreurs.Add(Invalide.avis);
            else
            {
                foreach(int? note in notes)
                {
                    if(note.HasValue && note < 0 || note > 10) erreurs.Add(Invalide.avis);
                }
            }
            return erreurs;
        }
    }
}
