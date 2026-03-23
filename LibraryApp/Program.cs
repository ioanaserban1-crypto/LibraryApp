using LibraryApp.Models;
using LibraryApp.Services;

class Program
{
    static void Main()
    {
        LibraryService service = new LibraryService();
        int optiune;

        do
        {
            Console.WriteLine("\n--- MENIU BIBLIOTECA ---");
            Console.WriteLine("1. Adauga carte");
            Console.WriteLine("2. Afiseaza toate cartile");
            Console.WriteLine("3. Cauta carte dupa titlu");
            Console.WriteLine("4. Cauta carte dupa autor");
            Console.WriteLine("0. Iesire");
            Console.Write("Alege optiunea: ");

            optiune = int.Parse(Console.ReadLine());

            switch (optiune)
            {
                case 1:
                    Console.Write("Id carte: ");
                    int idCarte = int.Parse(Console.ReadLine());

                    Console.Write("Titlu carte: ");
                    string titlu = Console.ReadLine();

                    Console.Write("Id autor: ");
                    int idAutor = int.Parse(Console.ReadLine());

                    Console.Write("Nume autor: ");
                    string numeAutor = Console.ReadLine();

                    Console.Write("Numar exemplare: ");
                    int numarExemplare = int.Parse(Console.ReadLine());

                    Autor autor = new Autor(idAutor, numeAutor);
                    Carte carte = new Carte(idCarte, titlu, autor, numarExemplare);

                    service.AdaugaCarte(carte);
                    break;

                case 2:
                    service.AfiseazaCarti();
                    break;

                case 3:
                    Console.Write("Introdu titlul cautat: ");
                    string titluCautat = Console.ReadLine();
                    service.CautaDupaTitlu(titluCautat);
                    break;

                case 4:
                    Console.Write("Introdu numele autorului: ");
                    string autorCautat = Console.ReadLine();
                    service.CautaDupaAutor(autorCautat);
                    break;

                case 0:
                    Console.WriteLine("Aplicatia s-a inchis.");
                    break;

                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }

        } while (optiune != 0);
    }
}