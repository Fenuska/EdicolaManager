using EdicolaManager.Models;
using Extensions.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EdicolaManager
{
    public partial class MagazineSoldWindow : Window
    {
        private List<MagazineSoldOverview> MagazineSoldList;
        public List<MagazineModel> MagazineAvailable;
        private string ISBN = string.Empty;
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private MagazineModel magazine;
        private MagazineSoldOverview magazineSold;
        private CronologiaModel cronologia;
        private BarcodeScanner scanner;
        private int magazineId;

        public MagazineSoldWindow()
        {
            InitializeComponent();
            magazine = new MagazineModel(_connection);
            cronologia = new CronologiaModel(_connection);
            scanner = new BarcodeScanner();

            MagazineAvailable = magazine.GetAvailableMagazineList();
            MagazineSoldList = new List<MagazineSoldOverview>();
            cbInserto.ItemsSource = MagazineAvailable.Select(p=>new {Nome = $"{p.Nome} - Numero {p.Numero}", p.IdMagazine}).OrderBy(p=> p.Nome);
        }

        protected void btnAddMagazine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                magazineId = GetMagazineId();
                magazine = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == magazineId);
                if (magazine != null)
                {
                    ImportMagazineSelectedIntoSold(magazine);

                    var copie = cbNumeroCopie.SelectedValue != null ? cbNumeroCopie.SelectedValue.ToInt() : 0;

                    AggiungiCopiaVendutaAlMagazine(copie);
                    AggiungiItemAlCarrello();
                }
                else
                    MessageBox.Show("La rivista non è disponibile: terminate le copie o codice non valido");
            }
            catch
            {
                //TODO: to be implemented
                MessageBox.Show("Errore durante l'inserimento della rivista nel carrello");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var magazineSoldItem in MagazineSoldList)
                {
                    magazine = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == magazineSoldItem.IdMagazine);
                    magazine.NumeroCopieVendute += magazineSoldItem.CopieVendute;

                    SaveMagazineChanges();
                }
                this.Close();
            }
            catch 
            {
                //TODO: to be implemented
                MessageBox.Show("Errore durante il salvataggio.");
            }
        }

        private void CbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateAvailableCopies();
            }
            catch 
            {
                //TODO: to be implemented
                MessageBox.Show("Impossibile trovare il numero di copie disponibili.");
            }
        }

        private void MagazineSoldWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                scanner.Read(e);
                if (e.Key == Key.Return)
                {
                    var rivistaVendutaNelCarrello = MagazineSoldList.OrderByDescending(p=> p.Numero)
                        .FirstOrDefault(p => p.ISSN == scanner.resultCode);

                    magazine = MagazineAvailable.OrderByDescending(p=> p.Numero)
                        .FirstOrDefault(p => p.ISSN == scanner.resultCode 
                        && (p.Numero == (rivistaVendutaNelCarrello?.Numero ?? p.Numero)) 
                        && (p.NumeroCopieTotale > 
                        (rivistaVendutaNelCarrello?.CopieVendute ?? 0) + p.NumeroCopieVendute + p.NumeroCopieRese));

                    if (magazine != null)
                    {
                        ImportMagazineSelectedIntoSold(magazine);
                        AggiungiCopiaVendutaAlMagazine(1);
                        AggiungiItemAlCarrello();
                    }
                    else
                        MessageBox.Show("Il magazine cercato non è disponibile: terminate le copie o codice non valido.");
                    scanner.resultCode = string.Empty;
                    cbInserto.Text = string.Empty;
                }
            }
            catch 
            {
                //TODO: to be implemented
                MessageBox.Show("Problemi durante la lettura del codice a barre.");
            }
        }

        private void ImportMagazineSelectedIntoSold(MagazineModel magazineSelected)
        {
            magazineSold = MagazineSoldList.FirstOrDefault(p => p.IdMagazine == magazineSelected.IdMagazine);
            if (magazineSold == null)
            {
                magazineSold = new MagazineSoldOverview
                {
                    IdMagazine = magazineSelected.IdMagazine,
                    Nome = magazineSelected.Nome,
                    Numero = (int)magazineSelected.Numero,
                    CopieVendute = 0,
                    CopieDisponibili = magazineSelected.NumeroCopieTotale - magazineSelected.NumeroCopieRese - magazineSelected.NumeroCopieVendute,
                    Prezzo = magazineSelected.Prezzo,
                    ISSN = magazineSelected.ISSN
                };
                MagazineSoldList.Add(magazineSold);
            }
        }

        private int GetMagazineId()
        {
            return cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToInt() : 0;
        }

        private void AggiungiItemAlCarrello()
        {
            LoadMagazineList();
            UpdateTotalPrice();
        }

        private void AggiungiCopiaVendutaAlMagazine(int copie)
        {
            magazineSold.CopieVendute += copie;
            magazineSold.CopieDisponibili -= copie;
        }

        private void LoadMagazineList()
        {
            gridMagazineSold.ItemsSource = null;
            gridMagazineSold.ItemsSource = MagazineSoldList;
        }

        private void SaveMagazineChanges()
        {
            magazine.Update();
            cronologia.TrackSoldMagazine(magazine, MagazineSoldList.FirstOrDefault(p => p.IdMagazine == magazine.IdMagazine).CopieVendute);
        }

        private void UpdateAvailableCopies()
        {
            magazineId = GetMagazineId();
            magazine = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == magazineId);

            if (magazine != null)
            {
                var copieDisponibili = magazine.NumeroCopieTotale - magazine.NumeroCopieVendute - magazine.NumeroCopieRese;
                magazineSold = MagazineSoldList.FirstOrDefault(p => p.IdMagazine == magazineId);
                if (magazineSold != null)
                    copieDisponibili -= magazineSold.CopieVendute;
                var rangeCopie = Enumerable.Range(0, copieDisponibili + 1);
                cbNumeroCopie.ItemsSource = rangeCopie;
                cbNumeroCopie.SelectedIndex = 0;
            }
        }

        private void UpdateTotalPrice()
        {
            lblTotalPrice.Content = MagazineSoldList.Where(p => p.CopieVendute > 0).Sum(p => p.Prezzo * p.CopieVendute);
        }
    }

    class MagazineSoldOverview
    {
        public string Nome { get; set; }
        public int Numero { get; set; }
        public int CopieVendute { get; set; }
        public int CopieDisponibili { get; set; }
        public decimal Prezzo { get; set; }
        public int IdMagazine { get; set; }
        public string ISSN { get; set; }
    }
}
