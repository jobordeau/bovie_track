using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using static DataContractPersistance.SerieDTO;
using static Modele.Serie;

namespace DataContractPersistance
{
    [DataContract]
    class SerieDTO : OeuvreDTO
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public uint NbSaisons { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 1)]
        public EtatSerieDTO Etat { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public List<AvisDTO> ListeAvis { get; private set; }

        [DataContract]
        public class EtatSerieDTO
        {
            [DataMember(EmitDefaultValue = false, Order = 0)]
            public int Saison { get; set; }
            [DataMember(EmitDefaultValue = false, Order = 1)]
            public int Episode { get; set; }

            public EtatSerieDTO(int saison, int episode)
            {
                Saison = saison;
                Episode = episode;
            }
        }

        public SerieDTO(string nom, DateTime? dateFini, DateTime? datePrevue, uint nbFini, uint nbSaisons, EtatSerieDTO etat, List<AvisDTO> listeAvis)
           : base(nom, dateFini, datePrevue, nbFini)
        {
            NbSaisons = nbSaisons;
            Etat = etat;
            ListeAvis = listeAvis;
        }
    }

    static class SerieExtensions
    {
        
        public static Serie ToPOCO(this SerieDTO dto)
            => new Serie(dto.Nom, dto.DateFini, dto.DatePrevue, dto.NbFini, dto.NbSaisons, dto.Etat.ToPOCO(), dto.ListeAvis.ToPOCOs());
        public static SerieDTO ToDTO(this Serie poco)
            => new SerieDTO(poco.Nom, poco.DateFini, poco.DatePrevue, poco.NbFini, poco.NbSaisons, poco.Etat.ToDTO(), poco.ListeAvis.ToDTOs());
    }
    static class EtatSerieExtensions
    {
        public static EtatSerie ToPOCO(this EtatSerieDTO dto) {
            if(dto == null)
            {
                return null;
            }
            else
                return new EtatSerie(dto.Saison, dto.Episode);
            
        }
        
        public static EtatSerieDTO ToDTO(this EtatSerie dto)
        {
            if (dto == null)
            {
                return null;
            }
            else
                return new EtatSerieDTO(dto.Saison, dto.Episode);

        }

    }
}
