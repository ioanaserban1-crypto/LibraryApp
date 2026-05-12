using LibrarieModele.Models;

namespace NivelStocareDate.Administrare
{
    // Implementare care stocheaza datele IN MEMORIE (List<T>).
    // Datele se pierd cand aplicatia se inchide.
    // Implementeaza interfata IStocareDate — acelasi contract
    // ca si clasa care scrie in fisier text.
    public class AdministratorEntitateMemorie : IStocareDate
    {
        private List<Carte> carti;
        private List<Autor> autori;
        private List<Persoana> persoane;
        private List<Imprumut> imprumuturi;

        public AdministratorEntitateMemorie()
        {
            carti = new List<Carte>();
            autori = new List<Autor>();
            persoane = new List<Persoana>();
            imprumuturi = new List<Imprumut>();
        }

        // =========================
        // CARTI
        // =========================

        public void AdaugaCarte(Carte carte) => carti.Add(carte);

        public List<Carte> GetCarti() => carti;

        public Carte? GetCarteById(int id) =>
            carti.FirstOrDefault(c => c.Id == id);

        public List<Carte> CautaDupaTitlu(string titlu) =>
            carti.Where(c => c.Titlu.ToLower().Contains(titlu.ToLower())).ToList();

        public List<Carte> CautaDupaAutor(string numeAutor) =>
            carti.Where(c => c.Autor.Nume.ToLower().Contains(numeAutor.ToLower())).ToList();

        public void ModificaCarte(Carte carteModificata)
        {
            int index = carti.FindIndex(c => c.Id == carteModificata.Id);
            if (index >= 0)
                carti[index] = carteModificata;
        }

        public void StergeCarte(int id)
        {
            Carte? carte = carti.FirstOrDefault(c => c.Id == id);
            if (carte != null)
                carti.Remove(carte);
        }

        // =========================
        // AUTORI
        // =========================

        public void AdaugaAutor(Autor autor) => autori.Add(autor);

        public List<Autor> GetAutori() => autori;

        public Autor? GetAutorById(int id) =>
            autori.FirstOrDefault(a => a.Id == id);

        public void ModificaAutor(Autor autorModificat)
        {
            int index = autori.FindIndex(a => a.Id == autorModificat.Id);
            if (index >= 0)
                autori[index] = autorModificat;
        }

        public void StergeAutor(int id)
        {
            Autor? autor = autori.FirstOrDefault(a => a.Id == id);
            if (autor != null)
                autori.Remove(autor);
        }

        // =========================
        // PERSOANE
        // =========================

        public void AdaugaPersoana(Persoana persoana) => persoane.Add(persoana);

        public List<Persoana> GetPersoane() => persoane;

        public Persoana? GetPersoanaById(int id) =>
            persoane.FirstOrDefault(p => p.Id == id);

        public void ModificaPersoana(Persoana persoanaModificata)
        {
            int index = persoane.FindIndex(p => p.Id == persoanaModificata.Id);
            if (index >= 0)
                persoane[index] = persoanaModificata;
        }

        public void StergePersoana(int id)
        {
            Persoana? persoana = persoane.FirstOrDefault(p => p.Id == id);
            if (persoana != null)
                persoane.Remove(persoana);
        }

        // =========================
        // IMPRUMUTURI
        // =========================

        public void AdaugaImprumut(Imprumut imprumut)
        {
            imprumuturi.Add(imprumut);
            imprumut.Carte.ExemplareImprumutate++;
        }

        public List<Imprumut> GetImprumuturi() => imprumuturi;

        public List<Imprumut> GetImprumuturiActive() =>
            imprumuturi.Where(i => i.DataReturnare == null).ToList();

        public void ReturneazaCarte(int idImprumut)
        {
            Imprumut? imprumut = imprumuturi.FirstOrDefault(i => i.Id == idImprumut);
            if (imprumut != null && imprumut.DataReturnare == null)
            {
                imprumut.DataReturnare = DateTime.Now;
                imprumut.Carte.ExemplareImprumutate--;
            }
        }
    }
}