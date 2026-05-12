using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelStocareDate.Administrare
{
    // Implementare care stocheaza datele IN FISIERE TEXT.
    // Datele persista dupa inchiderea aplicatiei.
    // Implementeaza aceeasi interfata IStocareDate ca si
    // AdministratorEntitateMemorie — codul din UI nu trebuie
    // sa stie ce implementare e folosita in spate (polimorfism).
    //
    // FORMAT FISIER CARTI (carti.txt):
    //   id|titlu|idAutor|numeAutor|numarExemplare|exemplareImprumutate|gen|optiuni
    //   Exemplu: 1|Morometii|1|Marin Preda|10|2|Roman|Niciuna
    //
    // FORMAT FISIER AUTORI (autori.txt):
    //   id|nume
    //   Exemplu: 1|Marin Preda
    //
    // FORMAT FISIER PERSOANE (persoane.txt):
    //   id|nume
    //   Exemplu: 1|Ion Ionescu
    //
    // FORMAT FISIER IMPRUMUTURI (imprumuturi.txt):
    //   id|idPersoana|numePersoana|idCarte|titluCarte|dataImprumut|dataReturnare
    //   Exemplu: 1|1|Ion Ionescu|1|Morometii|2024-01-15|
    //   (dataReturnare este gol daca imprumutul e activ)

    public class AdministratorFisierText : IStocareDate
    {
        // Separator intre campuri pe o linie
        private const char SEPARATOR = '|';

        // Cai catre fisierele text
        private readonly string caleCarti;
        private readonly string caleAutori;
        private readonly string calePersoane;
        private readonly string caleImprumuturi;

        // Format data pentru serializare/deserializare
        private const string FORMAT_DATA = "yyyy-MM-dd HH:mm:ss";

        public AdministratorFisierText(string folderDate)
        {
            // Creeaza folderul daca nu exista
            Directory.CreateDirectory(folderDate);

            caleCarti = Path.Combine(folderDate, "carti.txt");
            caleAutori = Path.Combine(folderDate, "autori.txt");
            calePersoane = Path.Combine(folderDate, "persoane.txt");
            caleImprumuturi = Path.Combine(folderDate, "imprumuturi.txt");

            // Creeaza fisierele daca nu exista
            foreach (string cale in new[] { caleCarti, caleAutori, calePersoane, caleImprumuturi })
            {
                if (!File.Exists(cale))
                    File.WriteAllText(cale, string.Empty);
            }
        }

        // ============================================================
        // METODE PRIVATE AJUTATOARE — serializare / deserializare
        // ============================================================

        // Transforma un obiect Autor intr-o linie de text pentru fisier
        private string AutorLaSir(Autor a) =>
            $"{a.Id}{SEPARATOR}{a.Nume}";

        // Construieste un obiect Autor dintr-o linie citita din fisier
        private Autor SirLaAutor(string linie)
        {
            string[] parti = linie.Split(SEPARATOR);
            return new Autor(
                id: int.Parse(parti[0]),
                nume: parti[1]
            );
        }

        // Transforma un obiect Persoana intr-o linie de text
        private string PersoanaLaSir(Persoana p) =>
            $"{p.Id}{SEPARATOR}{p.Nume}";

        // Construieste un obiect Persoana dintr-o linie din fisier
        private Persoana SirLaPersoana(string linie)
        {
            string[] parti = linie.Split(SEPARATOR);
            return new Persoana(
                id: int.Parse(parti[0]),
                nume: parti[1]
            );
        }

        // Transforma un obiect Carte intr-o linie de text
        private string CarteLaSir(Carte c) =>
            $"{c.Id}{SEPARATOR}{c.Titlu}{SEPARATOR}" +
            $"{c.Autor.Id}{SEPARATOR}{c.Autor.Nume}{SEPARATOR}" +
            $"{c.NumarExemplare}{SEPARATOR}{c.ExemplareImprumutate}{SEPARATOR}" +
            $"{c.Gen}{SEPARATOR}{c.Optiuni}";

        // Construieste un obiect Carte dintr-o linie din fisier
        private Carte SirLaCarte(string linie)
        {
            string[] parti = linie.Split(SEPARATOR);
            Autor autor = new Autor(int.Parse(parti[2]), parti[3]);
            Carte carte = new Carte(
                id: int.Parse(parti[0]),
                titlu: parti[1],
                autor: autor,
                numarExemplare: int.Parse(parti[4]),
                gen: Enum.Parse<GenCarte>(parti[6]),
                optiuni: Enum.Parse<OptiuniCarte>(parti[7])
            );
            // Restauram exemplarele imprumutate (nu le calculeaza constructorul)
            carte.ExemplareImprumutate = int.Parse(parti[5]);
            return carte;
        }

        // Transforma un obiect Imprumut intr-o linie de text
        private string ImprumutLaSir(Imprumut i) =>
            $"{i.Id}{SEPARATOR}" +
            $"{i.Persoana.Id}{SEPARATOR}{i.Persoana.Nume}{SEPARATOR}" +
            $"{i.Carte.Id}{SEPARATOR}{i.Carte.Titlu}{SEPARATOR}" +
            $"{i.DataImprumut.ToString(FORMAT_DATA)}{SEPARATOR}" +
            $"{(i.DataReturnare.HasValue ? i.DataReturnare.Value.ToString(FORMAT_DATA) : "")}";

        // Construieste un obiect Imprumut dintr-o linie din fisier
        // Primeste listele de carti si persoane pentru a face legatura
        private Imprumut SirLaImprumut(string linie, List<Carte> carti, List<Persoana> persoane)
        {
            string[] parti = linie.Split(SEPARATOR);

            int idPersoana = int.Parse(parti[1]);
            int idCarte = int.Parse(parti[3]);

            // Cautam persoana si cartea deja incarcate in memorie
            Persoana persoana = persoane.FirstOrDefault(p => p.Id == idPersoana)
                                ?? new Persoana(idPersoana, parti[2]);
            Carte carte = carti.FirstOrDefault(c => c.Id == idCarte)
                          ?? new Carte(idCarte, parti[4], new Autor(0, "necunoscut"), 0);

            Imprumut imprumut = new Imprumut(
                id: int.Parse(parti[0]),
                persoana: persoana,
                carte: carte,
                dataImprumut: DateTime.ParseExact(parti[5], FORMAT_DATA, null)
            );

            // Daca exista data returnare, o setam
            if (!string.IsNullOrWhiteSpace(parti[6]))
                imprumut.DataReturnare = DateTime.ParseExact(parti[6], FORMAT_DATA, null);

            return imprumut;
        }

        // Salveaza o lista de linii intr-un fisier (suprascrie tot)
        private void SalveazaFisier(string cale, IEnumerable<string> linii)
        {
            try
            {
                File.WriteAllLines(cale, linii);
            }
            catch (IOException ex)
            {
                throw new Exception($"Eroare la scrierea fisierului {cale}: {ex.Message}");
            }
        }

        // Citeste toate liniile dintr-un fisier (ignora liniile goale)
        private List<string> CitesteFisier(string cale)
        {
            try
            {
                if (!File.Exists(cale))
                    return new List<string>();

                return File.ReadAllLines(cale)
                           .Where(l => !string.IsNullOrWhiteSpace(l))
                           .ToList();
            }
            catch (IOException ex)
            {
                throw new Exception($"Eroare la citirea fisierului {cale}: {ex.Message}");
            }
        }

        // ============================================================
        // CARTI
        // ============================================================

        public void AdaugaCarte(Carte carte)
        {
            // Citim ce avem, adaugam linia noua, scriem tot
            List<string> linii = CitesteFisier(caleCarti);
            linii.Add(CarteLaSir(carte));
            SalveazaFisier(caleCarti, linii);
        }

        public List<Carte> GetCarti()
        {
            return CitesteFisier(caleCarti)
                   .Select(linie => SirLaCarte(linie))
                   .ToList();
        }

        public Carte? GetCarteById(int id) =>
            GetCarti().FirstOrDefault(c => c.Id == id);

        public List<Carte> CautaDupaTitlu(string titlu) =>
            GetCarti()
            .Where(c => c.Titlu.ToLower().Contains(titlu.ToLower()))
            .ToList();

        public List<Carte> CautaDupaAutor(string numeAutor) =>
            GetCarti()
            .Where(c => c.Autor.Nume.ToLower().Contains(numeAutor.ToLower()))
            .ToList();

        public void ModificaCarte(Carte carteModificata)
        {
            // Citim toate, inlocuim linia cu id-ul corespunzator, scriem tot
            List<Carte> carti = GetCarti();
            int index = carti.FindIndex(c => c.Id == carteModificata.Id);
            if (index >= 0)
            {
                carti[index] = carteModificata;
                SalveazaFisier(caleCarti, carti.Select(c => CarteLaSir(c)));
            }
        }

        public void StergeCarte(int id)
        {
            List<Carte> carti = GetCarti();
            carti.RemoveAll(c => c.Id == id);
            SalveazaFisier(caleCarti, carti.Select(c => CarteLaSir(c)));
        }

        // ============================================================
        // AUTORI
        // ============================================================

        public void AdaugaAutor(Autor autor)
        {
            List<string> linii = CitesteFisier(caleAutori);
            linii.Add(AutorLaSir(autor));
            SalveazaFisier(caleAutori, linii);
        }

        public List<Autor> GetAutori() =>
            CitesteFisier(caleAutori)
            .Select(linie => SirLaAutor(linie))
            .ToList();

        public Autor? GetAutorById(int id) =>
            GetAutori().FirstOrDefault(a => a.Id == id);

        public void ModificaAutor(Autor autorModificat)
        {
            List<Autor> autori = GetAutori();
            int index = autori.FindIndex(a => a.Id == autorModificat.Id);
            if (index >= 0)
            {
                autori[index] = autorModificat;
                SalveazaFisier(caleAutori, autori.Select(a => AutorLaSir(a)));
            }
        }

        public void StergeAutor(int id)
        {
            List<Autor> autori = GetAutori();
            autori.RemoveAll(a => a.Id == id);
            SalveazaFisier(caleAutori, autori.Select(a => AutorLaSir(a)));
        }

        // ============================================================
        // PERSOANE
        // ============================================================

        public void AdaugaPersoana(Persoana persoana)
        {
            List<string> linii = CitesteFisier(calePersoane);
            linii.Add(PersoanaLaSir(persoana));
            SalveazaFisier(calePersoane, linii);
        }

        public List<Persoana> GetPersoane() =>
            CitesteFisier(calePersoane)
            .Select(linie => SirLaPersoana(linie))
            .ToList();

        public Persoana? GetPersoanaById(int id) =>
            GetPersoane().FirstOrDefault(p => p.Id == id);

        public void ModificaPersoana(Persoana persoanaModificata)
        {
            List<Persoana> persoane = GetPersoane();
            int index = persoane.FindIndex(p => p.Id == persoanaModificata.Id);
            if (index >= 0)
            {
                persoane[index] = persoanaModificata;
                SalveazaFisier(calePersoane, persoane.Select(p => PersoanaLaSir(p)));
            }
        }

        public void StergePersoana(int id)
        {
            List<Persoana> persoane = GetPersoane();
            persoane.RemoveAll(p => p.Id == id);
            SalveazaFisier(calePersoane, persoane.Select(p => PersoanaLaSir(p)));
        }

        // ============================================================
        // IMPRUMUTURI
        // ============================================================

        public void AdaugaImprumut(Imprumut imprumut)
        {
            // Actualizam ExemplareImprumutate in fisierul de carti
            imprumut.Carte.ExemplareImprumutate++;
            ModificaCarte(imprumut.Carte);

            // Adaugam imprumutul
            List<string> linii = CitesteFisier(caleImprumuturi);
            linii.Add(ImprumutLaSir(imprumut));
            SalveazaFisier(caleImprumuturi, linii);
        }

        public List<Imprumut> GetImprumuturi()
        {
            List<Carte> carti = GetCarti();
            List<Persoana> persoane = GetPersoane();
            return CitesteFisier(caleImprumuturi)
                   .Select(linie => SirLaImprumut(linie, carti, persoane))
                   .ToList();
        }

        public List<Imprumut> GetImprumuturiActive() =>
            GetImprumuturi().Where(i => i.DataReturnare == null).ToList();

        public void ReturneazaCarte(int idImprumut)
        {
            List<Carte> carti = GetCarti();
            List<Persoana> persoane = GetPersoane();
            List<string> linii = CitesteFisier(caleImprumuturi);
            List<string> liniiNoi = new List<string>();

            foreach (string linie in linii)
            {
                Imprumut imprumut = SirLaImprumut(linie, carti, persoane);
                if (imprumut.Id == idImprumut && imprumut.DataReturnare == null)
                {
                    imprumut.DataReturnare = DateTime.Now;
                    // Actualizam ExemplareImprumutate in fisierul de carti
                    imprumut.Carte.ExemplareImprumutate--;
                    ModificaCarte(imprumut.Carte);
                }
                liniiNoi.Add(ImprumutLaSir(imprumut));
            }

            SalveazaFisier(caleImprumuturi, liniiNoi);
        }
    }
}