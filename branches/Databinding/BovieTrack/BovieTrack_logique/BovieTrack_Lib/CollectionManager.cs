using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Modele
{
    public enum ModeDeTri
    {
        DateCroissante,
        DateDecroissante,
        Alphabetique
    }

    public class CollectionManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int LastID { get; private set; } = 0;

        // Toutes les options de tri de la collection
        public bool Termine 
        {
            get => termine;
            set
            {
                if(value != termine)
                {
                    termine = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool termine;
        public bool EnCours
        {
            get => enCours;
            set
            {
                if (value != enCours)
                {
                    enCours = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool enCours;
        public bool NonCommence
        {
            get => nonCommence;
            set
            {
                if (value != nonCommence)
                {
                    nonCommence = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool nonCommence;
        public bool Planifie
        {
            get => planifie;
            set
            {
                if (value != planifie)
                {
                    planifie = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool planifie;
        public bool NonPlanifie
        {
            get => nonPlanifie;
            set
            {
                if (value != nonPlanifie)
                {
                    nonPlanifie = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool nonPlanifie;
        public bool TypeFilm
        {
            get => typeFilm;
            set
            {
                if (value != typeFilm)
                {
                    typeFilm = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool typeFilm;
        public bool TypeLivre
        {
            get => typeLivre;
            set
            {
                if (value != typeLivre)
                {
                    typeLivre = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool typeLivre;
        public bool TypeSerie
        {
            get => typeSerie;
            set
            {
                if (value != typeSerie)
                {
                    typeSerie = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool typeSerie;
        public ModeDeTri Mode
        {
            get => mode;
            set
            {
                if (value != mode)
                {
                    mode = value;
                    OnPropertyChanged();
                }
            }
        }
        private ModeDeTri mode;


        public List<string> tagsSelectionnes { get; private set; } = new List<string>();
        //

        private Dictionary<int, Oeuvre> collectionOeuvre = new Dictionary<int, Oeuvre>(); // Dictionnaire des Oeuvres enregistrées

        private ObservableCollection<Oeuvre> collectionTriee = new ObservableCollection<Oeuvre>(); // Collection où sont ordonnées et de sélectionnées les Oeuvres

        public ReadOnlyObservableCollection<Oeuvre> selection;

        public CollectionManager()
        {
            Termine = true;
            EnCours = true;
            NonCommence = true;
            Planifie = true;
            NonPlanifie = true;
            TypeFilm = true;
            TypeLivre = true;
            TypeSerie = true;
            Mode = ModeDeTri.DateDecroissante;

            selection = new ReadOnlyObservableCollection<Oeuvre>(collectionTriee);
        }


        // Méthodes gérant les éléments dans la collection
        public void AjouterElement(Oeuvre element)
        {
            if(element.ID.HasValue)
            {
                throw new ArgumentException("Erreur : Élément non ajouté car il appartient déjà à une collection");
            }
            collectionOeuvre.Add(LastID + 1, element);
            LastID++;
            element.ID = LastID; // L'id permet de connaître l'ordre d'ajout dans la collection à laquelle il appartient
        }

        public bool RetirerElement(Oeuvre element)
        {
            if (!element.ID.HasValue)
            {
                throw new ArgumentException("Erreur : Élément non retiré car il n'appartient pas à une collection"); 
            }

            return collectionOeuvre.Remove(element.ID.Value);
        }

        public Oeuvre GetOeuvre(int id)
        {
            if (!collectionOeuvre.TryGetValue(id, out Oeuvre oeuvre)) 
            {
                throw new ArgumentException("Erreur : L'ID n'est pas/plus dans la collection");
            }
            return oeuvre;
        }


        // Méthode pour gérer l'utilisation des tags dans la collection
        public int AssociationTag(string tag)
        {
            int nbUse = 0; 
            foreach(Oeuvre o in collectionOeuvre.Values)
            {
                if (o.Tags.Contains(tag))
                {
                    nbUse++;
                }
            }
            return nbUse;
        }

        public void SuppressionGlobaleTag(string tag)
        {
            foreach (Oeuvre o in collectionOeuvre.Values)
            {
                TagsManager.Enlever(o, tag);
            }
        }

        public void selectionTag(string tag) => tagsSelectionnes.Add(tag);
        public void deselectionTag(string tag) => tagsSelectionnes.Remove(tag);



        // Méthodes chargées de trier la collection à afficher
        public LinkedList<Oeuvre> TriAlphabetique()
        {
            // On place l'intégralité des oeuvre dans l'ordre alphabétique par rapport au nom

            LinkedList<Oeuvre> tri = new LinkedList<Oeuvre>();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.Nom))
            {
                tri.AddLast(o.Value);
            }
            return tri;

            
            /*
            collectionTriee.Clear();


            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.Nom))
            {
                collectionTriee.Add(o.Value);
            }
            */
        }

        public LinkedList<Oeuvre> TriDateFiniCroissant()
        {
            // On place les oeuvres qui n'ont pas de date au début dans l'ordre d'ajout (ID) PUIS on met les oeuvres dans l'ordre croissante par rapport à la DateFini 

            LinkedList<Oeuvre> tri = new LinkedList<Oeuvre>();
            SortedDictionary<int, Oeuvre> collectionIDSansDateFini = new SortedDictionary<int, Oeuvre>();

            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.DateFini))
            {
                if (o.Value.DateFini.HasValue)
                {
                    tri.AddLast(o.Value);
                }
                else
                {
                    collectionIDSansDateFini.Add(o.Key, o.Value);
                }
            }

            foreach (KeyValuePair<int, Oeuvre> o in collectionIDSansDateFini)
            {
                tri.AddFirst(o.Value);
            }
            return tri;


            /*
            collectionTriee.Clear();

            foreach(KeyValuePair<int, Oeuvre> kvp in collectionOeuvre.Where(kvp => !kvp.Value.DateFini.HasValue).OrderBy(kvp => kvp.Key))
            {
                collectionTriee.Add(kvp.Value);
            }
            foreach(Oeuvre o in collectionTriee.Where(o => o.DateFini.HasValue).OrderBy(o => o.DateFini))
            {
                collectionTriee.Add(o);
            }
            */
        }

        public LinkedList<Oeuvre> TriDateFiniDecroissant()
        {
            // On place les oeuvres dans l'ordre décroissant par rapport à la dateFini PUIS celles qui n'ont pas de dateFini dans l'ordre d'ajout décroissant (ID) à la fin

            LinkedList<Oeuvre> tri = new LinkedList<Oeuvre>();
            SortedDictionary<int, Oeuvre> collectionIDSansDateFini = new SortedDictionary<int, Oeuvre>();

            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderByDescending(kvp => kvp.Value.DateFini))
            {
                if (o.Value.DateFini.HasValue)
                {
                    tri.AddLast(o.Value);
                }
                else
                {
                    collectionIDSansDateFini.Add(o.Key, o.Value);
                }
            }

            foreach (KeyValuePair<int, Oeuvre> o in collectionIDSansDateFini)
            {
                tri.AddLast(o.Value);
            }
            return tri;

            /*
            collectionTriee.Clear();

            foreach (KeyValuePair<int, Oeuvre> kvp in collectionOeuvre.Where(kvp => !kvp.Value.DateFini.HasValue).OrderBy(kvp => kvp.Key))
            {
                collectionTriee.Add(kvp.Value);
            }
            foreach (Oeuvre o in collectionTriee.Where(o => o.DateFini.HasValue).OrderByDescending(o => o.DateFini))
            {
                collectionTriee.Add(o);
            }
            */
        }

        public LinkedList<Oeuvre> TriDatePrevueCroissant()
        {
            // On place uniquement les oeuvres qui ont une datePrevue et dans l'ordre croissant par rapport à la datePrevue

            LinkedList<Oeuvre> tri = new LinkedList<Oeuvre>();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.DatePrevue))
            {
                if (o.Value.DatePrevue.HasValue)
                {
                    tri.AddLast(o.Value);
                }
            }
            return tri;

            /*
            collectionTriee.Clear();

            foreach (Oeuvre o in collectionOeuvre.Values.Where(o => o.DateFini.HasValue).OrderBy(o => o.DateFini))
            {
                collectionTriee.Add(o);
            }
            */
        }

        public LinkedList<Oeuvre> TriDatePrevueDecroissant()
        {
            // On place uniquement les oeuvres qui ont une datePrevue et dans l'ordre décroissant par rapport à la datePrevue

            LinkedList<Oeuvre> tri = new LinkedList<Oeuvre>();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderByDescending(kvp => kvp.Value.DatePrevue))
            {
                if (o.Value.DatePrevue.HasValue)
                {
                    tri.AddLast(o.Value);
                }
            }
            return tri;
            
            /*
            collectionTriee.Clear();

            foreach (Oeuvre o in collectionOeuvre.Values.Where(o => o.DateFini.HasValue).OrderByDescending(o => o.DateFini))
            {
                collectionTriee.Add(o);
            }
            */
        }
        //


        // Méthode qui se charge de faire le tri sélectionné par l'utilisateur en fonction des paramètres ( utilise les méthodes de tris )
        public void SelectionCollection()
        {
            LinkedList<Oeuvre> tri = null;

            // Sélection du mode de tri
            switch (mode)
            {
                case ModeDeTri.DateDecroissante:
                    if (Planifie && !NonPlanifie) tri = TriDatePrevueDecroissant();
                    else tri = TriDateFiniDecroissant();
                    break;
                case ModeDeTri.DateCroissante:
                    if (Planifie && !NonPlanifie) tri = TriDatePrevueCroissant();
                    else tri = TriDateFiniCroissant();
                    break;
                case ModeDeTri.Alphabetique:
                    tri = TriAlphabetique();
                    break;
            }


            collectionTriee.Clear();

            LinkedListNode<Oeuvre> node = tri.First;
            LinkedListNode<Oeuvre> nextNode;

            while (node != null)
            {
                nextNode = node.Next;

                // Vérification des options
                if (node.Value is Film nodeFilm)
                {
                    if (!((!TypeFilm) ||
                        (!Termine && nodeFilm.NbFini > 0) ||
                        (!EnCours && nodeFilm.Etat.HasValue) ||
                        (!NonCommence && !nodeFilm.Etat.HasValue && nodeFilm.NbFini == 0) ||
                        (!Planifie && nodeFilm.DatePrevue.HasValue) ||
                        (!NonPlanifie && !nodeFilm.DatePrevue.HasValue) ||
                        !aLesTagsSelectionnes(nodeFilm)))
                    {
                        collectionTriee.Add(nodeFilm);
                    }            
                    node = nextNode;
                    continue;
                }

                if (node.Value is Livre nodeLivre)
                 {
                     if (!((!TypeLivre) ||
                         (!Termine && nodeLivre.NbFini > 0) ||
                         (!EnCours && nodeLivre.Etat.HasValue) ||
                         (!NonCommence && !nodeLivre.Etat.HasValue && nodeLivre.NbFini == 0) ||
                         (!Planifie && nodeLivre.DatePrevue.HasValue) ||
                         (!NonPlanifie && !nodeLivre.DatePrevue.HasValue) ||
                         !aLesTagsSelectionnes(nodeLivre)))
                    {
                        collectionTriee.Add(nodeLivre);
                    }
                    node = nextNode;
                    continue;
                }

                if (node.Value is Serie nodeSerie)
                 {
                     if (!((!TypeSerie) ||
                         (!Termine && nodeSerie.NbFini > 0) ||
                         (!EnCours && nodeSerie.Etat != null) ||
                         (!NonCommence && nodeSerie.Etat == null && nodeSerie.NbFini == 0) ||
                         (!Planifie && nodeSerie.DatePrevue.HasValue) ||
                         (!NonPlanifie && !nodeSerie.DatePrevue.HasValue) ||
                         !aLesTagsSelectionnes(nodeSerie)))
                    {
                        collectionTriee.Add(nodeSerie);
                    }
                    node = nextNode;
                    continue;
                }
            }

            // Méthode qui retourne true si tout l'oeuvre correspond aux tags sélectionnés sinon false
            bool aLesTagsSelectionnes(Oeuvre nodeOeuvre) 
            { 
                foreach (string tag in tagsSelectionnes)
                {
                    if (!nodeOeuvre.Tags.Contains(tag))
                    {
                        return false;
                    }
                }
                return true;
            }
            //
        }

        public override string ToString()
        {
            string str ="\nOption de trie:";
            str += $"\nTerminés: {Termine} \nNon terminés: {EnCours}\nNon commencés: {NonCommence}\nMode de trie: {mode}\nPlannifiés: {Planifie}\nNon Plannifié: {NonPlanifie}\nType Livre: {TypeLivre}\nType Film: {TypeFilm}\nType Série: {TypeSerie}\n";
            str += "\n---Liste des oeuvres---\n";
            foreach (Oeuvre o in collectionOeuvre.Values)
            {
                str += $"\n{o.ID} {o.Nom} {o.Tags.Count} tag(s)";
                
            }
            return str;
        }

    }
 }