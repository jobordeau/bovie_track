using Modele;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractPersistance
{
    [DataContract]
    class LivreDTO : OeuvreDTO
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public uint NbPages { get; private set; } // Le nombre de pages ne peut pas être inférieur à 0
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public string Auteur { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public int? Etat { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public AvisDTO Avis { get; private set; }

        public LivreDTO(string nom, string image, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, AvisDTO avis, ObservableCollection<string> tags)
            : base(nom, image, dateFini, datePrevue, nbFini, tags)
        {
            NbPages = nbPages;

            Auteur = auteur;

            Etat = etat;

            Avis = avis;
        }
    }

    static class LivreExtensions
    {
        public static Livre ToPOCO(this LivreDTO dto)
            => new Livre(dto.Nom, dto.Image, dto.DateFini, dto.DatePrevue, dto.NbFini, dto.NbPages, dto.Auteur, dto.Etat, dto.Avis.ToPOCO(), dto.Tags);
        public static LivreDTO ToDTO(this Livre poco)
            => new LivreDTO(poco.Nom, poco.Image, poco.DateFini, poco.DatePrevue, poco.NbFini, poco.NbPages, poco.Auteur, poco.Etat, poco.Avis.ToDTO(), poco.Tags);
    }
}
