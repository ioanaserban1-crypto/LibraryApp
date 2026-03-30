namespace LibrarieModele.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nume { get; set; }

        public Autor(int id, string nume)
        {
            Id = id;
            Nume = nume;
        }
    }
}
