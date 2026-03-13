using System;

namespace LibraryApp.Models
{
    public class Imprumut
    {
        public Carte Carte { get; set; }
        public Persoana Persoana { get; set; }
        public DateTime DataImprumut { get; set; }

        public Imprumut(Carte carte, Persoana persoana)
        {
            Carte = carte;
            Persoana = persoana;
            DataImprumut = DateTime.Now;
        }
    }
}