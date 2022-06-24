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
            Manager manager = new Manager(new DataContractPersJSON());
            manager.ChargeDonnees();
            manager.SauvegardeDonnees();
        }
    }
}
