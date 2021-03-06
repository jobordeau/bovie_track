using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace Modele
{
    public class Avis : IEquatable<Avis>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private float? note;
        public float? Note
        {
            get => note;
            set
            {
                if (note != value)
                {
                    note = value;
                    OnPropertyChanged();
                }
            }
        }
        private string commentaire;
        public string Commentaire
        {
            get => commentaire;
            set
            {
                if (commentaire != value)
                {
                    commentaire = value;
                    OnPropertyChanged();
                }
            }
        }

        public Avis(float? note, string commentaire)
        {
            if (note.HasValue && note < 0 || note > 10)
            {
                throw new ArgumentException("Erreur : La note de l'avis doit être comprise entre 0 et 10.");
            }
            else
            {
                Note = note;
                Commentaire = commentaire;
            }
        }

        public bool Equals(Avis other) => Note.Equals(other.Note) && Commentaire.Equals(other.Commentaire);

        public override bool Equals(object obj)
        {
            // Regarde si l'objet est null et si les types correspondances
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(obj, this)) return true;

            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Avis);

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string str;
            if (Note.HasValue) str = $"Note : {Note}/10 \n";
            else str = "Pas de note\n";
            if (Commentaire == null) str += "Pas de commentaire\n";
            else str += $"Commentaire : {Commentaire}\n";

            return str;
        }
    }
}
