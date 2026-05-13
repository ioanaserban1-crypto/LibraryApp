using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using NivelUIWPF.ViewModels;
using LibrarieModele.Enums;
using LibrarieModele.Models;
using LibraryApp; // pentru Constante

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        // LAB9: Lista de carti folosita de ComboBox si ListBox
        private List<Carte> listaCarte = new List<Carte>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarteViewModel();

            // Initializam lista de carti cu cateva exemple
            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

            listaCarte.Add(new Carte(1, "Moromeții", autor1, 10, GenCarte.Roman));
            listaCarte.Add(new Carte(2, "Luceafărul", autor2, 5, GenCarte.Poezie));
            listaCarte.Add(new Carte(3, "Amintiri din copilărie", autor3, 8, GenCarte.Roman));

            // LAB9: Populam ListBox-ul cu valorile enum-ului GenCarte
            SeteazaListBoxGen(lstGenCarte);
            SeteazaListBoxGen(lstGenCarteModifica);

            // LAB9: Populam ComboBox-ul cu lista de carti
            SeteazaComboBoxCarti();
        }

        // =============================================
        // LAB9: Metode de initializare controale
        // =============================================

        // Populeaza un ListBox cu toate valorile din enum-ul GenCarte
        private void SeteazaListBoxGen(System.Windows.Controls.ListBox listBox)
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = Enum.GetValues(typeof(GenCarte));
        }

        // Populeaza ComboBox-ul cu lista de carti
        private void SeteazaComboBoxCarti()
        {
            cmbCarti.ItemsSource = null;
            cmbCarti.ItemsSource = listaCarte;
        }

        // =============================================
        // Meniu: Adauga / Modifica
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

        // =============================================
        // CERINTA 2: Handleri RadioButton
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
        // LAB9: SelectionChanged pentru ListBox Gen (Adaugare)
        // =============================================

        private void lstGenCarte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Genul selectat va fi preluat la adaugare
            // (nu e nevoie de actiune vizuala suplimentara)
        }

        // =============================================
        // LAB9: SelectionChanged pentru ComboBox Carti (Modificare)
        // Precompleteaza campurile cu datele cartii selectate
        // =============================================

        private void cmbCarti_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Carte carteSelectata = cmbCarti.SelectedItem as Carte;
            if (carteSelectata != null)
            {
                // Precompletam campurile cu datele cartii selectate
                txtTitluModifica.Text = carteSelectata.Titlu;
                txtAutorModifica.Text = carteSelectata.Autor.Nume;
                txtNrExemplareModifica.Text = carteSelectata.NumarExemplare.ToString();

                // LAB9: Setam data actualizarii la data curenta (read-only)
                dtpDataActualizare.SelectedDate = DateTime.Today;

                // LAB9: Selectam genul curent in ListBox
                lstGenCarteModifica.SelectedItem = carteSelectata.Gen;

                txtMesajModifica.Text = "";
            }
        }

        // =============================================
        // LAB9: SelectionChanged pentru ListBox Gen (Modificare)
        // =============================================

        private void lstGenCarteModifica_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Genul selectat va fi preluat la actualizare
        }

        // =============================================
        // LAB9: Buton Actualizează — Modificare carte
        // =============================================

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

            // Aplicam modificarile pe obiectul din lista
            carteSelectata.Titlu = txtTitluModifica.Text;
            carteSelectata.Autor.Nume = txtAutorModifica.Text;
            carteSelectata.NumarExemplare = nrExemplare;

            // LAB9: Preluam genul selectat din ListBox
            if (lstGenCarteModifica.SelectedItem != null)
            {
                carteSelectata.Gen = (GenCarte)lstGenCarteModifica.SelectedItem;
            }

            // LAB9: Actualizam data modificarii
            dtpDataActualizare.SelectedDate = DateTime.Today;

            // Reimprospatam ComboBox-ul
            SeteazaComboBoxCarti();

            txtMesajModifica.Text = $"✔ Cartea \"{carteSelectata.Titlu}\" a fost actualizată cu succes!";
        }

        // =============================================
        // CODUL ORIGINAL — Adaugare carte
        // =============================================

        private void AdaugaCarte_Click(object sender, RoutedEventArgs e)
        {
            string titlu = txtTitlu.Text;
            string autor = txtAutor.Text;
            int nrExemplare;
            bool valid = true;

            // VALIDARE TITLU
            if (string.IsNullOrEmpty(titlu))
            {
                txtTitlu.Background = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                txtTitlu.Background = Brushes.White;
            }

            // VALIDARE AUTOR
            if (string.IsNullOrEmpty(autor))
            {
                txtAutor.Background = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                txtAutor.Background = Brushes.White;
            }

            // VALIDARE NR EXEMPLARE
            if (!int.TryParse(txtNrExemplare.Text, out nrExemplare))
            {
                txtNrExemplare.Background = Brushes.LightCoral;
                MessageBox.Show("Număr exemplare invalid!");
                return;
            }
            else
            {
                txtNrExemplare.Background = Brushes.White;
            }

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

            // LAB9: Preluam genul selectat din ListBox
            GenCarte genSelectat = GenCarte.Roman;
            if (lstGenCarte.SelectedItem != null)
                genSelectat = (GenCarte)lstGenCarte.SelectedItem;

            // LAB9: Preluam data achizitiei din DatePicker
            DateTime dataAchizitie = dtpDataAchizitie.SelectedDate ?? DateTime.Today;

            // Adaugam cartea in lista
            Autor autorNou = new Autor(listaCarte.Count + 1, autor);
            Carte carteNoua = new Carte(listaCarte.Count + 1, titlu, autorNou, nrExemplare, genSelectat);
            listaCarte.Add(carteNoua);
            SeteazaComboBoxCarti();

            MessageBox.Show($"Carte adăugată: {titlu} - {autor} ({nrExemplare} exemplare)\nGen: {genSelectat}\nData achiziției: {dataAchizitie:dd.MM.yyyy}");
        }

        // =============================================
        // CODUL ORIGINAL — Cautare
        // =============================================

        private void Cauta_Click(object sender, RoutedEventArgs e)
        {
            string cautare = txtCautare.Text;
            string termen = txtCautare.Text.ToLower();

            if (cautare == "Moromeții")
            {
                MessageBox.Show("Carte găsită!");
            }
            else
            {
                MessageBox.Show("Nu există!");
            }
        }

        // =============================================
        // Meniu File > Exit
        // =============================================

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}