using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelUIWPF.ViewModels
{
    public class CarteViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Carte> carti;
        public ObservableCollection<Carte> Carti
        {
            get => carti;
            set { carti = value; OnPropertyChanged(); }
        }

        private Carte carteCurenta;
        public Carte CarteCurenta
        {
            get => carteCurenta;
            set { carteCurenta = value; OnPropertyChanged(); }
        }

        public CarteViewModel()
        {
            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

            Carti = new ObservableCollection<Carte>
            {
                new Carte(1, "Morometii", autor1, 10, GenCarte.Roman, OptiuniCarte.Niciuna),
                new Carte(2, "Luceafarul", autor2, 5, GenCarte.Poezie, OptiuniCarte.Niciuna),
                new Carte(3, "Amintiri din copilarie", autor3, 8, GenCarte.Roman, OptiuniCarte.Niciuna)
            };

            CarteCurenta = Carti[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}