using Modele;
using System;
using System.Collections.Generic;
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

        public LivreDTO(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbPages, string auteur, int? etat, float? note, string commentaire)
            : base(nom, dateFini, datePrevue, nbFini)
        {
            NbPages = nbPages;

            Auteur = auteur;

            Etat = etat;

            Avis = new AvisDTO(note, commentaire);
        }
    }

    static class LivreExtensions
    {
        public static Livre ToPOCO(this LivreDTO dto)
            => new Livre(dto.Nom, dto.DateFini, dto.DatePrevue, dto.NbFini, dto.NbPages, dto.Auteur, dto.Etat, dto.Avis.ToPOCO().Note, dto.Avis.ToPOCO().Commentaire);
        public static LivreDTO ToDTO(this Livre poco)
            => new LivreDTO(poco.Nom, poco.DateFini, poco.DatePrevue, poco.NbFini, poco.NbPages, poco.Auteur, poco.Etat, poco.Avis.ToDTO().Note, poco.Avis.ToDTO().Commentaire);
    }
}
