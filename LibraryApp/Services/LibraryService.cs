using LibraryApp.Models;
using LibraryApp.Config;

namespace LibraryApp.Services
{
    public class LibraryService
    {
        private List<Carte> carti = new List<Carte>();

        public void AdaugaCarte(Carte carte)
        {
            carti.Add(carte);
            Console.WriteLine("Cartea a fost adaugata cu succes.");
        }

        public void AfiseazaCarti()
        {
            if (carti.Count == 0)
            {
                Console.WriteLine("Nu exista carti in biblioteca.");
                return;
            }

            foreach (Carte carte in carti)
            {
                Console.WriteLine(carte);
            }
        }

        public void CautaDupaTitlu(string titlu)
        {
            bool gasita = false;

            foreach (Carte carte in carti)
            {
                if (carte.Titlu.ToLower().Contains(titlu.ToLower()))
                {
                    Console.WriteLine(carte);
                    gasita = true;
                }
            }

            if (!gasita)
            {
                Console.WriteLine("Nu exista carti cu acest titlu.");
            }
        }

        public void CautaDupaAutor(string numeAutor)
        {
            bool gasita = false;

            foreach (Carte carte in carti)
            {
                if (carte.Autor.Nume.ToLower().Contains(numeAutor.ToLower()))
                {
                    Console.WriteLine(carte);
                    gasita = true;
                }
            }

            if (!gasita)
            {
                Console.WriteLine("Nu exista carti ale acestui autor.");
            }
        }

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