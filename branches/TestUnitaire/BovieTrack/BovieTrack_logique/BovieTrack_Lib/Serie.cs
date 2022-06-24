﻿using System;
using System.Collections.Generic;

namespace Modele
{
    public class Serie : Oeuvre
    {
        public class EtatSerie // Classe utilisée exclusivement par Série pour indiqué l'état du visionnage
        {
            public int Saison { get; set; }
            public int Episode { get; set; }

            public EtatSerie(int saison, int episode)
            {
                if (saison < 1 || episode < 1)
                {
                    throw new ArgumentException("Erreur : L'état du visionnage doit être fait d'une saison et d'un épisode tout les 2 supérieurs à 0");
                }
                Saison = saison;
                Episode = episode;
            }

            public override string ToString()
            {
                return $"Saison {Saison} Épisode {Episode}";
            }
        }

        public uint NbSaisons { get; private set; } // Le nombre de saisons ne peut pas être inférieur ou égal à 0
  
        public EtatSerie Etat { get; private set; }
        public List<Avis> ListeAvis { get; private set; }

        public Serie(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
            : base(nom, dateFini, datePrevue, nbFini)
        {
            NbSaisons = nbSaisons;

            if (etatSaison.HasValue && etatEpisode.HasValue)
            {
                Etat = new EtatSerie(etatSaison.Value, etatEpisode.Value);
            }
            else Etat = null;
            
            if (notes.Length != NbSaisons || commentaires.Length != NbSaisons)
            {
                throw new ArgumentException("Erreur : Le nombre de notes et de commentaires doit être le même et égal au nombre de saisons");
            }

            ListeAvis = new List<Avis>();
            for (int i = 0; i < notes.Length; i++)
            {
                ListeAvis.Add(new Avis(notes[i], commentaires[i]));
            }
        }

        public void MajSerie(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, int? etatSaison, int? etatEpisode, float?[] notes, string[] commentaires)
        {

            MajOeuvre(nom, dateFini, datePrevue, nbFini);

            NbSaisons = nbSaisons;

            if (etatSaison.HasValue && etatEpisode.HasValue)
            {
                if(etatSaison.Value > NbSaisons)
                {
                    throw new ArgumentException("Erreur : La saison en cours de visionnage ne peut pas être supérieur au nombre de saisons");
                }
                Etat = new EtatSerie(etatSaison.Value, etatEpisode.Value);
            }
            else Etat = null;
     
            if (notes.Length != NbSaisons || commentaires.Length != NbSaisons)
            {
                throw new ArgumentException("Erreur : Le nombre de notes et de commentaires doit être le même et égal au nombre de saisons"); // N'arrive jamais
            }

            ListeAvis = new List<Avis>();
            for (int i = 0; i < notes.Length; i++)
            {
                ListeAvis.Add(new Avis(notes[i], commentaires[i]));
            }
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
