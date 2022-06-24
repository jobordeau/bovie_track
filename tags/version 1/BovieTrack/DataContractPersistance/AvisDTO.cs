using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractPersistance
{
    [DataContract]
    class AvisDTO
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public float? Note { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public string Commentaire { get; set; }

        public AvisDTO(float? note, string commentaire)
        {
            Note = note;
            Commentaire = commentaire;
        }
    }

    static class AvisExtensions
    {
        public static Avis ToPOCO(this AvisDTO dto)
            => new Avis(dto.Note, dto.Commentaire);

        public static List<Avis> ToPOCOs(this List<AvisDTO> dtos)
        {
            List<Avis> avis=new List<Avis>();
            foreach(AvisDTO dto in dtos)
            {
                avis.Add(dto.ToPOCO());
            }
            return avis;
        }

        public static List<AvisDTO> ToDTOs(this List<Avis> pocos)
        {
            List<AvisDTO> avis = new List<AvisDTO>();
            foreach (Avis poco in pocos)
            {
                avis.Add(poco.ToDTO());
            }
            return avis;
        }
        public static AvisDTO ToDTO(this Avis poco)
            => new AvisDTO(poco.Note, poco.Commentaire);
            

    }

}
