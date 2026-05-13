using LibrarieModele;
using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelUIWPF.ViewModels
{
    public class CarteViewModel
    {
        public Carte CarteCurenta { get; set; }

        public CarteViewModel()
        {
            Autor autor = new Autor(1, "Marin Preda");

            CarteCurenta = new Carte(
                1,                  // id
                "Moromeții",        // titlu
                autor,              // obiect Autor
                10,                 // nr exemplare
                GenCarte.Roman,     // enum
                OptiuniCarte.Niciuna
            );
        }
    }
}