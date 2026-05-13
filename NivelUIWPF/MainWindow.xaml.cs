using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Collections.ObjectModel;
using System.Linq;
=======
>>>>>>> salvare-commits
using System.Windows;
using System.Windows.Media;
using NivelUIWPF.ViewModels;
using LibrarieModele.Enums;
using LibrarieModele.Models;
<<<<<<< HEAD
using LibraryApp.Config;
=======
using LibraryApp; // pentru Constante
>>>>>>> salvare-commits

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
<<<<<<< HEAD
        // Liste de date
        private List<Carte> listaCarte = new List<Carte>();
        private ObservableCollection<Autor> listaAutori = new ObservableCollection<Autor>();
        private ObservableCollection<Persoana> listaPersoane = new ObservableCollection<Persoana>();
        private List<Imprumut> listaImprumuturi = new List<Imprumut>();

        // Contoare ID
        private int nextIdAutor = 4;
        private int nextIdCarte = 4;
        private int nextIdPersoana = 3;
        private int nextIdImprumut = 1;
=======
        // LAB9: Lista de carti folosita de ComboBox si ListBox
        private List<Carte> listaCarte = new List<Carte>();
>>>>>>> salvare-commits

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarteViewModel();

<<<<<<< HEAD
            // Date initiale de test
=======
            // Initializam lista de carti cu cateva exemple
>>>>>>> salvare-commits
            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

<<<<<<< HEAD
            listaCarte.Add(new Carte(1, "Morometii", autor1, 10, GenCarte.Roman));
            listaCarte.Add(new Carte(2, "Luceafarul", autor2, 5, GenCarte.Poezie));
            listaCarte.Add(new Carte(3, "Amintiri din copilarie", autor3, 8, GenCarte.Roman));

            listaAutori.Add(autor1);
            listaAutori.Add(autor2);
            listaAutori.Add(autor3);

            // Persoane initiale de test
            listaPersoane.Add(new Persoana(1, "Ion Ionescu"));
            listaPersoane.Add(new Persoana(2, "Maria Popescu"));

            SeteazaListBoxGen(lstGenCarte);
            SeteazaListBoxGen(lstGenCarteModifica);
            SeteazaComboBoxCarti();

            lstCarti.ItemsSource = listaCarte;
            lstAutori.ItemsSource = listaAutori;
            lstPersoane.ItemsSource = listaPersoane;

            // Setam data curenta in DatePicker-ul de imprumut
            dtpDataImprumut.SelectedDate = DateTime.Today;
        }

        // ============================================================
        // METODE HELPER
        // ============================================================

=======
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
>>>>>>> salvare-commits
        private void SeteazaListBoxGen(System.Windows.Controls.ListBox listBox)
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = Enum.GetValues(typeof(GenCarte));
        }

<<<<<<< HEAD
=======
        // Populeaza ComboBox-ul cu lista de carti
>>>>>>> salvare-commits
        private void SeteazaComboBoxCarti()
        {
            cmbCarti.ItemsSource = null;
            cmbCarti.ItemsSource = listaCarte;
        }

<<<<<<< HEAD
        // Returneaza numarul de imprumuturi ACTIVE ale unei persoane
        private int GetNrCartiImprumutate(Persoana persoana)
        {
            return listaImprumuturi
                .Count(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null);
        }

        // Verifica daca persoana poate face un nou imprumut
        private bool PoateFaceImprumut(Persoana persoana)
        {
            return GetNrCartiImprumutate(persoana) < Configurari.LIMITA_CARTI_IMPRUMUTATE;
        }

        // ============================================================
        // MENIU
        // ============================================================

        private void MenuAdauga_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelAdauga.Visibility = Visibility.Visible;
=======
        // =============================================
        // Meniu: Adauga / Modifica
        // =============================================

        private void MenuAdauga_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Visible;
            panelModifica.Visibility = Visibility.Collapsed;
>>>>>>> salvare-commits
            radioAdauga.IsChecked = true;
        }

        private void MenuModifica_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            AscundePaneluri();
=======
            panelAdauga.Visibility = Visibility.Collapsed;
>>>>>>> salvare-commits
            panelModifica.Visibility = Visibility.Visible;
            radioEditeaza.IsChecked = true;
        }

