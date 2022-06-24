using System;
using System.Collections.Generic;
using System.Linq;

namespace Modele
{
    public enum ModeDeTri : byte
    {
        DateCroissante,
        DateDecroissante,
        Alphabetique
    }

    public class CollectionManager
    {
        public int LastID { get; set; } = 0;

        // Toutes les options de tri de la collection
        public bool Termine { get; set; } = true;
        public bool EnCours { get; set; } = true;
        public bool NonCommence { get; set; } = true;

        public bool Planifie { get; set; } = true;
        public bool NonPlanifie { get; set; } = true;

        public bool TypeFilm { get; set; } = true;
        public bool TypeLivre { get; set; } = true;
        public bool TypeSerie { get; set; } = true;

        private List<string> tagsSelectionnes;

        public ModeDeTri mode { private get; set; } = ModeDeTri.DateDecroissante;
        //
        private Dictionary<int, Oeuvre> collectionOeuvre; // Dictionnaire des Oeuvres enregistrées

        private LinkedList<Oeuvre> collectionTriee; // LinkedList qui permet d'ordonner et de sélectionner les Oeuvres
        public IReadOnlyCollection<Oeuvre> selection => collectionTriee; // Collection transmise pour affichage


        public IReadOnlyDictionary<int, Oeuvre> collection => collectionOeuvre; // Collection non triée

        public CollectionManager()
        {
            tagsSelectionnes = new List<string>();
            collectionOeuvre = new Dictionary<int, Oeuvre>();
            collectionTriee = new LinkedList<Oeuvre>();
        }

        public CollectionManager(int lastID, Dictionary<int, Oeuvre> collectionOeuvre)
        {
            LastID = lastID;
            this.collectionOeuvre = collectionOeuvre;
            tagsSelectionnes = new List<string>();
            collectionTriee = new LinkedList<Oeuvre>();

        }


        // Méthodes gérant les éléments dans la collection
        public void AjouterElement(Oeuvre element)
        {
            /*if(element.ID.HasValue) //Impossible d'utiliser le stub avec cette condition
            {
                throw new ArgumentException("Erreur : Élément non ajouté car il appartient déjà à une collection");
            }*/
            collectionOeuvre.Add(LastID + 1, element);
            LastID++;
            element.ID = LastID; // L'id permet de connaître l'ordre d'ajout dans la collection à laquelle il appartient
        }

        public bool RetirerElement(Oeuvre element)
        {
            if (!element.ID.HasValue)
            {
                throw new ArgumentException("Erreur : Élément non ajouté car il n'appartient pas à une collection"); 
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
                if (o.Tags.Contains(tag))
                {
                    TagsManager.Enlever(o, tag);
                }
            }
        }


