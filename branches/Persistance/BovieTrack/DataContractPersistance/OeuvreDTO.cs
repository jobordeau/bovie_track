using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractPersistance
{
    [DataContract]
    [KnownType(typeof(FilmDTO))]
    [KnownType(typeof(SerieDTO))]
    [KnownType(typeof(LivreDTO))]
    abstract class OeuvreDTO
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public int? ID { get; set; } = null;

        [DataMember(EmitDefaultValue = false, Order = 1)]
        public string Nom { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public uint NbFini { get; private set; } // Erreur : Le nombre de fois où l'élément à été fini doit être renseigné et supérieur ou égal à 0

        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Image { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 4)]
        public DateTime? DateFini { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public DateTime? DatePrevue { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 6)]
        public List<string> Tags { get; private set; }

        public OeuvreDTO(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini)
        {
            Nom = nom;
            DateFini = dateFini;
            DatePrevue = datePrevue;
            NbFini = nbFini;

        }
    }
    static class OeuvreExtensions
    {
        public static Oeuvre ToPOCO(this OeuvreDTO dto)
        {
            if (dto.GetType() == typeof(FilmDTO))
            {
                FilmDTO film = (FilmDTO)dto;
                return film.ToPOCO();
            }
            else if (dto.GetType() == typeof(SerieDTO))
            {
                SerieDTO serie = (SerieDTO)dto;
                return serie.ToPOCO();
            }
            LivreDTO livre = (LivreDTO)dto;
            return livre.ToPOCO();
        }

        public static OeuvreDTO ToDTO(this Oeuvre poco)
        {
            if (poco.GetType() == typeof(Film))
            {
                Film film = (Film)poco;
                return film.ToDTO();
            }
            else if (poco.GetType() == typeof(Serie))
            {
                Serie serie = (Serie)poco;
                return serie.ToDTO();
            }
            Livre livre = (Livre)poco;
            return livre.ToDTO();
        }
    }
}
