using System;

namespace LibraryApp.Models
{
    public class Imprumut
    {
        public int Id { get; set; }
        public Persoana Persoana { get; set; }
        public Carte Carte { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime? DataReturnare { get; set; }

        public Imprumut(int id, Persoana persoana, Carte carte, DateTime dataImprumut)
        {
            Id = id;
            Persoana = persoana;
            Carte = carte;
            DataImprumut = dataImprumut;
            DataReturnare = null;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Persoana: {Persoana.Nume}, Carte: {Carte.Titlu}, Data imprumut: {DataImprumut}, Data returnare: {DataReturnare}";
        }
    }
}