        // Méthodes retournant une LinkedList<Oeuve> triée
        public void TriAlphabetique()
        {
            collectionTriee.Clear();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.Nom))
            {
                collectionTriee.AddLast(o.Value);
            }
        }

        public void TriDateFiniCroissant()
        {
            collectionTriee.Clear();
            SortedDictionary<int, Oeuvre> collectionIDSansDateFini = new SortedDictionary<int, Oeuvre>();

            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.DateFini))
            {
                if (o.Value.DateFini.HasValue)
                {
                    collectionTriee.AddLast(o.Value);
                }
                else
                {
                    collectionIDSansDateFini.Add(o.Key, o.Value);
                }
            }

            foreach (KeyValuePair<int, Oeuvre> o in collectionIDSansDateFini)
            {
                collectionTriee.AddFirst(o.Value);
            }
        }

        public void TriDateFiniDecroissant()
        {
            collectionTriee.Clear();
            SortedDictionary<int, Oeuvre> collectionIDSansDateFini = new SortedDictionary<int, Oeuvre>();

            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderByDescending(kvp => kvp.Value.DateFini))
            {
                if (o.Value.DateFini.HasValue)
                {
                    collectionTriee.AddLast(o.Value);
                }
                else
                {
                    collectionIDSansDateFini.Add(o.Key, o.Value);
                }
            }

            foreach (KeyValuePair<int, Oeuvre> o in collectionIDSansDateFini)
            {
                collectionTriee.AddLast(o.Value);
            }
        }

        public void TriDatePrevueCroissant()
        {
            collectionTriee.Clear();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderBy(kvp => kvp.Value.DatePrevue))
            {
                if (o.Value.DatePrevue.HasValue)
                {
                    collectionTriee.AddLast(o.Value);
                }
            }
        }

        public void TriDatePrevueDecroissant()
        {
            collectionTriee.Clear();
            foreach (KeyValuePair<int, Oeuvre> o in collectionOeuvre.OrderByDescending(kvp => kvp.Value.DatePrevue))
            {
                if (o.Value.DatePrevue.HasValue)
                {
                    collectionTriee.AddLast(o.Value);
                }
            }
        }
        //

        // Méthode qui se charge de faire le tri sélectionné par l'utilisateur en fonction des paramètres ( utilise les méthodes de tris )
        public void SelectionCollection()
        {

            // Sélection du mode de tri
            switch (mode)
            {
                case ModeDeTri.DateDecroissante:
                    if (Planifie && !NonPlanifie) TriDatePrevueDecroissant();
                    else TriDateFiniDecroissant();
                    break;
                case ModeDeTri.DateCroissante:
                    if (Planifie && !NonPlanifie) TriDatePrevueCroissant();
                    else TriDateFiniCroissant();
                    break;
                case ModeDeTri.Alphabetique:
                    TriAlphabetique();
                    break;
            }
            

            
            LinkedListNode<Oeuvre> node = collectionTriee.First;
            LinkedListNode<Oeuvre> nextNode;

            while (node != null)
            {
                nextNode = node.Next;

                // Vérification des options
                if (node.Value is Film nodeFilm)
                {
                     if ((!TypeFilm) ||
                         (!Termine && nodeFilm.NbFini > 0) ||
                         (!EnCours && nodeFilm.Etat.HasValue) || 
                         (!NonCommence && !nodeFilm.Etat.HasValue && nodeFilm.NbFini == 0) ||
                         (!Planifie && nodeFilm.DatePrevue.HasValue) ||
                         (!NonPlanifie && !nodeFilm.DatePrevue.HasValue))
                     {
                        collectionTriee.Remove(node);
                         node = nextNode;
                         continue;
                     }
                }

                 if (node.Value is Livre nodeLivre)
                 {
                     if ((!TypeLivre) ||
                         (!Termine && nodeLivre.NbFini > 0) ||
                         (!EnCours && nodeLivre.Etat.HasValue) ||
                         (!NonCommence && !nodeLivre.Etat.HasValue && nodeLivre.NbFini == 0) ||
                         (!Planifie && nodeLivre.DatePrevue.HasValue) ||
                         (!NonPlanifie && !nodeLivre.DatePrevue.HasValue))
                     {
                        collectionTriee.Remove(node);
                         node = nextNode;
                         continue;
                     }
                 }

                 if (node.Value is Serie nodeSerie)
                 {
                     if ((!TypeSerie) ||
                         (!Termine && nodeSerie.NbFini > 0) ||
                         (!EnCours && nodeSerie.Etat != null) ||
                         (!NonCommence && nodeSerie.Etat == null && nodeSerie.NbFini == 0) ||
                         (!Planifie && nodeSerie.DatePrevue.HasValue) ||
                         (!NonPlanifie && !nodeSerie.DatePrevue.HasValue))
                     {
                        collectionTriee.Remove(node);
                         node = nextNode;
                         continue;
                     }
                 }

                // Vérification des tags des éléments sélectionnés
                foreach (string tagSelectionne in tagsSelectionnes)
                {
                    if (!node.Value.Tags.Contains(tagSelectionne))
                    {
                        collectionTriee.Remove(node);
                        node = nextNode;
                        continue;
                    }
                }

                node = nextNode;
            }
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



        /*
         * Ancienne façon de stocker la liste : Oeuvres stockées en plusieurs fois dans différents collections qui tri à l'insertion 
         * >> un peu plus rapide mais consumme plus de mémoire 
         * On se permet d'effectuer le tri à chaque fois car dans notre contexte il y aura difficilement plus de 5000 oeuvres stockées par utilisateur 
         * 
         * 
        /// Toutes les collections pour gérer les 3 principaux types de tri ///
        // Les éléments sont stockés dans plusieurs collections déjà triées pour évité de devoir réeffectuer le tri 

        // Collection triée alphabétiquement (seule collection qui contient tout les éléments enregistrés)
        private SortedSet<Oeuvre> CollectionTriAlpha = new SortedSet<Oeuvre>();

        // Collection triée en fonction de la date de la dernière fois où l'élément fut terminé
        private SortedSet<Oeuvre> CollectionTriDateFini = new SortedSet<Oeuvre>(new OeuvreCompDateFini());
        private SortedList<int, Oeuvre> CollectionNonFiniTriID = new SortedList<int, Oeuvre>(); // Collection pour les éléments sans date de visionnage/lecture

        // Collection triée en fonction de la date prévue pour continuer, visionner ou lire l'élément
        private SortedSet<Oeuvre> CollectionTriDatePrevue = new SortedSet<Oeuvre>(new OeuvreCompDatePrevue());

        /// Collection utilisé pour affichés les éléments sélectionnés/triés ///


        public void AjouterElement(Oeuvre element)
        {
            if(!CollectionTriAlpha.Add(element)) throw new ArgumentException("Erreur : Élément non ajouté car déjà dans la collection");

            if (element.DateFini.HasValue)
            {
                CollectionTriDateFini.Add(element);
            }
            else CollectionNonFiniTriID.Add(element.ID.Value, element);

            if (element.DatePrevue.HasValue) 
            {
                CollectionTriDatePrevue.Add(element); 
            }
        }

        public void RetirerElement(Oeuvre element)
        {
            if (!CollectionTriAlpha.Remove(element))
            {
                throw new ArgumentException("Erreur : Élément non supprimé car pas dans la collection"); ;
            }

            if (element.DateFini.HasValue)
            {
                CollectionTriDateFini.Remove(element);
            }
            else CollectionNonFiniTriID.Remove(element.ID.Value);

            if (element.DatePrevue.HasValue)
            {
                CollectionTriDatePrevue.Remove(element);
            }
        }



        public override string ToString()
        {
            // Tri Alphabétique
            string str = "-Ordre alphabétique-\n";
            foreach(Oeuvre o in CollectionTriAlpha)
            {
                str += $"{o.Nom}\n";
            }

            // Tri Date Fini
            str += "\n-Ordre DateFini croissant-\n";
            foreach (Oeuvre o in CollectionTriDateFini)
            {
                str += $"{o.Nom} {o.DateFini.Value.ToShortDateString()}\n";
            }
            foreach(Oeuvre o in CollectionNonFiniTriID.Values)
            {
                str += $"{o.Nom} Pas de DateFini\n";
            }
            str += "\n-Ordre DateFini décroissant-\n";
            foreach (Oeuvre o in CollectionNonFiniTriID.Values.OrderBy())
            {
                str += $"{o.Nom} Pas de DateFini\n";
            }
            foreach (Oeuvre o in CollectionTriDateFini.Reverse())
            {
                str += $"{o.Nom} {o.DateFini.Value.ToShortDateString()}\n";
            }

            // Tri Date Prevue
            str += "\n-Ordre DatePrevue croissant-\n";
            foreach (Oeuvre o in CollectionTriDatePrevue)
            {
                str += $"{o.Nom} {o.DatePrevue.Value.ToShortDateString()}\n";
            }
            str += "\n-Ordre DatePrevue décroissant-\n";
            foreach (Oeuvre o in CollectionTriDatePrevue.Reverse())
            {
                str += $"{o.Nom} {o.DatePrevue.Value.ToShortDateString()}\n";
            }

            return str;
        }
        */

    }

    /*
    public class OeuvreCompDateFini : Comparer<Oeuvre> // Comparateur entre Oeuvres par la date de dernier visionnage/lecture
    {
        public override int Compare(Oeuvre x, Oeuvre y)
        {
            if (x.DateFini.Value.CompareTo(y.DateFini.Value) != 0)
            {
                return x.DateFini.Value.CompareTo(y.DateFini.Value);
            }
            return x.ID.Value.CompareTo(y.ID.Value);
        }
    }

    public class OeuvreCompDatePrevue : Comparer<Oeuvre> // Comparateur entre Oeuvre par la date de visionnage/lecture prévue
    {
        public override int Compare(Oeuvre x, Oeuvre y)
        {
            if (x.DatePrevue.Value.CompareTo(y.DatePrevue.Value) != 0)
            {
                return x.DatePrevue.Value.CompareTo(y.DatePrevue.Value);
            }
            return x.ID.Value.CompareTo(y.ID.Value);
        }
    }
    */
 }