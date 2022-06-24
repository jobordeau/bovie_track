using Data;
using DataContractPersistance;
using Modele;
using System;

namespace Test_DataContract
{
    class Program
    {
        static void Main(string[] args)
        {
                Manager manager = new Manager(new Stub());
                manager.ChargeDonnees();
                manager.Persistance = new DataContractPersJSON();
                manager.AssocierTag(manager.GetOeuvre(12), "ça marche");
                manager.SauvegardeDonnees();
                manager.SauvegardeDonnees();

        }
    }
}
