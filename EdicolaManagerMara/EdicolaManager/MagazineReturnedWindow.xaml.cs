using EdicolaManager.Models;
using Extensions.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for MagazineReturnedWindow.xaml
    /// </summary>
    public partial class MagazineReturnedWindow : Window
    {
        private List<MagazineModel> MagazineList;
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private MagazineModel magazine;
        private CronologiaModel cronologia;
        private int numeroCopieRese;
        private BarcodeScanner scanner;

        public MagazineReturnedWindow()
        {
            InitializeComponent();
            cronologia = new CronologiaModel(_connection);
            magazine = new MagazineModel(_connection);
            GetListOfAvailableMagazine();
            UpdateComboboxMagazine();
            scanner = new BarcodeScanner();
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateMagazine();
                tbLog.Text += $"Periodico {magazine.Nome} - numero {magazine.Numero} - aggiunte {numeroCopieRese} copie ai resi. \n";
                GetListOfAvailableMagazine();
                UpdateComboboxMagazine();
                GetAmountOfCopies();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Problema riscontrato durante il reso");
            }
        }

        private void cbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FocusOnSelectedMagazine();
                GetAmountOfCopies();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel trovare le copie rimanenti per il magazine selezionato");
            }
        }

        private void cbInserto_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                scanner.Read(e);
                if (e.Key == Key.Return)
                {
                    magazine = MagazineList.FirstOrDefault(p => p.ISSN == scanner.resultCode);
                    scanner.resultCode = string.Empty;
                    if (magazine != null)
                    {
                        cbInserto.SelectedItem = new { Nome = $"{magazine.Nome} - Numero {magazine.Numero}", magazine.IdMagazine };
                        GetAmountOfCopies();
                    }
                    else
                    {
                        cbInserto.Text = string.Empty;
                        MessageBox.Show("Rivista non trovata");
                    }
                }
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel rilevamento della rivista con lo scanner");
            }

        }

        private void GetListOfAvailableMagazine()
        {
            MagazineList = magazine.GetAvailableMagazineList();
        }

        private void UpdateComboboxMagazine()
        {
            cbInserto.ItemsSource = MagazineList.Select(p => new { Nome = $"{p.Nome} - Numero {p.Numero}", p.IdMagazine }).OrderBy(p => p.Nome);
        }

        private void UpdateMagazine()
        {
            if (magazine != null)
            {
                numeroCopieRese = cbNumeroCopie.SelectedValue.ToInt();

                magazine.NumeroCopieRese += numeroCopieRese;
                magazine.Update();
                cronologia.TrackReturnedMagazine(magazine, numeroCopieRese);
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void GetAmountOfCopies()
        {
            int amountAvailableMagazine = 0;
            if (magazine != null)
                amountAvailableMagazine = magazine.NumeroCopieTotale - magazine.NumeroCopieVendute - magazine.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, amountAvailableMagazine + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
            cbNumeroCopie.SelectedIndex = 0;
        }

        private void FocusOnSelectedMagazine()
        {
            string IdMagazineSelected = cbInserto.SelectedValue?.ToString() ?? "0";
            magazine = MagazineList.FirstOrDefault(p => p.IdMagazine == IdMagazineSelected.ToInt());
        }

    }
}
