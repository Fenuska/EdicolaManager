using EdicolaManager.ExtensionMethods;
using EdicolaManager.Models;
using System;
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
            //MagazineSoldList = MagazineAvailable.Select(p => new MagazineSoldOverview
            //{
            //    IdMagazine = p.IdMagazine,
            //    Nome = p.Nome,
            //    Numero = (int)p.Numero,
            //    CopieVendute = 0,
            //    CopieDisponibili = p.NumeroCopieTotale - p.NumeroCopieRese - p.NumeroCopieVendute,
            //    Prezzo = p.Prezzo,
            //    ISSN = p.ISSN
            //}).ToList();

            cbInserto.ItemsSource = MagazineAvailable;
        }

        private void btnAddMagazine_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("Il magazine cercato non è disponibile: terminate le copie o codice non valido.");
            }
            catch (Exception)
            {
                //TODO: to be implemented
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
            catch (Exception ex)
            {
                //TODO: to be implemented
            }
        }

        private void CbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UpdateAvailableCopies();
            }
            catch(Exception ex)
            {
                //TODO: to be implemented
            }
        }

        private void MagazineSoldWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                scanner.Read(e);
                if (e.Key == Key.Return)
                {
                    magazine = MagazineAvailable.FirstOrDefault(p => p.ISSN == scanner.resultCode &&
                         (p.NumeroCopieTotale > p.NumeroCopieVendute + p.NumeroCopieRese));
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
            catch (Exception ex)
            {
                //TODO: to be implemented
            }
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

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
            magazine.UpdateMagazine();
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
