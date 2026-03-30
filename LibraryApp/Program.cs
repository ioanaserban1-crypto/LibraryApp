using LibrarieModele.Models;
using LibrarieModele.Enums;
using NivelStocareDate.Administrare;

class Program
{
    static void Main()
    {
        AdministratorEntitateMemorie admin = new AdministratorEntitateMemorie();
        int optiune;

        do
        {
            Console.WriteLine("\n=== MENIU BIBLIOTECA ===");
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
                    AdaugaCarte(admin);
                    break;

                case 2:
                    AfiseazaCarti(admin);
                    break;

                case 3:
                    CautaDupaTitlu(admin);
                    break;

                case 4:
                    CautaDupaAutor(admin);
                    break;

                case 0:
                    Console.WriteLine("Aplicatia s-a inchis.");
                    break;

                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }

        } while (optiune != 0);
    }

    // =========================
    // ADAUGARE CARTE
    // =========================
    static void AdaugaCarte(AdministratorEntitateMemorie admin)
    {
        Console.Write("Id carte: ");
        int idCarte = int.Parse(Console.ReadLine());

        Console.Write("Titlu carte: ");
        string titlu = Console.ReadLine();

        Console.Write("Id autor: ");
        int idAutor = int.Parse(Console.ReadLine());

        Console.Write("Nume autor: ");
        string numeAutor = Console.ReadLine();

        Console.Write("Numar exemplare: ");
        int nrExemplare = int.Parse(Console.ReadLine());

        // alegere gen
        Console.WriteLine("Alege genul:");
        Console.WriteLine("1. Roman");
        Console.WriteLine("2. Poezie");
        Console.WriteLine("3. Istorie");
        Console.WriteLine("4. Stiinta");
        Console.WriteLine("5. Biografie");
        Console.WriteLine("6. Copii");

        int genOpt = int.Parse(Console.ReadLine());
        GenCarte gen = (GenCarte)(genOpt - 1);

        // opțiuni (Flags)
        OptiuniCarte optiuni = OptiuniCarte.Niciuna;

        Console.Write("Este imprumutabila? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
            optiuni |= OptiuniCarte.Imprumutabila;

        Console.Write("Este rezervabila? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
            optiuni |= OptiuniCarte.Rezervabila;

        Autor autor = new Autor(idAutor, numeAutor);

        Carte carte = new Carte(idCarte, titlu, autor, nrExemplare, gen, optiuni);

        admin.AdaugaCarte(carte);

        Console.WriteLine("✔ Cartea a fost adaugata!");
    }

    // =========================
    // AFISARE CARTI
    // =========================
    static void AfiseazaCarti(AdministratorEntitateMemorie admin)
    {
        var carti = admin.GetCarti();

        if (carti.Count == 0)
        {
            Console.WriteLine("Nu exista carti.");
            return;
        }

        foreach (var c in carti)
        {
            Console.WriteLine(c);
        }
    }

    // =========================
    // CAUTARE TITLU
    // =========================
    static void CautaDupaTitlu(AdministratorEntitateMemorie admin)
    {
        Console.Write("Titlu cautat: ");
        string titlu = Console.ReadLine();

        var rezultate = admin.CautaDupaTitlu(titlu);

        if (rezultate.Count == 0)
        {
            Console.WriteLine("Nu s-au gasit carti.");
            return;
        }

        foreach (var c in rezultate)
        {
            Console.WriteLine(c);
        }
    }

    // =========================
    // CAUTARE AUTOR
    // =========================
    static void CautaDupaAutor(AdministratorEntitateMemorie admin)
    {
        Console.Write("Autor cautat: ");
        string autor = Console.ReadLine();

        var rezultate = admin.CautaDupaAutor(autor);

        if (rezultate.Count == 0)
        {
            Console.WriteLine("Nu s-au gasit carti.");
            return;
        }

        foreach (var c in rezultate)
        {
            Console.WriteLine(c);
        }
    }
}