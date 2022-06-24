using System;

namespace Modele
{
    public class Livre : Oeuvre
    {
        public uint NbPages { get; private set; } // Le nombre de pages ne peut pas être inférieur à 0

        private string auteur;
        public string Auteur {
            get => auteur; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Erreur : L'auteur doit être informé.");
                }
                auteur = value;
            } 
        }
        private int? etat;
        public int? Etat 
        { 
            get => etat;
            private set
            {
                if (value.HasValue && (value.Value < 0 || value.Value > NbPages))
                {
                    throw new ArgumentException("Erreur : La page d'arrêt de la lecture doit être situé entre 0 et le nombre de pages du livre.");
                }
                etat = value;
            }
        }
        public Avis Avis { get; private set; }

        public Livre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
            : base(nom, dateFini, datePrevue, nbFini)
        {
           NbPages = nbPages;

           Auteur = auteur;
         
           Etat = etat;

           Avis = new Avis(note, commentaire);
        }


        public void MajLivre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
        {
            MajOeuvre(nom, dateFini, datePrevue, nbFini); 

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
