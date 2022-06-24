using Modele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace DataContractPersistance
{
    public class DataContractPersJSON : DataContractPers
    {
        public DataContractPersJSON()
        {
            RelativePath = "..//JSON";
            FileName = "BovieTrack.json";
            Serializer = new DataContractJsonSerializer(typeof(DataToPersist));
        }

        public override void SauvegardeDonnees(Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID)
        {


            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            DataToPersist data = new DataToPersist();
            foreach (KeyValuePair<int, Oeuvre> o in collection)
            {
                data.collection.Add(o.Key, o.Value.ToDTO());
            }
            data.tags = tags;
            data.LastID = lastID;

            using (Stream writer = File.Create(PersFile))
            {
                Serializer.WriteObject(writer, data);
            }

        }
    }
}
