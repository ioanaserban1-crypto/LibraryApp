namespace LibraryApp.Models
{
    public class Carte
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public Autor Autor { get; set; }
        public int NumarExemplare { get; set; }
        public int ExemplareImprumutate { get; set; }

        public Carte(int id, string titlu, Autor autor, int numarExemplare)
        {
            Id = id;
            Titlu = titlu;
            Autor = autor;
            NumarExemplare = numarExemplare;
            ExemplareImprumutate = 0;
        }

        public bool EsteDisponibila()
        {
            return ExemplareImprumutate < NumarExemplare;
        }
    }
}