<<<<<<< HEAD
﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
=======
﻿using LibrarieModele;
>>>>>>> salvare-commits
using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelUIWPF.ViewModels
{
<<<<<<< HEAD
    // LAB10: Implementam INotifyPropertyChanged pentru notificarea UI-ului
    public class CarteViewModel : INotifyPropertyChanged
    {
        // LAB10: ObservableCollection in loc de List<T>
        // — UI-ul se actualizeaza automat la Add/Remove
        private ObservableCollection<Carte> carti;
        public ObservableCollection<Carte> Carti
        {
            get => carti;
            set
            {
                carti = value;
                OnPropertyChanged();
            }
        }

        // LAB10: Cartea selectata curenta — binding TwoWay
        private Carte carteCurenta;
        public Carte CarteCurenta
        {
            get => carteCurenta;
            set
            {
                carteCurenta = value;
                OnPropertyChanged();
            }
        }

        public CarteViewModel()
        {
            // Date initiale
            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

            Carti = new ObservableCollection<Carte>
            {
                new Carte(1, "Moromeții", autor1, 10, GenCarte.Roman, OptiuniCarte.Niciuna),
                new Carte(2, "Luceafărul", autor2, 5, GenCarte.Poezie, OptiuniCarte.Niciuna),
                new Carte(3, "Amintiri din copilărie", autor3, 8, GenCarte.Roman, OptiuniCarte.Niciuna)
            };

            CarteCurenta = Carti[0];
        }

        // LAB10: INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
=======
    public class CarteViewModel
    {
        public Carte CarteCurenta { get; set; }

        public CarteViewModel()
        {
            Autor autor = new Autor(1, "Marin Preda");

            CarteCurenta = new Carte(
                1,                  // id
                "Moromeții",        // titlu
                autor,              // obiect Autor
                10,                 // nr exemplare
                GenCarte.Roman,     // enum
                OptiuniCarte.Niciuna
            );
>>>>>>> salvare-commits
        }
    }
}