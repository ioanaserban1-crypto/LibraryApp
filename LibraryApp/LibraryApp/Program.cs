using LibraryApp.Models;
using LibraryApp.Services;
using LibraryApp.Models;

class Program
{
    static void Main()
    {
        Autor autor = new Autor(1, "Ion Creanga");

        Carte carte = new Carte(1, "Amintiri din copilarie", autor, 2);

        Persoana persoana = new Persoana(1, "Maria Popescu");

        LibraryService service = new LibraryService();

        Console.WriteLine("Disponibilitate carte: " + carte.EsteDisponibila());

        service.ImprumutaCarte(carte, persoana);
        service.ImprumutaCarte(carte, persoana);
        service.ImprumutaCarte(carte, persoana);
    }
}