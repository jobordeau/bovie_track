using System;
using System.Collections.Generic;

namespace Modele
{

    public abstract class Oeuvre : IComparable<Oeuvre>, IComparable, IEquatable<Oeuvre>
    {
        public int? ID { get; set; } = null;

        private string nom;
        public string Nom
        {
            get => nom;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Erreur : Le nom doit être renseigné.");
                }
                nom = value;
            }
        }
        public uint NbFini { get; private set; } // Erreur : Le nombre de fois où l'élément à été fini doit être renseigné et supérieur ou égal à 0
        public string Image { get; private set; }
        public DateTime? DateFini { get; private set; }
        public DateTime? DatePrevue { get; private set; }
        public List<string> Tags { get; private set; }

        public Oeuvre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini)
        {
            MajOeuvre(nom, dateFini, datePrevue, nbFini); // On peut éviter de réecrire deux fois l'affectation des attributs de la classe
        }

        public void MajOeuvre(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini) // Met à jour les attributs de l'oeuvre en fonction des paramètres
        {
            // Donne les attributs de l'oeuvre et vérifie leur validité
            Nom = nom;

            Image = "img";

            if (dateFini.HasValue && dateFini.Value > DateTime.Now)
            {
                throw new ArgumentException("Erreur : La date à laquelle l'élément à été fini pour la dernière fois doit être antérieure.");
            }
            else DateFini = dateFini;

            if (datePrevue.HasValue && dateFini.HasValue && datePrevue.Value < dateFini.Value)
            {
                throw new ArgumentException("Erreur : La date à laquelle l'élément est planifié doit être après la date où l'oeuvre fut finie.");
            }
            else DatePrevue = datePrevue;

            NbFini = nbFini;

            Tags = new List<string>();
            //
        }

        // Méthodes pour gérer l'oeuvre dans la collection

        public bool Equals(Oeuvre other) => ID == other.ID;


        public override bool Equals(object obj)
        {
            // Regarde si l'objet est null et si les types correspondent
            if(ReferenceEquals(obj, null)) return false;

            if(ReferenceEquals(obj, this)) return true;

            if (GetType() != obj.GetType()) return false;
            
            return Equals(obj as Oeuvre);
        }
       
        public int CompareTo(Oeuvre other) // Comparateur par défault qui compare par le nom
        {
            if (Nom.Equals(other.Nom))
            {
                return ID.Value.CompareTo(other.ID.Value); 
            }
            return Nom.CompareTo(other.Nom);
        }

        int IComparable.CompareTo(object obj)
        {
            if(!(obj is Oeuvre))
            {
                throw new ArgumentException("L'argument n'est pas une Oeuvre", "obj");
            }
            return this.CompareTo(obj as Oeuvre);
        }
        
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override string ToString()
        {
            string str = Nom + "\n";
            str += "Vu " + NbFini + " fois\n";
            if (DateFini != null) str += "Vu le " + DateFini.Value.ToShortDateString() + "\n";
            if (DatePrevue != null) str += "Planifié le " + DatePrevue.Value.ToShortDateString() + "\n";
            else str += "Non planifié\n";

            if (Tags.Count == 0)
            {
                str += "Pas de tags";
            }
            else
            {
                str += "Tags :";
                foreach (string tag in Tags)
                {
                    str += " " + tag;
                }
            }

            return str;
        }

    }
}
