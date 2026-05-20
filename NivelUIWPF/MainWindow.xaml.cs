using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using NivelUIWPF.ViewModels;
using LibrarieModele.Enums;
using LibrarieModele.Models;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        private CarteViewModel carteVM;
        private ObservableCollection<Autor> listaAutori = new ObservableCollection<Autor>();
        private ObservableCollection<Persoana> listaPersoane = new ObservableCollection<Persoana>();
        private List<Imprumut> listaImprumuturi = new List<Imprumut>();

        private int nextIdAutor = 4;
        private int nextIdCarte = 4;
        private int nextIdPersoana = 3;
        private int nextIdImprumut = 1;

        public MainWindow()
        {
            InitializeComponent();

            // LAB11: MVVM — initializam ViewModel si setam DataContext
            carteVM = new CarteViewModel();
            DataContext = carteVM;

            Autor autor1 = new Autor(1, "Marin Preda");
            Autor autor2 = new Autor(2, "Mihai Eminescu");
            Autor autor3 = new Autor(3, "Ion Creanga");

            listaAutori.Add(autor1);
            listaAutori.Add(autor2);
            listaAutori.Add(autor3);

            listaPersoane.Add(new Persoana(1, "Ion Ionescu"));
            listaPersoane.Add(new Persoana(2, "Maria Popescu"));

            SeteazaListBoxGen(lstGenCarte);
            SeteazaListBoxGen(lstGenCarteModifica);

            // Legam lstCarti la ObservableCollection din ViewModel
            lstCarti.ItemsSource = carteVM.Carti;
            cmbCarti.ItemsSource = carteVM.Carti;

            lstAutori.ItemsSource = listaAutori;
            lstPersoane.ItemsSource = listaPersoane;

            dtpDataImprumut.SelectedDate = DateTime.Today;
        }

        // ============================================================
        // METODE HELPER
        // ============================================================

        private void SeteazaListBoxGen(System.Windows.Controls.ListBox listBox)
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = Enum.GetValues(typeof(GenCarte));
        }

        private void SeteazaComboBoxCarti()
        {
            cmbCarti.ItemsSource = null;
            cmbCarti.ItemsSource = carteVM.Carti;
        }

        private int GetNrCartiImprumutate(Persoana persoana)
        {
            return listaImprumuturi
                .Count(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null);
        }

        private bool PoateFaceImprumut(Persoana persoana)
        {
            return GetNrCartiImprumutate(persoana) < 3;
        }

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
        // MENIU
        // ============================================================

        private void MenuAdauga_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelAdauga.Visibility = Visibility.Visible;
            radioAdauga.IsChecked = true;
        }

        private void MenuModifica_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelModifica.Visibility = Visibility.Visible;
            radioEditeaza.IsChecked = true;
        }

        private void MenuAutori_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelAutori.Visibility = Visibility.Visible;
        }

        private void MenuPersoane_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelPersoane.Visibility = Visibility.Visible;
        }

        private void MenuImprumuta_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelImprumuta.Visibility = Visibility.Visible;

            cmbPersoaneImprumut.ItemsSource = null;
            cmbPersoaneImprumut.ItemsSource = listaPersoane;

            cmbCartiImprumut.ItemsSource = null;
            cmbCartiImprumut.ItemsSource = carteVM.Carti.Where(c => c.EsteDisponibila()).ToList();

            txtAvertismentLimita.Visibility = Visibility.Collapsed;
            txtInfoCartiPersoana.Text = "";
            txtInfoDisponibilitateCarte.Text = "";
            txtMesajImprumut.Text = "";
            btnImprumuta.IsEnabled = true;
        }

        private void MenuReturneaza_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelReturneaza.Visibility = Visibility.Visible;

            cmbPersoaneReturnare.ItemsSource = null;
            cmbPersoaneReturnare.ItemsSource = listaPersoane;
            cmbImprumuturiReturnare.ItemsSource = null;
            txtMesajReturnare2.Text = "";
        }

        private void MenuVeziImprumuturi_Click(object sender, RoutedEventArgs e)
        {
            AscundePaneluri();
            panelImprumuturi.Visibility = Visibility.Visible;

            dgImprumuturi.ItemsSource = null;
            dgImprumuturi.ItemsSource = listaImprumuturi
                .Where(i => i.DataReturnare == null)
                .ToList();

            txtMesajReturnare.Text = "";
        }

        private void Exit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();

        // ============================================================
        // RADIOBUTTON
        // ============================================================

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

            listaPersoane.Add(new Persoana(nextIdPersoana++, nume));
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

        private void cmbPersoaneImprumut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Persoana persoana = cmbPersoaneImprumut.SelectedItem as Persoana;
            if (persoana == null) return;

            int nrImprumutate = GetNrCartiImprumutate(persoana);
            txtInfoCartiPersoana.Text =
                $"{persoana.Nume} are {nrImprumutate} / 3 carti imprumutate.";

            if (!PoateFaceImprumut(persoana))
            {
                txtAvertismentLimita.Text =
                    $"ATENTIE! {persoana.Nume} a atins limita de 3 carti imprumutate.\n" +
                    "Nu se permite imprumutarea unei alte carti!";
                txtAvertismentLimita.Visibility = Visibility.Visible;
                btnImprumuta.IsEnabled = false;
            }
            else
            {
                txtAvertismentLimita.Visibility = Visibility.Collapsed;
                btnImprumuta.IsEnabled = true;
            }
        }

        private void cmbCartiImprumut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Carte carte = cmbCartiImprumut.SelectedItem as Carte;
            if (carte == null) return;

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

            if (!PoateFaceImprumut(persoana))
            {
                MessageBox.Show(
                    $"Imprumutul nu este permis!\n{persoana.Nume} a atins limita de 3 carti.",
                    "Imprumut blocat", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (!carte.EsteDisponibila())
            {
                MessageBox.Show($"Cartea \"{carte.Titlu}\" nu mai are exemplare disponibile!",
                    "Carte indisponibila", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime dataImprumut = dtpDataImprumut.SelectedDate ?? DateTime.Today;

            Imprumut imprumutNou = new Imprumut(nextIdImprumut++, persoana, carte, dataImprumut);
            listaImprumuturi.Add(imprumutNou);
            carte.ExemplareImprumutate++;

            txtMesajImprumut.Text =
                $"Imprumut inregistrat!\nPersoana: {persoana.Nume}\n" +
                $"Carte: {carte.Titlu}\nData: {dataImprumut:dd.MM.yyyy}";

            cmbCartiImprumut.ItemsSource = null;
            cmbCartiImprumut.ItemsSource = carteVM.Carti.Where(c => c.EsteDisponibila()).ToList();

            int nrNou = GetNrCartiImprumutate(persoana);
            txtInfoCartiPersoana.Text = $"{persoana.Nume} are acum {nrNou} / 3 carti imprumutate.";

            if (!PoateFaceImprumut(persoana))
            {
                txtAvertismentLimita.Text = $"ATENTIE! {persoana.Nume} a atins limita maxima de 3 carti!";
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

            List<Imprumut> imprumuturiActive = listaImprumuturi
                .Where(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null)
                .ToList();

            cmbImprumuturiReturnare.ItemsSource = null;
            cmbImprumuturiReturnare.ItemsSource = imprumuturiActive;

            txtMesajReturnare2.Text = imprumuturiActive.Count == 0
                ? $"{persoana.Nume} nu are imprumuturi active." : "";
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

            imprumutSelectat.DataReturnare = DateTime.Now;
            imprumutSelectat.Carte.ExemplareImprumutate--;

            txtMesajReturnare2.Text =
                $"Cartea \"{imprumutSelectat.Carte.Titlu}\" a fost returnata cu succes!\n" +
                $"Data returnarii: {imprumutSelectat.DataReturnare:dd.MM.yyyy}";

            Persoana persoana = cmbPersoaneReturnare.SelectedItem as Persoana;
            if (persoana != null)
            {
                cmbImprumuturiReturnare.ItemsSource = null;
                cmbImprumuturiReturnare.ItemsSource = listaImprumuturi
                    .Where(i => i.Persoana.Id == persoana.Id && i.DataReturnare == null)
                    .ToList();
            }
        }

        private void ReturneazaSelectata_Click(object sender, RoutedEventArgs e)
        {
            Imprumut imprumutSelectat = dgImprumuturi.SelectedItem as Imprumut;
            if (imprumutSelectat == null)
            {
                MessageBox.Show("Selectati un rand din tabel!", "Informatie",
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

                dgImprumuturi.ItemsSource = null;
                dgImprumuturi.ItemsSource = listaImprumuturi
                    .Where(i => i.DataReturnare == null).ToList();

                txtMesajReturnare.Text =
                    $"Cartea \"{imprumutSelectat.Carte.Titlu}\" a fost returnata la " +
                    $"{imprumutSelectat.DataReturnare:dd.MM.yyyy HH:mm}.";
            }
        }

        // ============================================================
        // CARTE — ListBox Gen
        // ============================================================

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
                MessageBox.Show("Selectati o carte!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNrExemplareModifica.Text, out int nrExemplare) || nrExemplare <= 0)
            {
                MessageBox.Show("Numar exemplare invalid!", "Eroare",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
        // LAB11: ADAUGARE CARTE — date din ViewModel prin Binding
        // ============================================================

        private void AdaugaCarte_Click(object sender, RoutedEventArgs e)
        {
            // LAB11: Datele vin din ViewModel prin Binding TwoWay
            string titlu = carteVM.TitluNou;
            string autor = carteVM.AutorNou;

            if (!int.TryParse(carteVM.NrExemplareNou, out int nrExemplare) || nrExemplare <= 0)
                return;

            GenCarte genSelectat = lstGenCarte.SelectedItem != null
                ? (GenCarte)lstGenCarte.SelectedItem : GenCarte.Roman;

            DateTime dataAchizitie = dtpDataAchizitie.SelectedDate ?? DateTime.Today;

            Autor autorNou = new Autor(nextIdAutor++, autor.Trim());
            Carte carteNoua = new Carte(nextIdCarte++, titlu.Trim(), autorNou, nrExemplare, genSelectat);

            // LAB10: ObservableCollection — lstCarti se actualizeaza automat
            carteVM.Carti.Add(carteNoua);

            MessageBox.Show($"Cartea \"{carteNoua.Titlu}\" a fost adaugata!\nData: {dataAchizitie:dd.MM.yyyy}");

            // LAB11: Resetam campurile prin ViewModel
            carteVM.TitluNou = "";
            carteVM.AutorNou = "";
            carteVM.NrExemplareNou = "";
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

            List<Carte> rezultate = carteVM.Carti
                .Where(c => c.Titlu.ToLower().Contains(termen))
                .ToList();

            if (rezultate.Count == 0)
                txtRezultatCautare.Text = $"Nu s-au gasit carti cu titlul \"{txtCautare.Text}\".";
            else
                txtRezultatCautare.Text = string.Join("\n",
                    rezultate.Select(c =>
                        $"• {c.Titlu} — {c.Autor.Nume} | " +
                        $"{(c.EsteDisponibila() ? "Disponibila" : "Indisponibila")}"));
        }
    }
}