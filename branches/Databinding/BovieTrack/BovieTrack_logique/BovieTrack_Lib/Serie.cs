using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Modele
{
    public class Serie : Oeuvre, INotifyPropertyChanged
    {
        public class EtatSerie : INotifyPropertyChanged // Classe utilisée exclusivement par Série pour indiqué l'état du visionnage
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            public int Saison 
            {
                get => saison;
                set
                {
                    if(value != saison)
                    {
                        saison = value;
                        OnPropertyChanged();
                    }
                } 
            }
            private int saison;
            public int Episode 
            { 
                get => episode;
                set
                {
                    if (value != episode)
                    {
                        episode = value;
                        OnPropertyChanged();
                    }
                }
            }
            private int episode;

            public EtatSerie(int saison, int episode)
            {
                Saison = saison;
                Episode = episode;
            }

            public override string ToString()
            {
                return $"Saison {Saison} Épisode {Episode}";
            }
        }




        private uint nbSaisons;
        public uint NbSaisons// Le nombre de saisons ne peut pas être inférieur ou égal à 0
        {
            get => nbSaisons;
            set
            {
                if (nbSaisons != value)
                {
                    nbSaisons = value;
                    OnPropertyChanged();
                }
            }
        }
        private EtatSerie etat;
        public EtatSerie Etat
        {
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
        public List<Avis> ListeAvis { get; set; }

        public Serie(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
            NbSaisons = nbSaisons;

            if (etatSaison.HasValue && etatEpisode.HasValue)
            {
                Etat = new EtatSerie(etatSaison.Value, etatEpisode.Value);
            }
            else Etat = null;       

            ListeAvis = new List<Avis>();
            for (int i = 0; i < notes.Length; i++)
            {
                ListeAvis.Add(new Avis(notes[i], commentaires[i]));
            }
        }

        public void MajSerie(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {

            MajOeuvre(nom, image, dateFini, datePrevue, nbFini);

            NbSaisons = nbSaisons;

            if (etatSaison.HasValue && etatEpisode.HasValue)
            {
                Etat = new EtatSerie(etatSaison.Value, etatEpisode.Value);
            }
            else Etat = null;

            ListeAvis = new List<Avis>();
            for (int i = 0; i < notes.Length; i++)
            {
                ListeAvis.Add(new Avis(notes[i], commentaires[i]));
            }            
        }


        public override string ToString()
        {
            string str = base.ToString() + "\n";
            str += $"{NbSaisons} saisons\n";
            if (Etat == null) str += $"Pas de visionnage en cours\n";
            else str += $"État du visionnage : {Etat}\n";
            int numSaison = 1;
            foreach(Avis avis in ListeAvis)
            {
                str += $"Avis saison {numSaison} :\n{avis}";
                numSaison++;
            }
            return str;
        }
    }


}
