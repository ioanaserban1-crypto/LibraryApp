using LibraryApp.Models;
using LibraryApp.Config;

namespace LibraryApp.Services
{
    public class LibraryService
    {
        public bool ImprumutaCarte(Carte carte, Persoana persoana)
        {
            if (!carte.EsteDisponibila())
            {
                Console.WriteLine("Cartea nu este disponibila.");
                return false;
            }

            if (persoana.NumarCartiImprumutate >= Configurari.LimitaCarti)
            {
                Console.WriteLine("Limita de carti imprumutate a fost depasita.");
                return false;
            }

            carte.ExemplareImprumutate++;
            persoana.NumarCartiImprumutate++;

            Console.WriteLine("Imprumut realizat.");
            return true;
        }
    }
}