using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using NivelUIWPF.ViewModels;
using LibrarieModele.Enums;
using LibrarieModele.Models;
using LibraryApp;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        // codul tau original
        private List<Carte> listaCarte = new List<Carte>();

        // LAB10: ObservableCollection pentru autori — UI se actualizeaza automat
        private ObservableCollection<Autor> listaAutori = new ObservableCollection<Autor>();

        // LAB10: contor ID autori
        private int nextIdAutor = 4;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarteViewModel();

            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

            listaCarte.Add(new Carte(1, "Moromeții", autor1, 10, GenCarte.Roman));
            listaCarte.Add(new Carte(2, "Luceafărul", autor2, 5, GenCarte.Poezie));
            listaCarte.Add(new Carte(3, "Amintiri din copilărie", autor3, 8, GenCarte.Roman));

            // LAB10: initializam si lista de autori
            listaAutori.Add(autor1);
            listaAutori.Add(autor2);
            listaAutori.Add(autor3);

            SeteazaListBoxGen(lstGenCarte);
            SeteazaListBoxGen(lstGenCarteModifica);
            SeteazaComboBoxCarti();

            // LAB10: legam lstCarti si lstAutori la colectii
            lstCarti.ItemsSource = listaCarte;
            lstAutori.ItemsSource = listaAutori;
        }

        // codul tau original
        private void SeteazaListBoxGen(System.Windows.Controls.ListBox listBox)
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = Enum.GetValues(typeof(GenCarte));
        }

        private void SeteazaComboBoxCarti()
        {
            cmbCarti.ItemsSource = null;
            cmbCarti.ItemsSource = listaCarte;
        }

        // =============================================
        // Meniu
        // =============================================
        private void MenuAdauga_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Visible;
            panelModifica.Visibility = Visibility.Collapsed;
            radioAdauga.IsChecked = true;
        }

        private void MenuModifica_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Collapsed;
            panelModifica.Visibility = Visibility.Visible;
            radioEditeaza.IsChecked = true;
        }

        // LAB10: meniu autori
        private void MenuAutori_Click(object sender, RoutedEventArgs e)
        {
            panelAutori.Visibility = Visibility.Visible;
        }

        // =============================================
        // RadioButton — codul tau original
        // =============================================
        private void RadioAdauga_Checked(object sender, RoutedEventArgs e)
        {
            if (panelAdauga != null)
            {
                panelAdauga.Visibility = Visibility.Visible;
                panelModifica.Visibility = Visibility.Collapsed;
            }
        }

        private void RadioEditeaza_Checked(object sender, RoutedEventArgs e)
        {
            if (panelModifica != null)
            {
                panelAdauga.Visibility = Visibility.Collapsed;
                panelModifica.Visibility = Visibility.Visible;
            }
        }

        // =============================================
        // LAB10: SelectionChanged ListBox Carti
        // =============================================
        private void lstCarti_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // nu e nevoie de actiune suplimentara
        }

        // =============================================
        // LAB10: CRUD AUTOR
        // =============================================

        // CREATE
        private void AdaugaAutor_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumeAutor.Text.Trim();
            if (string.IsNullOrWhiteSpace(nume))
            {
                MessageBox.Show("Introduceți un nume valid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // ObservableCollection notifica lstAutori automat
            listaAutori.Add(new Autor(nextIdAutor++, nume));
            txtMesajAutor.Text = $"✔ Autorul \"{nume}\" a fost adăugat!";
            txtNumeAutor.Clear();
        }

        // UPDATE
        private void ModificaAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autorSelectat = lstAutori.SelectedItem as Autor;
            string numeNou = txtNumeAutor.Text.Trim();

            if (autorSelectat == null || string.IsNullOrWhiteSpace(numeNou))
            {
                MessageBox.Show("Selectați un autor și introduceți un nume valid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int index = listaAutori.IndexOf(autorSelectat);
            autorSelectat.Nume = numeNou;
            // Fortam refresh ObservableCollection
            listaAutori.RemoveAt(index);
            listaAutori.Insert(index, autorSelectat);

            txtMesajAutor.Text = $"✔ Autorul a fost modificat în \"{numeNou}\"!";
        }

        // DELETE
        private void StergeAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autorSelectat = lstAutori.SelectedItem as Autor;
            if (autorSelectat == null)
            {
                MessageBox.Show("Selectați un autor din listă!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var rezultat = MessageBox.Show(
                $"Sigur vreți să ștergeți autorul \"{autorSelectat.Nume}\"?",
                "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultat == MessageBoxResult.Yes)
            {
                string nume = autorSelectat.Nume;
                listaAutori.Remove(autorSelectat); // lstAutori se actualizeaza automat
                txtMesajAutor.Text = $"✔ Autorul \"{nume}\" a fost șters!";
                txtNumeAutor.Clear();
            }
        }

        // SelectionChanged lstAutori — precompletam campul
        private void lstAutori_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAutori.SelectedItem is Autor autorSelectat)
            {
                txtNumeAutor.Text = autorSelectat.Nume;
                txtMesajAutor.Text = "";
            }
        }

        // =============================================
        // codul tau original — neschimbat
        // =============================================
        private void lstGenCarte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void lstGenCarteModifica_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }

        private void cmbCarti_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Carte carteSelectata = cmbCarti.SelectedItem as Carte;
            if (carteSelectata != null)
            {
                txtTitluModifica.Text = carteSelectata.Titlu;
                txtAutorModifica.Text = carteSelectata.Autor.Nume;
                txtNrExemplareModifica.Text = carteSelectata.NumarExemplare.ToString();
                dtpDataActualizare.SelectedDate = DateTime.Today;
                lstGenCarteModifica.SelectedItem = carteSelectata.Gen;
                txtMesajModifica.Text = "";
            }
        }

        private void ActualizeazaCarte_Click(object sender, RoutedEventArgs e)
        {
            Carte carteSelectata = cmbCarti.SelectedItem as Carte;

            if (carteSelectata == null)
            {
                MessageBox.Show("Selectați o carte din listă!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtTitluModifica.Text) ||
                string.IsNullOrEmpty(txtAutorModifica.Text) ||
                string.IsNullOrEmpty(txtNrExemplareModifica.Text))
            {
                MessageBox.Show("Completați toate câmpurile!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNrExemplareModifica.Text, out int nrExemplare) || nrExemplare <= 0)
            {
                MessageBox.Show("Număr exemplare invalid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            carteSelectata.Titlu = txtTitluModifica.Text;
            carteSelectata.Autor.Nume = txtAutorModifica.Text;
            carteSelectata.NumarExemplare = nrExemplare;

            if (lstGenCarteModifica.SelectedItem != null)
                carteSelectata.Gen = (GenCarte)lstGenCarteModifica.SelectedItem;

            dtpDataActualizare.SelectedDate = DateTime.Today;
            SeteazaComboBoxCarti();
            txtMesajModifica.Text = $"✔ Cartea \"{carteSelectata.Titlu}\" a fost actualizată cu succes!";
        }

        private void AdaugaCarte_Click(object sender, RoutedEventArgs e)
        {
            string titlu = txtTitlu.Text;
            string autor = txtAutor.Text;
            int nrExemplare;
            bool valid = true;

            if (string.IsNullOrEmpty(titlu))
            {
                txtTitlu.Background = Brushes.LightCoral;
                valid = false;
            }
            else { txtTitlu.Background = Brushes.White; }

            if (string.IsNullOrEmpty(autor))
            {
                txtAutor.Background = Brushes.LightCoral;
                valid = false;
            }
            else { txtAutor.Background = Brushes.White; }

            if (!int.TryParse(txtNrExemplare.Text, out nrExemplare))
            {
                txtNrExemplare.Background = Brushes.LightCoral;
                MessageBox.Show("Număr exemplare invalid!");
                return;
            }
            else { txtNrExemplare.Background = Brushes.White; }

            if (nrExemplare <= 0)
            {
                MessageBox.Show("Numărul de exemplare trebuie să fie pozitiv!");
                return;
            }

            if (!valid)
            {
                MessageBox.Show("Completați corect câmpurile!");
                return;
            }

            GenCarte genSelectat = GenCarte.Roman;
            if (lstGenCarte.SelectedItem != null)
                genSelectat = (GenCarte)lstGenCarte.SelectedItem;

            DateTime dataAchizitie = dtpDataAchizitie.SelectedDate ?? DateTime.Today;

            Autor autorNou = new Autor(listaCarte.Count + 1, autor);
            Carte carteNoua = new Carte(listaCarte.Count + 1, titlu, autorNou, nrExemplare, genSelectat);
            listaCarte.Add(carteNoua);
            SeteazaComboBoxCarti();

            MessageBox.Show($"Carte adăugată: {titlu} - {autor} ({nrExemplare} exemplare)\nGen: {genSelectat}\nData achiziției: {dataAchizitie:dd.MM.yyyy}");
        }

        private void Cauta_Click(object sender, RoutedEventArgs e)
        {
            string cautare = txtCautare.Text;
            string termen = txtCautare.Text.ToLower();

            if (cautare == "Moromeții")
                MessageBox.Show("Carte găsită!");
            else
                MessageBox.Show("Nu există!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}