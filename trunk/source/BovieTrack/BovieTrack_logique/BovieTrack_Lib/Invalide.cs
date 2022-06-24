using System;
using System.Collections.Generic;
using System.Text;

namespace Modele
{
    public enum Invalide : byte
    {
        nom,
        duree,
        dateFini,
        datePrevue,
        etat,
        avis,
        auteur // Spécifique aux livres
    }
}
