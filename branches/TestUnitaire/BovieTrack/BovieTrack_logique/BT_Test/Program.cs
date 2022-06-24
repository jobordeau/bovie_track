using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading;
using Modele;
using static System.Console;

namespace BT_Test
{
    
    class Program
    {
        /// <summary>
        /// 
        ///
        ///
        ///    
        /// </summary>
        public static Manager manager = new Manager();

        static void Main(string[] args)
        {
            // BIP BIP

            Beep();
         
            
            Beep();
        }

        /*
        static void TestCollection()
        {
            Console.SetWindowSize(40, 50);

            manager.AjouterFilm("Ratatouille", new DateTime(2018, 8, 12), new DateTime(2022, 5, 13), 5, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterFilm("Aladin", new DateTime(2018, 8, 12), new DateTime(2022, 5, 13), 7, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterFilm("Harry Potter", new DateTime(2012, 8, 12), new DateTime(2020, 7, 3), 9, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterLivre("Harry Potter", new DateTime(2013, 8, 12), new DateTime(2020, 7, 3), 9, 500, "JK Rolling", null, null, null);

            manager.AjouterFilm("Pocahontas", null, new DateTime(2023, 7, 3), 0, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterFilm("Frères des ours", null, new DateTime(2021, 7, 3), 1, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterFilm("Mulan", new DateTime(2014, 8, 12), null, 3, new TimeSpan(2, 15, 0), null, null, null);
            manager.AjouterFilm("Le Roi Lion", null, null, 0, new TimeSpan(2, 15, 0), null, null, null);

            manager.AjouterNouveauTag("tag");
            manager.AjouterNouveauTag("tag2");
            manager.AjouterNouveauTag("tag3");

            manager.AssocierTag(manager.GetOeuvre(1), "tag");
            manager.EnleverTag(manager.GetOeuvre(1), "tag");
            manager.AssocierTag(manager.GetOeuvre(3), "tag");
            manager.AssocierTag(manager.GetOeuvre(1), "tag2");
            manager.AssocierTag(manager.GetOeuvre(2), "tag3");
            manager.SupprimerUnTag("tag3");
            //manager.SupprimerOeuvre(manager.GetOeuvre(4));


            // Test 1

            WriteLine("\n-Tri de Date Fini Decroissant-\n");
            foreach (Oeuvre o in manager.GetCollectionAffichee())
            {
                Write($"{o.ID} {o.Nom} ");
                if (o.DateFini.HasValue) Write($" | {o.DateFini.Value.ToShortDateString()}\n");
                else Write(" | Pas de DateFini\n");

            }

            // Test 2
            manager.CollectionMgr.mode = ModeDeTri.DateCroissante;
            manager.CollectionMgr.Planifie = false;

            WriteLine("\n-Tri Date Fini Croissant-\n");
            foreach (Oeuvre o in manager.GetCollectionAffichee())
            {
                Write($"{o.ID} {o.Nom} ");
                if (o.DateFini.HasValue) Write($" | {o.DateFini.Value.ToShortDateString()}\n");
                else Write(" | Pas de DateFini\n");
            }


            // Test 3
            manager.CollectionMgr.Planifie = true;
            manager.CollectionMgr.NonPlanifie = false;

            WriteLine("\n-Tri Date Prevue Croissant-\n");
            foreach (Oeuvre o in manager.GetCollectionAffichee())
            {
                Write($"{o.ID} {o.Nom} ");
                if (o.DatePrevue.HasValue) Write($" | {o.DatePrevue.Value.ToShortDateString()}\n");
                else Write(" | Pas de DatePrevue\n");
            }

            // Test 4
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.CollectionMgr.NonPlanifie = true;
            manager.CollectionMgr.TypeLivre = false;


            WriteLine("\n-Tri Alphabétique-\n");
            foreach (Oeuvre o in manager.GetCollectionAffichee())
            {
                WriteLine($"{o.ID} {o.Nom}");
            }
            WriteLine("\n");
            manager.ActualiseTrie(false, false, false, false, false, false, false, false, ModeDeTri.DateCroissante);
            WriteLine(manager);
        }

        static void TestClassesEtSaisies() // Test de toutes les saisies de classes 
        {
            // Test Film
            Oeuvre o1 = null;
            WriteLine("-Saisie d'un film-");
            try { o1 = SaisieFilm(); }
            catch (Exception e) { WriteLine(e.Message); }

            if (o1 == null) WriteLine("---"); else WriteLine($"Film créé (ID ={o1.ID}) :\n\n{o1}\n{o1.ID}\n\n");

            //Test Livre
            Oeuvre o2 = null;
            WriteLine("-Saisie d'un livre-");
            try { o2 = SaisieLivre(); }
            catch (Exception e) { WriteLine(e.Message); }

            if (o2 == null) WriteLine("---");
            else
            {
                List<string> tags = new List<string> { "Un tag", "Un autre tag", "Encore un tag" };
                o2.ChangerLesTags(tags);
                WriteLine($"Livre créé (ID ={o2.ID}) :\n\n{o2}\n\n");

            }

            //Test Série
            Oeuvre o3 = null;
            WriteLine("-Saisie d'une série-");
            try { o3 = SaisieSerie(); }
            catch (Exception e) { WriteLine(e.Message); }
            if (o3 == null) WriteLine("---"); else WriteLine($"Série créée  (ID ={o3.ID}) :\n\n{o3}\n{o3.ID}\n\n");
        }

        static void SaisieOeuvre(out string nom, out int nbFini, ref DateTime? dateFini, ref DateTime? datePrevue) // Saisie d'une oeuvre à partir de la console
        {
            //Saisie du titre
            Write("Titre : ");
            nom = ReadLine();

            //Saisie du nombre de fois terminé
            Write("Nombre de fois terminé(e) : ");
            if (!int.TryParse(ReadLine(), out nbFini)) throw new ArgumentException("Erreur : Chiffres uniquement");
            // Convertir la variable lue est retourne true si la conversion à réussi sinon false


            string lecture;
            // Saisie de la date de la dernière fois où l'oeuvre a été finie
            if (nbFini != 0)
            {
                Write("Date de la dernière fois où vous avez terminé l'oeuvre (Ex: 20/09/2001) ou rien si oubliée : ");
                lecture = ReadLine();
                if (!string.IsNullOrWhiteSpace(lecture))
                {
                    if (!DateTime.TryParse(lecture, out DateTime dateFiniInput)) throw new ArgumentException("Erreur : Format de date invalide (Ex: 20/09/2001)");
                    else dateFini = dateFiniInput;
                }
            }     

            // Saisie de la date planifiée
            Write("Date à laquelle vous voulez continuer/commencer l'oeuvre (Ex: 20/09/2001) ou rien si non planifiée : ");
            lecture = ReadLine();
            if (!string.IsNullOrWhiteSpace(lecture))
            {
                if (!DateTime.TryParse(lecture, out DateTime datePrevueInput)) throw new ArgumentException("Erreur : Format de date invalide (Ex: 20/09/2001)");
                else datePrevue = datePrevueInput;
            }
        }

        static Film SaisieFilm() // Saisie d'un film à partir de la console
        {

            DateTime? dateFini = null;
            DateTime? datePrevue = null;

            SaisieOeuvre(out string nom, out int nbFini, ref dateFini, ref datePrevue);

                /// Saisie spécifique au film ///

            //Saisie de la durée du film
            Write("Durée du film (Ex: 02h15): ");
            if (!TimeSpan.TryParseExact(ReadLine(), "hh\\hmm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan duree)) throw new ArgumentException("Erreur : Format de durée invalide (Ex: 02h15)");
            // Convertir la variable lue est retourne true si la conversion à réussi sinon false

            //Saisie de l'état du visionnage du film
            string lecture;
            TimeSpan? etat;
            Write("Heure d'arrêt (Ex: 00h38) ou rien si pas de visionnage en cours: ");
            lecture = ReadLine();
            if (!string.IsNullOrWhiteSpace(lecture))
            {
                if (!TimeSpan.TryParseExact(lecture, "hh\\hmm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan etatLu)) throw new ArgumentException("Erreur : Format de durée invalide (Ex: 2h15)");
                etat = etatLu;
            }
            else etat = null;
                
            // Saisie de l'avis
            SaisieUnAvis(out float? note, out string commentaire);
            return new Film(nom, dateFini, datePrevue, nbFini, duree, etat, note, commentaire);
           
        }

        static Livre SaisieLivre() // Saisie d'un livre à partir de la console
        {
            DateTime? dateFini = null;
            DateTime? datePrevue = null;

            SaisieOeuvre(out string nom, out int nbFini, ref dateFini, ref datePrevue);

            /// Saisie spécifique au Livre ///

            // Saisie de l'auteur du livre
            Write("Auteur du livre :");
            string auteur = ReadLine();

            //Saisie de la durée du livre
            Write("Nombre de pages du livre: ");
            if (!int.TryParse(ReadLine(), out int nbPages)) throw new ArgumentException("Erreur : Chiffres uniquement");
            // Convertir la variable lue est retourne true si la conversion à réussi sinon false

            //Saisie de l'état de la lecture
            string lecture;
            int? etat;
            Write("Page d'arrêt ou rien si pas de lecture en cours: ");
            lecture = ReadLine();
            if (!string.IsNullOrWhiteSpace(lecture))
            {
                if (!int.TryParse(lecture, out int etatLu)) throw new ArgumentException("Erreur : Chiffres uniquement");
                etat = etatLu;
            }
            else etat = null;

            // Saisie de l'avis
            SaisieUnAvis(out float? note, out string commentaire);
            return new Livre(nom, dateFini, datePrevue, nbFini, nbPages, auteur, etat, note, commentaire);

        }

        static Serie SaisieSerie() // Saisie d'un film à partir de la console
        {

            DateTime? dateFini = null;
            DateTime? datePrevue = null;

            SaisieOeuvre(out string nom, out int nbFini, ref dateFini, ref datePrevue);

            /// Saisie spécifique à la série ///

            //Saisie du nombre de saison de la série
            Write("Nombre de saison: ");
            if (!int.TryParse(ReadLine(), out int nbSaisons)) throw new ArgumentException("Erreur : Chiffres uniquement");
            // Convertir la variable lue est retourne true si la conversion à réussi sinon false

            //Saisie de l'état du visionnage de la série
            string lecture;
            do
            {
                Write("La série est-elle en cours de visionnage ? (o/n): ");
                lecture = ReadLine();
                if (lecture == "n") break;
            } while (lecture != "o");
            int? saisonEtat, episodeEtat;
            if (lecture == "o")
            {
                WriteLine("-Épisode auquel continuer-");
                Write("Numéro de la saison :");
                if (!int.TryParse(ReadLine(), out int saisonEtatLu)) throw new ArgumentException("Erreur : Chiffres uniquement");
                Write("Numéro de l'épisode :");
                if (!int.TryParse(ReadLine(), out int episodeEtatLu)) throw new ArgumentException("Erreur : Chiffres uniquement");
                saisonEtat = saisonEtatLu;
                episodeEtat = episodeEtatLu;
            }
            else
            {
                saisonEtat = null;
                episodeEtat = null;
            }

            // Saisie des avis

            float?[] notes = new float?[nbSaisons];
            string[] commentaires = new string[nbSaisons];
            for(int i = 0; i < nbSaisons; i++)
            {
                WriteLine($"-Avis saison {i+1}-");
                SaisieUnAvis(out float? note, out string commentaire);
                notes[i] = note;
                commentaires[i] = commentaire;
            }

            return new Serie(nom, dateFini, datePrevue, nbFini, nbSaisons, saisonEtat, episodeEtat, notes, commentaires);
        }

        static void SaisieUnAvis(out float? note, out string commentaire) // Saisie d'une note et d'un commentaire
        {
            string lecture;
            Write("Saisir une note entre 0 et 10 ou rien si pas de note: ");
            lecture = ReadLine();
            if (!string.IsNullOrWhiteSpace(lecture))
            {
                if (!float.TryParse(lecture, out float noteLue)) throw new ArgumentException("Erreur : Note invalide");
                else note = noteLue;
            }
            else note = null;

            Write("Saisir un commentaire ou rien: ");
            lecture = ReadLine();
            if (string.IsNullOrWhiteSpace(lecture)) commentaire = null;
            else commentaire = lecture;
        }
        */
    }

}
