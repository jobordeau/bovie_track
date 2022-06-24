using System;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'oeuvre</param>
        /// <param name="image">Chemin du répertoire de l'image associer à l'oeuvre</param>
        /// <param name="dateFini">Date à laquelle l'oeuvre a été terminer</param>
        /// <param name="datePrevue">Date à laquelle l'oeuvre doit être commencer ou continuer par l'utilisateur</param>
        /// <param name="nbFini">Nombre de fois que l'oeuvre a été terminé</param>
        /// <param name="duree">Durée totale du film</param>
        /// <param name="etat">Durée à laquelle l'utilisateur s'est arrêté</param>
        /// <param name="note">Note donné par l'utilisateur au film</param>
        /// <param name="commentaire">Critique de l'utilisateur sur le film</param>
        public Film(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
            Duree = duree;

            Etat = etat;

            Avis = new Avis(note, commentaire);
        }

        /// <summary>
        /// Constructeur seulement appelé pour convertir plus facilement un Film en FilmDTO
        /// </summary>
        /// <param name="avis">L'avis de l'utilisateur sur l'oeuvre avec une note et/ou un commentaire </param>
        /// <param name="tags">Liste des tags associés au film</param>
        public Film(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, Avis avis, ObservableCollection<string> tags)
            : base(nom, image, dateFini, datePrevue, nbFini, tags)
        {
            Duree = duree;

            Etat = etat;

            Avis = avis;
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
