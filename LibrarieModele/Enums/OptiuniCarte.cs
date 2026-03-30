using System;

namespace LibrarieModele.Enums
{
    [Flags]
    public enum OptiuniCarte
    {
        Niciuna = 0,
        Imprumutabila = 1,
        Rezervabila = 2,
        EditieSpeciala = 4,
        DisponibilaOnline = 8
    }
}