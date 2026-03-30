using LibrarieModele.Enums;

namespace LibrarieModele.Models
{
    public class Carte
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public Autor Autor { get; set; }
        public int NumarExemplare { get; set; }
        public int ExemplareImprumutate { get; set; }

        public GenCarte Gen { get; set; }
        public OptiuniCarte Optiuni { get; set; }

        public Carte(int id, string titlu, Autor autor, int numarExemplare,
            GenCarte gen = GenCarte.Roman,
            OptiuniCarte optiuni = OptiuniCarte.Niciuna)
        {
            Id = id;
            Titlu = titlu;
            Autor = autor;
            NumarExemplare = numarExemplare;
            ExemplareImprumutate = 0;
            Gen = gen;
            Optiuni = optiuni;
        }

        public bool EsteDisponibila()
        {
            return ExemplareImprumutate < NumarExemplare;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Titlu: {Titlu}, Autor: {Autor.Nume}, Exemplare: {NumarExemplare}, Imprumutate: {ExemplareImprumutate}, Gen: {Gen}, Optiuni: {Optiuni}";
        }
    }
}