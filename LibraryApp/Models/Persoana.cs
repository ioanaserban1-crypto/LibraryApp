namespace LibraryApp.Models
{
    public class Persoana
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public int NumarCartiImprumutate { get; set; }

        public Persoana(int id, string nume)
        {
            Id = id;
            Nume = nume;
            NumarCartiImprumutate = 0;
        }
    }
}