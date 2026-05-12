using LibrarieModele.Models;

namespace NivelStocareDate.Administrare
{
    public interface IStocareDate
    {
        // ===== CARTI =====
        void AdaugaCarte(Carte carte);
        List<Carte> GetCarti();
        Carte? GetCarteById(int id);
        List<Carte> CautaDupaTitlu(string titlu);
        List<Carte> CautaDupaAutor(string numeAutor);
        void ModificaCarte(Carte carteModificata);
        void StergeCarte(int id);

        // ===== AUTORI =====
        void AdaugaAutor(Autor autor);
        List<Autor> GetAutori();
        Autor? GetAutorById(int id);
        void ModificaAutor(Autor autorModificat);
        void StergeAutor(int id);

        // ===== PERSOANE =====
        void AdaugaPersoana(Persoana persoana);
        List<Persoana> GetPersoane();
        Persoana? GetPersoanaById(int id);
        void ModificaPersoana(Persoana persoanaModificata);
        void StergePersoana(int id);

        // ===== IMPRUMUTURI =====
        void AdaugaImprumut(Imprumut imprumut);
        List<Imprumut> GetImprumuturi();
        List<Imprumut> GetImprumuturiActive();
        void ReturneazaCarte(int idImprumut);
    }
}