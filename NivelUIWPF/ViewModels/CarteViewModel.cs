using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelUIWPF.ViewModels
{
    public class CarteViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        // =============================================
        // LAB10: ObservableCollection + INotifyPropertyChanged
        // =============================================
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

        // =============================================
        // LAB11: Proprietati pentru formularul de adaugare
        // Binding TwoWay la campurile din XAML
        // =============================================
        private string titluNou;
        public string TitluNou
        {
            get => titluNou;
            set { titluNou = value; OnPropertyChanged(); OnPropertyChanged(nameof(EsteValid)); }
        }

        private string autorNou;
        public string AutorNou
        {
            get => autorNou;
            set { autorNou = value; OnPropertyChanged(); OnPropertyChanged(nameof(EsteValid)); }
        }

        private string nrExemplareNou;
        public string NrExemplareNou
        {
            get => nrExemplareNou;
            set { nrExemplareNou = value; OnPropertyChanged(); OnPropertyChanged(nameof(EsteValid)); }
        }

        // =============================================
        // LAB11: IDataErrorInfo — validare proprietati
        // =============================================
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(TitluNou):
                        if (string.IsNullOrWhiteSpace(TitluNou))
                            return "Titlul este obligatoriu!";
                        if (TitluNou.Length < 2)
                            return "Titlul trebuie sa aiba minim 2 caractere!";
                        if (TitluNou.Length > 100)
                            return "Titlul nu poate depasi 100 de caractere!";
                        break;

                    case nameof(AutorNou):
                        if (string.IsNullOrWhiteSpace(AutorNou))
                            return "Autorul este obligatoriu!";
                        if (AutorNou.Length < 3)
                            return "Numele autorului trebuie sa aiba minim 3 caractere!";
                        break;

                    case nameof(NrExemplareNou):
                        if (string.IsNullOrWhiteSpace(NrExemplareNou))
                            return "Numarul de exemplare este obligatoriu!";
                        if (!int.TryParse(NrExemplareNou, out int nr) || nr <= 0)
                            return "Introduceti un numar pozitiv!";
                        if (nr > 50)
                            return "Numarul de exemplare nu poate depasi 50!";
                        break;
                }
                return null;
            }
        }

        // LAB11: EsteValid — butonul Adauga se activeaza doar cand toate campurile sunt valide
        public bool EsteValid =>
            string.IsNullOrEmpty(this[nameof(TitluNou)]) &&
            string.IsNullOrEmpty(this[nameof(AutorNou)]) &&
            string.IsNullOrEmpty(this[nameof(NrExemplareNou)]);

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

            // Initializam campurile goale
            TitluNou = "";
            AutorNou = "";
            NrExemplareNou = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}