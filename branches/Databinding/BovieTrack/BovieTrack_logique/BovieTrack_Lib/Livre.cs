using System;
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
        public Livre(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
            : base(nom, image, dateFini, datePrevue, nbFini)
        {
           NbPages = nbPages;

           Auteur = auteur;
         
           Etat = etat;

           Avis = new Avis(note, commentaire);
        }


        public void MajLivre(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            MajOeuvre(nom, image, dateFini, datePrevue, nbFini); 

            NbPages = nbPages;

            Auteur = auteur;
            
            Etat = etat;

            Avis = new Avis(note, commentaire);
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
