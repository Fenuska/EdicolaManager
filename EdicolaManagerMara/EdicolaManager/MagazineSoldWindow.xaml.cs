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
        private Dictionary<int, int> MagazineSoldList = new Dictionary<int, int>();
        public List<MagazineModel> MagazineAvailable;
        private string ISBN = string.Empty;
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private MagazineModel magazine;
        private CronologiaModel cronologia;

        public MagazineSoldWindow()
        {
            InitializeComponent();
            magazine = new MagazineModel(_connection);
            cronologia = new CronologiaModel(_connection);
            MagazineAvailable = magazine.GetAvailableMagazineList();
            cbInserto.ItemsSource = MagazineAvailable;
        }

        private void btnAddMagazine_Click(object sender, RoutedEventArgs e)
        {
            UpdateMagazine();
            LoadMagazineList();
            UpdateAmountOfCopiesAvailable();
            UpdateTotalPrice();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var magazineId in MagazineSoldList.Keys)
            {
                magazine = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == magazineId);
                SaveMagazineChanges();
            }
            this.Close();
        }

        private void CbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAmountOfCopiesAvailable();
        }

        private void MagazineSoldWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var magazineSelected = magazine.GetMagazine().FirstOrDefault(p => p.ISSN == ISBN);
                cbInserto.SelectedItem = magazineSelected;
                ISBN = string.Empty;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
                ISBN += e.Key.ToString().Remove(0, 1);
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void LoadMagazineList()
        {
            cbInserto.ItemsSource = MagazineAvailable.Select(p => new { p.Nome, p.Numero, p.IdMagazine }).ToList();

            gridMagazineSold.ItemsSource = MagazineAvailable.Where(p => MagazineSoldList.ContainsKey(p.IdMagazine)).ToList();
        }

        private void UpdateMagazine()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";

            var magazine = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == IdMagazineSelected.ToInt());
            if (magazine != null)
            {
                var amount = cbNumeroCopie.SelectedValue.ToInt();
                magazine.NumeroCopieVendute += amount;
                if (!MagazineSoldList.ContainsKey(magazine.IdMagazine))
                    MagazineSoldList.Add(magazine.IdMagazine, amount);
                else
                    MagazineSoldList[magazine.IdMagazine] += amount;
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void SaveMagazineChanges()
        {
            magazine.UpdateMagazine();
            cronologia.TrackSoldMagazine(magazine, MagazineSoldList[magazine.IdMagazine]);
        }

        private void UpdateAmountOfCopiesAvailable()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            var MagazineSelected = MagazineAvailable.FirstOrDefault(p => p.IdMagazine == IdMagazineSelected.ToInt());
            int numeroCopieInserto = 0;
            if (MagazineSelected != null)
                numeroCopieInserto = MagazineSelected.NumeroCopieTotale - MagazineSelected.NumeroCopieVendute -
                    MagazineSelected.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieInserto + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private void UpdateTotalPrice()
        {
            lblTotalPrice.Content = MagazineAvailable.Where(p => MagazineSoldList.Keys.Contains(p.IdMagazine))
                .Sum(p => p.Prezzo * MagazineSoldList[p.IdMagazine]);
        }
    }
}
