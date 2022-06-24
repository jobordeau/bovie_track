using Modele;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractPersistance
{
    [DataContract]
    class FilmDTO : OeuvreDTO
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public TimeSpan Duree { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public TimeSpan? Etat { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public AvisDTO Avis { get; private set; }

        public FilmDTO(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, float? note, string commentaire)
            : base(nom, dateFini, datePrevue, nbFini)
        {
            Duree = duree;

            Etat = etat;

            Avis = new AvisDTO(note, commentaire);
        }
    }

    static class FilmExtensions
    {
        public static Film ToPOCO(this FilmDTO dto)
            => new Film(dto.Nom, dto.DateFini, dto.DatePrevue, dto.NbFini, dto.Duree, dto.Etat, dto.Avis.ToPOCO().Note, dto.Avis.ToPOCO().Commentaire);
        public static FilmDTO ToDTO(this Film poco)
            => new FilmDTO(poco.Nom, poco.DateFini, poco.DatePrevue, poco.NbFini, poco.Duree, poco.Etat, poco.Avis.ToDTO().Note, poco.Avis.ToDTO().Commentaire);
    }
}
