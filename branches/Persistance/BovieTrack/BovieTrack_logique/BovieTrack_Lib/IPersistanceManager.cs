using System;
using System.Collections.Generic;
using System.Text;

namespace Modele
{
    public interface IPersistanceManager
    {
        (Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID) ChargeDonees();

        void SauvegardeDonnees(Dictionary<int, Oeuvre> collection, SortedSet<string> tags, int lastID);
    }
}
