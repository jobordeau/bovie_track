using System;
using System.Collections.Generic;
using System.Text;

namespace Appli
{
    [Flags]
    public enum ModeEdition : byte
    {
        Creation,
        Modification,
        DeFilm,
        DeLivre,
        DeSerie,
        ModificationDeFilm = Modification | DeFilm,
        ModificationDeLivre = Modification | DeLivre,
        ModificationDeSerie = Modification | DeSerie
    }
}
