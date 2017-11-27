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
        private string ISBN = string.Empty;
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private MagazineModel magazine;

        public MagazineSoldWindow()
        {
            InitializeComponent();
            magazine = new MagazineModel(_connection);
            cbInserto.ItemsSource = magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
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
            foreach (var MagazineItem in MagazineSoldList.Keys)
            {
                Magazine mag = magazine.GetMagazine().FirstOrDefault(p => p.IdMagazine == MagazineItem);
                SaveMagazineChanges(mag);
            }
            this.Close();
        }

        private void LoadMagazineList()
        {
            cbInserto.ItemsSource = magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();

            gridMagazineSold.ItemsSource = magazine.GetMagazine().ToList().Where(p => MagazineSoldList.ContainsKey(p.IdMagazine)).ToList();
        }

        private void UpdateMagazine()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";

            Magazine Magazine = magazine.GetMagazine().FirstOrDefault(p => p.IdMagazine == IdMagazineSelected.ToInt());
            if (Magazine != null)
            {
                Magazine.NumeroCopieVendute += cbNumeroCopie.SelectedValue.ToString().ToInt();
                if (!MagazineSoldList.ContainsKey(Magazine.IdMagazine))
                    MagazineSoldList.Add(Magazine.IdMagazine, cbNumeroCopie.SelectedValue.ToString().ToInt());
                else
                    MagazineSoldList[Magazine.IdMagazine] += cbNumeroCopie.SelectedValue.ToString().ToInt();
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void SaveMagazineChanges(Magazine Magazine)
        {
            magazine.UpdateMagazine();
            AddMagazineIntoHistory(Magazine);
        }

        private void AddMagazineIntoHistory(Magazine Magazine)
        {
            var history = new CronologiaModel(_connection);
            history.IdMagazine = Magazine.IdMagazine;
            history.IdPeriodico = Magazine.IdPeriodico;
            history.NumeroMagazineVenduti = MagazineSoldList[Magazine.IdMagazine];
            history.Data = DateTime.Now;
            history.CreateHistoryRecord();
        }

        private void CbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAmountOfCopiesAvailable();
        }

        private void UpdateAmountOfCopiesAvailable()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            var MagazineSelected = magazine.GetMagazine().FirstOrDefault(p =>
                   p.IdMagazine == IdMagazineSelected.ToInt());
            int numeroCopieInserto = 0;
            if (MagazineSelected != null)
                numeroCopieInserto = MagazineSelected.NumeroCopieTotale - MagazineSelected.NumeroCopieVendute -
                    MagazineSelected.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieInserto + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private void UpdateTotalPrice()
        {
            var MagazineSoldListIds = MagazineSoldList.Select(p => p.Key).ToList();
            lblTotalPrice.Content = magazine.GetMagazine().ToList().Where(p => MagazineSoldListIds.Contains(p.IdMagazine))
                .Sum(p => p.Prezzo * MagazineSoldList[p.IdMagazine]);
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
    }
}
