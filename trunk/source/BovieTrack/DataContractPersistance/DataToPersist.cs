using Modele;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractPersistance
{
    [DataContract]
    [KnownType(typeof(Dictionary<int, Oeuvre>))]
    [KnownType(typeof(List<string>))]
    class DataToPersist
    {
        [DataMember(EmitDefaultValue = false)]
        public int LastID;

        [DataMember(EmitDefaultValue = false)]
        public Dictionary<int, OeuvreDTO> collection = new Dictionary<int, OeuvreDTO>();

        [DataMember(EmitDefaultValue = false)]
        public List<string> tags = new List<string>();
    }
}
