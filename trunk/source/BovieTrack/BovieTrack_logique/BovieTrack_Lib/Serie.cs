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

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'oeuvre</param>
        /// <param name="image">Chemin du répertoire de l'image associer à l'oeuvre</param>
        /// <param name="dateFini">Date à laquelle l'oeuvre a été terminer</param>
        /// <param name="datePrevue">Date à laquelle l'oeuvre doit être commencer ou continuer par l'utilisateur</param>
        /// <param name="nbFini">Nombre de fois que l'oeuvre a été terminé</param>
        /// <param name="nbSaisons">Nombre de saisons dans la série</param>
        /// <param name="etatSaison">Saison actuel de l'utilisateur</param>
        /// <param name="etatEpisode">Episode actuel de l'utilisateur</param>
        /// <param name="notes">Liste des notes donné par l'utilisateur pour chaque épisode</param>
        /// <param name="commentaires">Liste des critiques de l'utilisateur pour chaque épisode</param>
        public Serie(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
            NbSaisons = nbSaisons;

            //Si l'utilisateur n'a pas commencé la série Etat = null
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

        /// <summary>
        /// Constructeur appelé pour convertir plus facilment une Serie en SerieDTO
        /// </summary>
        /// <param name="etat">Avancement de l'utilisateur dans la série indiquant la saison et l'épisode actuelle</param>
        /// <param name="listeAvis">Liste des avis de l'utilisateur sur l'oeuvre avec une note et/ou un commentaire pour chaque épisode</param>
        /// <param name="tags">Liste des tags associés à la série</param>
        public Serie(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, EtatSerie etat, List<Avis> listeAvis, ObservableCollection<string> tags)
            : base(nom, image, dateFini, datePrevue, nbFini, tags)
        {
            NbSaisons = nbSaisons;
            Etat = etat;
            ListeAvis = listeAvis;
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