<<<<<<< HEAD
        private void MenuAutori_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelAutori.Visibility = Visibility.Visible;
        }

        // Meniu Persoane — deschide panelul CRUD persoane
        private void MenuPersoane_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelPersoane.Visibility = Visibility.Visible;
        }

        // Meniu Imprumuta — deschide formularul de imprumut
        private void MenuImprumuta_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelImprumuta.Visibility = Visibility.Visible;

            // Populam ComboBox-urile cu datele actuale
            cmbPersoaneImprumut.ItemsSource = null;
            cmbPersoaneImprumut.ItemsSource = listaPersoane;

            // Afisam doar cartile DISPONIBILE (EsteDisponibila() == true)
            cmbCartiImprumut.ItemsSource = null;
            cmbCartiImprumut.ItemsSource = listaCarte.Where(c => c.EsteDisponibila()).ToList();

            // Resetam mesajele
            txtAvertismentLimita.Visibility = Visibility.Collapsed;
            txtInfoCartiPersoana.Text = "";
            txtInfoDisponibilitateCarte.Text = "";
            txtMesajImprumut.Text = "";
            btnImprumuta.IsEnabled = true;
        }

        // Meniu Returneaza — deschide formularul de returnare
        private void MenuReturneaza_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelReturneaza.Visibility = Visibility.Visible;

            cmbPersoaneReturnare.ItemsSource = null;
            cmbPersoaneReturnare.ItemsSource = listaPersoane;
            cmbImprumuturiReturnare.ItemsSource = null;
            txtMesajReturnare2.Text = "";
        }

        // Meniu Vezi imprumuturi — afiseaza DataGrid-ul cu imprumuturi active
        private void MenuVeziImprumuturi_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelImprumuturi.Visibility = Visibility.Visible;

            // Incarcam doar imprumuturile active in DataGrid
            dgImprumuturi.ItemsSource = null;
            dgImprumuturi.ItemsSource = listaImprumuturi
                .Where(i => i.DataReturnare == null)
                .ToList();

            txtMesajReturnare.Text = "";
        }

        private void Exit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();

        // Ascunde toate panelurile din coloana dreapta si stanga
        private void AscundePaneluri()
        {
            panelAdauga.Visibility = Visibility.Collapsed;
            panelModifica.Visibility = Visibility.Collapsed;
            panelAutori.Visibility = Visibility.Collapsed;
            panelPersoane.Visibility = Visibility.Collapsed;
            panelImprumuta.Visibility = Visibility.Collapsed;
            panelReturneaza.Visibility = Visibility.Collapsed;
            panelImprumuturi.Visibility = Visibility.Collapsed;
        }

        // ============================================================
        // RADIOBUTTON
        // ============================================================
=======
        // =============================================
        // CERINTA 2: Handleri RadioButton
        // =============================================
>>>>>>> salvare-commits

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

