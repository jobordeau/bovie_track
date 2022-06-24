using System;

namespace Modele
{
    public class Film : Oeuvre
    {
        public TimeSpan Duree { get; private set; }
        private TimeSpan? etat;
        public TimeSpan? Etat {
            get => etat;
            private set
            {
                if (value.HasValue && value.Value > Duree)
                {
                    throw new ArgumentException("Erreur : Le moment de l'arrêt ne peut pas être supérieur à la durée du film.");
                }
                etat = value;
            } 
        }
        public Avis Avis { get; private set; }

        public Film(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
            : base(nom, dateFini, datePrevue, nbFini)
        {
            Duree = duree;

            Etat = etat;

            Avis = new Avis(note, commentaire);
        }

        public void MajFilm(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            MajOeuvre(nom, dateFini, datePrevue, nbFini);

            Duree = duree;
            
            Etat = etat;

            Avis = new Avis(note, commentaire);
        }


        public override string ToString()
        {
            string str = base.ToString() + "\n";
            str += $"{Duree:hh\\hmm\\m}" + "\n";
            if (Etat == null) str += $"Pas de visionnage en cours\n";
            else str += $"État du visionnage : {Etat.Value:hh\\hmm\\m}\n";
            str += Avis;
            return str;
        }

    }
}
