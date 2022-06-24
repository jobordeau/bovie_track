using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Modele;

namespace DataContractPersistance
{
    public class DataContractPers : IPersistanceManager
    {
       
        public string RelativePath { get; set; } = "..//XML";

        public string FilePath => Path.Combine(Directory.GetCurrentDirectory(), RelativePath);

        public string FileName { get; set; } = "BovieTrack.xml";

        public string PersFile => Path.Combine(FilePath, FileName);

        public Dictionary<int, Oeuvre> Collection { get; set; } = new Dictionary<int, Oeuvre>();

        protected XmlObjectSerializer Serializer { get; set; } = new DataContractSerializer(typeof(DataToPersist));
        public virtual (Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID) ChargeDonees()
        {
            if (!Directory.Exists(FilePath))
            {
                throw new FileNotFoundException("Le fichier de persistance n'existe pas");
            }
            DataToPersist data;

            using (Stream s = File.OpenRead(PersFile))
            {
                data = Serializer.ReadObject(s) as DataToPersist;
            }
            
            foreach (KeyValuePair<int, OeuvreDTO> o in data.collection)
            {
                Collection.Add(o.Key, o.Value.ToPOCO());
            }

            return (Collection, data.tags, data.LastID);
        }

        public virtual void SauvegardeDonnees(Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID)
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

            var settings = new XmlWriterSettings() { Indent = true };
            using(TextWriter tw=File.CreateText(PersFile))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, settings))
                {
                    Serializer.WriteObject(writer, data);
                }
            }

                
        }
    }
}