<<<<<<< HEAD
        private void lstCarti_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }

        // ============================================================
        // CRUD AUTOR
        // ============================================================

        private void AdaugaAutor_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumeAutor.Text.Trim();
            if (string.IsNullOrWhiteSpace(nume))
            {
                MessageBox.Show("Introduceti un nume valid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            listaAutori.Add(new Autor(nextIdAutor++, nume));
            txtMesajAutor.Text = $"Autorul \"{nume}\" a fost adaugat!";
            txtNumeAutor.Clear();
        }

        private void ModificaAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autorSelectat = lstAutori.SelectedItem as Autor;
            string numeNou = txtNumeAutor.Text.Trim();

            if (autorSelectat == null || string.IsNullOrWhiteSpace(numeNou))
            {
                MessageBox.Show("Selectati un autor si introduceti un nume valid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int index = listaAutori.IndexOf(autorSelectat);
            autorSelectat.Nume = numeNou;
            listaAutori.RemoveAt(index);
            listaAutori.Insert(index, autorSelectat);
            txtMesajAutor.Text = $"Autorul a fost modificat in \"{numeNou}\"!";
        }

        private void StergeAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autorSelectat = lstAutori.SelectedItem as Autor;
            if (autorSelectat == null)
            {
                MessageBox.Show("Selectati un autor din lista!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult rezultat = MessageBox.Show(
                $"Sigur vreti sa stergeti autorul \"{autorSelectat.Nume}\"?",
                "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultat == MessageBoxResult.Yes)
            {
                string nume = autorSelectat.Nume;
                listaAutori.Remove(autorSelectat);
                txtMesajAutor.Text = $"Autorul \"{nume}\" a fost sters!";
                txtNumeAutor.Clear();
            }
        }

        private void lstAutori_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAutori.SelectedItem is Autor autorSelectat)
            {
                txtNumeAutor.Text = autorSelectat.Nume;
                txtMesajAutor.Text = "";
            }
        }

        // ============================================================
        // CRUD PERSOANA
        // ============================================================

        private void AdaugaPersoana_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNumePersoana.Text.Trim();

            // Validare
            if (string.IsNullOrWhiteSpace(nume))
            {
                txtNumePersoana.Background = Brushes.LightCoral;
                lblEroarePersoana.Text = "Numele persoanei este obligatoriu.";
                lblEroarePersoana.Visibility = Visibility.Visible;
                return;
            }
            if (nume.Length < 3)
            {
                txtNumePersoana.Background = Brushes.LightCoral;
                lblEroarePersoana.Text = "Numele trebuie sa aiba minim 3 caractere.";
                lblEroarePersoana.Visibility = Visibility.Visible;
                return;
            }

            txtNumePersoana.Background = Brushes.White;
            lblEroarePersoana.Visibility = Visibility.Collapsed;

            Persoana persoanaNoua = new Persoana(nextIdPersoana++, nume);
            listaPersoane.Add(persoanaNoua); 
            txtMesajPersoana.Text = $"Persoana \"{nume}\" a fost adaugata!";
            txtNumePersoana.Clear();
        }

        private void ModificaPersoana_Click(object sender, RoutedEventArgs e)
        {
            Persoana persoanaSelectata = lstPersoane.SelectedItem as Persoana;
            string numeNou = txtNumePersoana.Text.Trim();

            if (persoanaSelectata == null || string.IsNullOrWhiteSpace(numeNou))
            {
                txtNumePersoana.Background = Brushes.LightCoral;
                lblEroarePersoana.Text = persoanaSelectata == null
                    ? "Selectati o persoana din lista."
                    : "Introduceti un nume valid.";
                lblEroarePersoana.Visibility = Visibility.Visible;
                return;
            }

            txtNumePersoana.Background = Brushes.White;
            lblEroarePersoana.Visibility = Visibility.Collapsed;

            int index = listaPersoane.IndexOf(persoanaSelectata);
            persoanaSelectata.Nume = numeNou;
            listaPersoane.RemoveAt(index);
            listaPersoane.Insert(index, persoanaSelectata);
            txtMesajPersoana.Text = $"Persoana a fost modificata in \"{numeNou}\"!";
        }

        private void StergePersoana_Click(object sender, RoutedEventArgs e)
        {
            Persoana persoanaSelectata = lstPersoane.SelectedItem as Persoana;
            if (persoanaSelectata == null)
            {
                lblEroarePersoana.Text = "Selectati o persoana din lista.";
                lblEroarePersoana.Visibility = Visibility.Visible;
                return;
            }

            // Verificam daca persoana are imprumuturi active — nu o putem sterge
            int nrActive = GetNrCartiImprumutate(persoanaSelectata);
            if (nrActive > 0)
            {
                MessageBox.Show(
                    $"Persoana \"{persoanaSelectata.Nume}\" are {nrActive} imprumut(uri) active.\n" +
                    "Returnati cartile inainte de a sterge persoana.",
                    "Nu se poate sterge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult rezultat = MessageBox.Show(
                $"Sigur vreti sa stergeti persoana \"{persoanaSelectata.Nume}\"?",
                "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultat == MessageBoxResult.Yes)
            {
                string nume = persoanaSelectata.Nume;
                listaPersoane.Remove(persoanaSelectata);
                txtMesajPersoana.Text = $"Persoana \"{nume}\" a fost stearsa!";
                ResetPersoanaForm();
            }
        }

        private void ResetPersoana_Click(object sender, RoutedEventArgs e) => ResetPersoanaForm();

        private void ResetPersoanaForm()
        {
            txtNumePersoana.Clear();
            txtNumePersoana.Background = Brushes.White;
            lblEroarePersoana.Visibility = Visibility.Collapsed;
            lstPersoane.SelectedItem = null;
        }

        private void lstPersoane_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstPersoane.SelectedItem is Persoana persoana)
            {
                txtNumePersoana.Text = persoana.Nume;
                txtNumePersoana.Background = Brushes.White;
                lblEroarePersoana.Visibility = Visibility.Collapsed;
                txtMesajPersoana.Text = "";
            }
        }

        // ============================================================
        // IMPRUMUT CARTE
        // ============================================================

        // Cand se selecteaza o persoana — verificam limita si afisam info
        private void cmbPersoaneImprumut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Persoana persoana = cmbPersoaneImprumut.SelectedItem as Persoana;
            if (persoana == null) return;

            int nrImprumutate = GetNrCartiImprumutate(persoana);

            // Afisam numarul de carti imprumutate curent
            txtInfoCartiPersoana.Text =
                $"{persoana.Nume} are {nrImprumutate} / {Configurari.LIMITA_CARTI_IMPRUMUTATE} carti imprumutate.";

            // Verificam daca a depasit limita din Configurari
            if (!PoateFaceImprumut(persoana))
            {
                // MESAJ DE ATENTIONARE + BLOCARE (cerinta din enunt)
                txtAvertismentLimita.Text =
                    $"ATENTIE! {persoana.Nume} a atins limita de " +
                    $"{Configurari.LIMITA_CARTI_IMPRUMUTATE} carti imprumutate.\n" +
                    "Nu se permite imprumutarea unei alte carti!";
                txtAvertismentLimita.Visibility = Visibility.Visible;

                // Dezactivam butonul — imprumutul este blocat
                btnImprumuta.IsEnabled = false;
            }
            else
            {
                txtAvertismentLimita.Visibility = Visibility.Collapsed;
                btnImprumuta.IsEnabled = true;
            }
        }

        // Cand se selecteaza o carte — afisam disponibilitatea
        private void cmbCartiImprumut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Carte carte = cmbCartiImprumut.SelectedItem as Carte;
            if (carte == null) return;

            // Cerinta din enunt: "pentru o anumita carte se va afisa daca este disponibila sau nu"
            if (carte.EsteDisponibila())
            {
                txtInfoDisponibilitateCarte.Text = $"Cartea \"{carte.Titlu}\" este DISPONIBILA.";
                txtInfoDisponibilitateCarte.Foreground = Brushes.DarkGreen;
            }
            else
            {
                txtInfoDisponibilitateCarte.Text = $"Cartea \"{carte.Titlu}\" NU este disponibila.";
                txtInfoDisponibilitateCarte.Foreground = Brushes.Red;
            }
        }

        // Confirmare imprumut
        private void ConfirmaImprumut_Click(object sender, RoutedEventArgs e)
        {
            Persoana persoana = cmbPersoaneImprumut.SelectedItem as Persoana;
            Carte carte = cmbCartiImprumut.SelectedItem as Carte;

            if (persoana == null || carte == null)
            {
                MessageBox.Show("Selectati o persoana si o carte!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificare finala limita (double-check de siguranta)
            if (!PoateFaceImprumut(persoana))
            {
                MessageBox.Show(
                    $"Imprumutul nu este permis!\n" +
                    $"{persoana.Nume} a atins limita de {Configurari.LIMITA_CARTI_IMPRUMUTATE} carti.",
                    "Imprumut blocat", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            // Verificare disponibilitate carte
            if (!carte.EsteDisponibila())
            {
                MessageBox.Show($"Cartea \"{carte.Titlu}\" nu mai are exemplare disponibile!",
                    "Carte indisponibila", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime dataImprumut = dtpDataImprumut.SelectedDate ?? DateTime.Today;

            // Cream imprumutul
            Imprumut imprumutNou = new Imprumut(nextIdImprumut++, persoana, carte, dataImprumut);
            listaImprumuturi.Add(imprumutNou);

            // Actualizam exemplarele imprumutate
            carte.ExemplareImprumutate++;

            txtMesajImprumut.Text =
                $"Imprumut inregistrat!\n" +
                $"Persoana: {persoana.Nume}\n" +
                $"Carte: {carte.Titlu}\n" +
                $"Data: {dataImprumut:dd.MM.yyyy}";

            // Actualizam ComboBox-ul de carti disponibile
            cmbCartiImprumut.ItemsSource = null;
            cmbCartiImprumut.ItemsSource = listaCarte.Where(c => c.EsteDisponibila()).ToList();

            // Actualizam info limita pentru persoana
            int nrNou = GetNrCartiImprumutate(persoana);
            txtInfoCartiPersoana.Text =
                $"{persoana.Nume} are acum {nrNou} / {Configurari.LIMITA_CARTI_IMPRUMUTATE} carti imprumutate.";

            // Daca acum a atins limita, afisam avertismentul
            if (!PoateFaceImprumut(persoana))
            {
                txtAvertismentLimita.Text =
                    $"ATENTIE! {persoana.Nume} a atins limita maxima de {Configurari.LIMITA_CARTI_IMPRUMUTATE} carti!";
                txtAvertismentLimita.Visibility = Visibility.Visible;
                btnImprumuta.IsEnabled = false;
            }
        }

        // ============================================================
        // RETURNARE CARTE
        // ============================================================
        private void cmbPersoaneReturnare_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Persoana persoana = cmbPersoaneReturnare.SelectedItem as Persoana;
            if (persoana == null) return;

            // Filtram imprumuturile active ale persoanei selectate
            List<Imprumut> imprumuturiActive = listaImprumuturi
                .Where(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null)
                .ToList();

            cmbImprumuturiReturnare.ItemsSource = null;
            cmbImprumuturiReturnare.ItemsSource = imprumuturiActive;

            if (imprumuturiActive.Count == 0)
                txtMesajReturnare2.Text = $"{persoana.Nume} nu are imprumuturi active.";
            else
                txtMesajReturnare2.Text = "";
        }

        private void ConfirmaReturnare_Click(object sender, RoutedEventArgs e)
        {
            Imprumut imprumutSelectat = cmbImprumuturiReturnare.SelectedItem as Imprumut;

            if (imprumutSelectat == null)
            {
                MessageBox.Show("Selectati un imprumut de returnat!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Marcam imprumutul ca returnat
            imprumutSelectat.DataReturnare = DateTime.Now;

            // Reducem exemplarele imprumutate
            imprumutSelectat.Carte.ExemplareImprumutate--;

            txtMesajReturnare2.Text =
                $"Cartea \"{imprumutSelectat.Carte.Titlu}\" a fost returnata cu succes!\n" +
                $"Data returnarii: {imprumutSelectat.DataReturnare:dd.MM.yyyy}";

            // Actualizam lista de imprumuturi din ComboBox
            Persoana persoana = cmbPersoaneReturnare.SelectedItem as Persoana;
            if (persoana != null)
            {
                cmbImprumuturiReturnare.ItemsSource = null;
                cmbImprumuturiReturnare.ItemsSource = listaImprumuturi
                    .Where(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null)
                    .ToList();
            }
        }

        // Returnare din DataGrid-ul de imprumuturi active
        private void ReturneazaSelectata_Click(object sender, RoutedEventArgs e)
        {
            Imprumut imprumutSelectat = dgImprumuturi.SelectedItem as Imprumut;
            if (imprumutSelectat == null)
            {
                MessageBox.Show("Selectati un rand din tabel pentru a returna cartea!", "Informatie",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult rezultat = MessageBox.Show(
                $"Confirmati returnarea cartii \"{imprumutSelectat.Carte.Titlu}\" " +
                $"de catre {imprumutSelectat.Persoana.Nume}?",
                "Confirmare returnare", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultat == MessageBoxResult.Yes)
            {
                imprumutSelectat.DataReturnare = DateTime.Now;
                imprumutSelectat.Carte.ExemplareImprumutate--;

                // Reincarcam DataGrid-ul cu imprumuturile active ramase
                dgImprumuturi.ItemsSource = null;
                dgImprumuturi.ItemsSource = listaImprumuturi
                    .Where(i => i.DataReturnare == null)
                    .ToList();

                txtMesajReturnare.Text =
                    $"Cartea \"{imprumutSelectat.Carte.Titlu}\" a fost returnata la " +
                    $"{imprumutSelectat.DataReturnare:dd.MM.yyyy HH:mm}.";
            }
        }

        // ============================================================
        // ADAUGARE + MODIFICARE CARTE (neschimbate)
        // ============================================================

        private void lstGenCarte_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void lstGenCarteModifica_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }

        private void AdaugaCarte_Click(object sender, RoutedEventArgs e)
        {
            string titlu = txtTitlu.Text;
            string autor = txtAutor.Text;
            bool valid = true;

            if (string.IsNullOrEmpty(titlu))
            { txtTitlu.Background = Brushes.LightCoral; valid = false; }
            else { txtTitlu.Background = Brushes.White; }

            if (string.IsNullOrEmpty(autor))
            { txtAutor.Background = Brushes.LightCoral; valid = false; }
            else { txtAutor.Background = Brushes.White; }

            if (!int.TryParse(txtNrExemplare.Text, out int nrExemplare) || nrExemplare <= 0)
            { txtNrExemplare.Background = Brushes.LightCoral; valid = false; }
            else { txtNrExemplare.Background = Brushes.White; }

            if (!valid)
            {
                MessageBox.Show("Completati corect toate campurile!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            GenCarte genSelectat = lstGenCarte.SelectedItem != null
                ? (GenCarte)lstGenCarte.SelectedItem : GenCarte.Roman;

            Autor autorNou = new Autor(nextIdAutor++, autor.Trim());
            Carte carteNoua = new Carte(nextIdCarte++, titlu.Trim(), autorNou, nrExemplare, genSelectat);
            listaCarte.Add(carteNoua);

            lstCarti.ItemsSource = null;
            lstCarti.ItemsSource = listaCarte;
            SeteazaComboBoxCarti();

            MessageBox.Show($"Cartea \"{carteNoua.Titlu}\" a fost adaugata!", "Succes",
                MessageBoxButton.OK, MessageBoxImage.Information);

            txtTitlu.Clear();
            txtAutor.Clear();
            txtNrExemplare.Clear();
        }
=======
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
>>>>>>> salvare-commits

        private void cmbCarti_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Carte carteSelectata = cmbCarti.SelectedItem as Carte;
            if (carteSelectata != null)
            {
<<<<<<< HEAD
                txtTitluModifica.Text = carteSelectata.Titlu;
                txtAutorModifica.Text = carteSelectata.Autor.Nume;
                txtNrExemplareModifica.Text = carteSelectata.NumarExemplare.ToString();
                dtpDataActualizare.SelectedDate = DateTime.Today;
                lstGenCarteModifica.SelectedItem = carteSelectata.Gen;
=======
                // Precompletam campurile cu datele cartii selectate
                txtTitluModifica.Text = carteSelectata.Titlu;
                txtAutorModifica.Text = carteSelectata.Autor.Nume;
                txtNrExemplareModifica.Text = carteSelectata.NumarExemplare.ToString();

                // LAB9: Setam data actualizarii la data curenta (read-only)
                dtpDataActualizare.SelectedDate = DateTime.Today;

                // LAB9: Selectam genul curent in ListBox
                lstGenCarteModifica.SelectedItem = carteSelectata.Gen;

>>>>>>> salvare-commits
                txtMesajModifica.Text = "";
            }
        }

<<<<<<< HEAD
        private void ActualizeazaCarte_Click(object sender, RoutedEventArgs e)
        {
            Carte carteSelectata = cmbCarti.SelectedItem as Carte;
            if (carteSelectata == null)
            {
                MessageBox.Show("Selectati o carte!", "Eroare",
=======
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
>>>>>>> salvare-commits
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNrExemplareModifica.Text, out int nrExemplare) || nrExemplare <= 0)
            {
<<<<<<< HEAD
                MessageBox.Show("Numar exemplare invalid!", "Eroare",
=======
                MessageBox.Show("Număr exemplare invalid!", "Eroare",
>>>>>>> salvare-commits
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

<<<<<<< HEAD
            carteSelectata.Titlu = txtTitluModifica.Text.Trim();
            carteSelectata.Autor.Nume = txtAutorModifica.Text.Trim();
            carteSelectata.NumarExemplare = nrExemplare;

            if (lstGenCarteModifica.SelectedItem != null)
                carteSelectata.Gen = (GenCarte)lstGenCarteModifica.SelectedItem;

            dtpDataActualizare.SelectedDate = DateTime.Today;
            SeteazaComboBoxCarti();
            txtMesajModifica.Text = $"Cartea \"{carteSelectata.Titlu}\" a fost actualizata!";
        }

        // ============================================================
        // CAUTARE
        // ============================================================

        private void Cauta_Click(object sender, RoutedEventArgs e)
        {
            string termen = txtCautare.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(termen))
            {
                txtRezultatCautare.Text = "Introduceti un termen de cautare.";
                return;
            }

            List<Carte> rezultate = listaCarte
                .Where(c => c.Titlu.ToLower().Contains(termen))
                .ToList();

            if (rezultate.Count == 0)
                txtRezultatCautare.Text = $"Nu s-au gasit carti cu titlul \"{txtCautare.Text}\".";
            else
                txtRezultatCautare.Text = string.Join("\n",
                    rezultate.Select(c =>
                        $"• {c.Titlu} — {c.Autor.Nume} | " +
                        $"{(c.EsteDisponibila() ? "Disponibila" : "Indisponibila")}"));
=======
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
>>>>>>> salvare-commits
        }
    }
}