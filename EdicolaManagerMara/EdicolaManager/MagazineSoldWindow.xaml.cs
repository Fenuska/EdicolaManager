using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for SoldInsertoWindow.xaml
    /// </summary>
    public partial class MagazineSoldWindow : Window
    {
        private List<Magazine> MagazineList;
        private Dictionary<int, int> MagazineSoldList = new Dictionary<int, int>();

        public MagazineSoldWindow()
        {
            InitializeComponent();
            MagazineList = Magazine.getMagazineList().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
            cbInserto.ItemsSource = MagazineList;
        }

        private void LoadMagazineList()
        {
            MagazineList = MagazineList.Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
            cbInserto.ItemsSource = MagazineList;
            gridMagazineSold.ItemsSource = MagazineList.Where(p => MagazineSoldList.ContainsKey(p.IdMagazine)).ToList();
        }

        private void UpdateMagazine()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            Magazine Magazine = MagazineList.Where(p => p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected)).FirstOrDefault();
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

        private void AddMagazineIntoHistory(EdicolaManager.Magazine Magazine)
        {
            Cronologia history = new Cronologia();
            history.IdMagazine = Magazine.IdMagazine;
            history.IdPeriodico = Magazine.IdPeriodico;
            history.NumeroMagazineVenduti = MagazineSoldList[Magazine.IdMagazine];
            history.Data = DateTime.Now;
            history.CreateHistoryRecord();
        }

        private void cbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAmountOfCopiesAvailable();
        }

        private void UpdateAmountOfCopiesAvailable()
        {
            var IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            var Magazine = MagazineList.Where(p => p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected)).FirstOrDefault();
            int numeroCopieInserto = 0;
            if (Magazine != null)
                numeroCopieInserto = Magazine.NumeroCopieTotale - Magazine.NumeroCopieVendute - Magazine.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieInserto + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private int ConvertStringIntoInt(string value)
        {
            int result = -1;

            int.TryParse(value, out result);

            return result;
        }

        private void btnAddMagazine_Click(object sender, RoutedEventArgs e)
        {
            UpdateMagazine();
            LoadMagazineList();
            UpdateAmountOfCopiesAvailable();
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            lblTotalPrice.Content = MagazineList.Where(p=> MagazineSoldList.ContainsKey(p.IdMagazine)).Sum(p=> p.Prezzo * MagazineSoldList[p.IdMagazine]) ;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var MagazineItem in MagazineSoldList.Keys)
            {
                Magazine mag = MagazineList.Where(p => p.IdMagazine == MagazineItem).FirstOrDefault();
                SaveMagazineChanges(mag);
            }
            this.Close();
        }
    }
}
