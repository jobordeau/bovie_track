using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Modele
{
    public interface IPersistanceManager
    {
        (Dictionary<int, Oeuvre> collection, List<string> tags, int lastID) ChargeDonnees();

        void SauvegardeDonnees(Dictionary<int, Oeuvre> collection, List<string> tags, int lastID);
    }
}
