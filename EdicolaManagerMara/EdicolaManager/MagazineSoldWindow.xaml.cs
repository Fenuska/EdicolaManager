using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EdicolaManager
{
    public partial class MagazineSoldWindow : Window
    {
        private Dictionary<int, int> MagazineSoldList = new Dictionary<int, int>();
        private string ISBN = string.Empty;

        public MagazineSoldWindow()
        {
            InitializeComponent();
            cbInserto.ItemsSource = Magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
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
                Magazine mag = Magazine.GetMagazine().FirstOrDefault(p => p.IdMagazine == MagazineItem);
                SaveMagazineChanges(mag);
            }
            this.Close();
        }

        private void LoadMagazineList()
        {
            cbInserto.ItemsSource = Magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();

            gridMagazineSold.ItemsSource = Magazine.GetMagazine().ToList().Where(p => MagazineSoldList.ContainsKey(p.IdMagazine)).ToList();
        }

        private void UpdateMagazine()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";

            Magazine Magazine = Magazine.GetMagazine().FirstOrDefault(p => p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected));
            if (Magazine != null)
            {
                Magazine.NumeroCopieVendute += ConvertStringIntoInt(cbNumeroCopie.SelectedValue.ToString());
                if (!MagazineSoldList.ContainsKey(Magazine.IdMagazine))
                    MagazineSoldList.Add(Magazine.IdMagazine, ConvertStringIntoInt(cbNumeroCopie.SelectedValue.ToString()));
                else
                    MagazineSoldList[Magazine.IdMagazine] += ConvertStringIntoInt(cbNumeroCopie.SelectedValue.ToString());
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void SaveMagazineChanges(Magazine Magazine)
        {
            Magazine.updateMagazine();
            AddMagazineIntoHistory(Magazine);
        }

        private void AddMagazineIntoHistory(Magazine Magazine)
        {
            Cronologia history = new Cronologia();
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
            var MagazineSelected = Magazine.GetMagazine().FirstOrDefault(p =>
                   p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected));
            int numeroCopieInserto = 0;
            if (MagazineSelected != null)
                numeroCopieInserto = MagazineSelected.NumeroCopieTotale - MagazineSelected.NumeroCopieVendute -
                    MagazineSelected.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieInserto + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private int ConvertStringIntoInt(string value)
        {
            int result = -1;

            int.TryParse(value, out result);

            return result;
        }

        private void UpdateTotalPrice()
        {
            var MagazineSoldListIds = MagazineSoldList.Select(p => p.Key).ToList();
            lblTotalPrice.Content = Magazine.GetMagazine().ToList().Where(p => MagazineSoldListIds.Contains(p.IdMagazine))
                .Sum(p => p.Prezzo * MagazineSoldList[p.IdMagazine]);
        }

        private void MagazineSoldWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var magazine = Magazine.GetMagazine().FirstOrDefault(p => p.ISSN == ISBN);
                //cbInserto.SelectedItem = magazine.IdMagazine;
                cbInserto.SelectedItem = magazine;
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
