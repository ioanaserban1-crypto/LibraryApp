using LibrarieModele.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NivelStocareDate.Administrare
{
    public class AdministratorEntitateMemorie
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

        public void AdaugaCarte(Carte carte)
        {
            carti.Add(carte);
        }

        public List<Carte> GetCarti()
        {
            return carti;
        }

        public Carte? GetCarteById(int id)
        {
            // LINQ
            return carti.FirstOrDefault(c => c.Id == id);
        }

        public List<Carte> CautaDupaTitlu(string titlu)
        {
            // LINQ
            return carti
                .Where(c => c.Titlu.ToLower().Contains(titlu.ToLower()))
                .ToList();
        }

        public List<Carte> CautaDupaAutor(string numeAutor)
        {
            // LINQ
            return carti
                .Where(c => c.Autor.Nume.ToLower().Contains(numeAutor.ToLower()))
                .ToList();
        }

        // =========================
        // AUTORI
        // =========================

        public void AdaugaAutor(Autor autor)
        {
            autori.Add(autor);
        }

        public List<Autor> GetAutori()
        {
            return autori;
        }

        public Autor? GetAutorById(int id)
        {
            // LINQ
            return autori.FirstOrDefault(a => a.Id == id);
        }

        // =========================
        // PERSOANE
        // =========================

        public void AdaugaPersoana(Persoana persoana)
        {
            persoane.Add(persoana);
        }

        public List<Persoana> GetPersoane()
        {
            return persoane;
        }

        public Persoana? GetPersoanaById(int id)
        {
            // LINQ
            return persoane.FirstOrDefault(p => p.Id == id);
        }

        // =========================
        // IMPRUMUTURI
        // =========================

        public void AdaugaImprumut(Imprumut imprumut)
        {
            imprumuturi.Add(imprumut);

            // crește numărul de exemplare împrumutate
            imprumut.Carte.ExemplareImprumutate++;
        }

        public List<Imprumut> GetImprumuturi()
        {
            return imprumuturi;
        }

        public List<Imprumut> GetImprumuturiActive()
        {
            // LINQ
            return imprumuturi
                .Where(i => i.DataReturnare == null)
                .ToList();
        }

        public void ReturneazaCarte(int idImprumut)
        {
            var imprumut = imprumuturi.FirstOrDefault(i => i.Id == idImprumut);

            if (imprumut != null && imprumut.DataReturnare == null)
            {
                imprumut.DataReturnare = DateTime.Now;
                imprumut.Carte.ExemplareImprumutate--;
            }
        }
    }
}