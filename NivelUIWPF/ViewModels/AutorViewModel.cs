using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibrarieModele.Models;

namespace NivelUIWPF.ViewModels
{
    // LAB10: A doua entitate — CRUD complet pentru Autor
    public class AutorViewModel : INotifyPropertyChanged
    {
        // LAB10: ObservableCollection — UI-ul se actualizeaza automat
        private ObservableCollection<Autor> autori;
        public ObservableCollection<Autor> Autori
        {
            get => autori;
            set
            {
                autori = value;
                OnPropertyChanged();
            }
        }

        // LAB10: Autorul selectat curent — binding TwoWay
        private Autor autorCurent;
        public Autor AutorCurent
        {
            get => autorCurent;
            set
            {
                autorCurent = value;
                OnPropertyChanged();
            }
        }

        // Contor pentru ID-uri noi
        private int nextId = 4;

        public AutorViewModel()
        {
            Autori = new ObservableCollection<Autor>
            {
                new Autor(1, "Marin Preda"),
                new Autor(2, "Mihai Eminescu"),
                new Autor(3, "Ion Creanga")
            };

            AutorCurent = null;
        }

        // =============================================
        // CREATE — Adauga autor nou
        // =============================================
        public bool AdaugaAutor(string nume)
        {
            if (string.IsNullOrWhiteSpace(nume))
                return false;

            Autor autorNou = new Autor(nextId++, nume);
            Autori.Add(autorNou); // ObservableCollection notifica UI automat
            AutorCurent = autorNou;
            return true;
        }

        // =============================================
        // UPDATE — Modifica autorul curent
        // =============================================
        public bool ModificaAutor(Autor autor, string numeNou)
        {
            if (autor == null || string.IsNullOrWhiteSpace(numeNou))
                return false;

            autor.Nume = numeNou;

            // Fortam notificarea UI-ului (Autor nu implementeaza INotifyPropertyChanged)
            int index = Autori.IndexOf(autor);
            if (index >= 0)
            {
                Autori.RemoveAt(index);
                Autori.Insert(index, autor);
            }

            AutorCurent = autor;
            return true;
        }

        // =============================================
        // DELETE — Sterge autorul curent
        // =============================================
        public bool StergeAutor(Autor autor)
        {
            if (autor == null)
                return false;

            Autori.Remove(autor); // ObservableCollection notifica UI automat
            AutorCurent = null;
            return true;
        }

        // =============================================
        // LAB10: INotifyPropertyChanged
        // =============================================
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}