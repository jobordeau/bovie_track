using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Modele
{
    public class Livre : Oeuvre, INotifyPropertyChanged
    {
        private uint nbpages;
        public uint NbPages  // Le nombre de pages ne peut pas être inférieur à 0
        {
            get => nbpages;
            set
            {
                if (nbpages != value)
                {
                    nbpages = value;
                    OnPropertyChanged();
                }
            }
        }
        private string auteur;
        public string Auteur {
            get => auteur; 
            set
            {
                if (auteur != value)
                {
                    auteur = value;
                    OnPropertyChanged();
                }
            } 
        }
        private int? etat;
        public int? Etat 
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
        /// <param name="nbPages">Nombre total de page</param>
        /// <param name="auteur">Nom de l'auteur</param>
        /// <param name="etat">Page à laquelle l'utilisateur s'est arrêté</param>
        /// <param name="note">Note donné par l'utilisateur au livre</param>
        /// <param name="commentaire">Critique de l'utilisateur sur le livre</param>
        public Livre(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
           NbPages = nbPages;

           Auteur = auteur;
         
           Etat = etat;

           Avis = new Avis(note, commentaire);
        }

        /// <summary>
        /// Constructeur seulement appelé pour convertir plus facilement un Livre en LivreDTO
        /// </summary>
        /// <param name="avis">L'avis de l'utilisateur sur le livre avec une note et/ou un commentaire </param>
        /// <param name="tags">Liste des tags associés au livre</param>
        public Livre(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, Avis avis, ObservableCollection<string> tags)
       : base(nom, image, dateFini, datePrevue, nbFini, tags)
        {
            NbPages = nbPages;

            Auteur = auteur;

            Etat = etat;

            Avis = avis;
        }


        public override string ToString()
        {
            string str = base.ToString() + "\n";
            str += $"Auteur : {Auteur}\n";
            str += $"{NbPages} pages\n";
            if (Etat == null) str += $"Pas de lecture en cours\n";
            else str += $"État de la lecture : page {Etat}\n"; str += Avis;
            return str;
        }
    }
}
