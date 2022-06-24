using Modele;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public FilmDTO(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, TimeSpan duree, TimeSpan? etat, AvisDTO avis, ObservableCollection<string> tags)
            : base(nom, image, dateFini, datePrevue, nbFini, tags)
        {
            Duree = duree;

            Etat = etat;

            Avis = avis;
        }
    }

    static class FilmExtensions
    {
        public static Film ToPOCO(this FilmDTO dto)
            => new Film(dto.Nom, dto.Image, dto.DateFini, dto.DatePrevue, dto.NbFini, dto.Duree, dto.Etat, dto.Avis.ToPOCO(), dto.Tags);
        public static FilmDTO ToDTO(this Film poco)
            => new FilmDTO(poco.Nom, poco.Image, poco.DateFini, poco.DatePrevue, poco.NbFini, poco.Duree, poco.Etat, poco.Avis.ToDTO(), poco.Tags);
    }
}
