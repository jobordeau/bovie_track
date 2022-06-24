using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Modele
{
    public class Film : Oeuvre, INotifyPropertyChanged
    {
        private TimeSpan duree;
        public TimeSpan Duree
        {
            get => duree;
            set
            {
                if (duree != value)
                {
                    duree = value;
                    OnPropertyChanged();
                }
            }
        }
        private TimeSpan? etat;
        public TimeSpan? Etat {
            get => etat;
            set
            {
                if (etat != value)
                {
                    etat = value;
                    OnPropertyChanged();
                }
            } 
        }
        private Avis avis;
        public Avis Avis
        {
            get => avis;
            set
            {
                if (avis != value)
                {
                    avis = value;
                    OnPropertyChanged();
                }
            }
        }

        public Film(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
            Duree = duree;

            Etat = etat;

            Avis = new Avis(note, commentaire);
        }

        public void MajFilm(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
        {
            MajOeuvre(nom, image, dateFini, datePrevue, nbFini);

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
