using EdicolaManager.ExtensionMethods;
using EdicolaManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        public MagazineReturnedWindow()
        {
            InitializeComponent();
            cronologia = new CronologiaModel(_connection);
            magazine = new MagazineModel(_connection);
            GetListOfAvailableMagazine();
            UpdateComboboxMagazine();
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            UpdateMagazine();
            GetListOfAvailableMagazine();
            UpdateComboboxMagazine();
            GetAmountOfCopies();
        }

        private void cbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAmountOfCopies();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetListOfAvailableMagazine()
        {
            MagazineList = magazine.GetAvailableMagazineList();
        }

        private void UpdateComboboxMagazine()
        {
            cbInserto.ItemsSource = MagazineList.Select(p => new
            {
                p.ISSN,
                p.Nome,
                p.Numero,
                p.NumeroCopieTotale,
                p.NumeroCopieVendute,
                p.Prezzo,
                p.IdMagazine
            }).ToList();
        }

        private void UpdateMagazine()
        {
            FocusOnSelectedMagazine();
            if (magazine != null)
            {
                var numeroCopieRese = cbNumeroCopie.SelectedValue.ToInt();

                magazine.NumeroCopieRese += numeroCopieRese;
                magazine.UpdateMagazine();
                cronologia.TrackReturnedMagazine(magazine, numeroCopieRese);
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void GetAmountOfCopies()
        {
            FocusOnSelectedMagazine();
            int amountAvailableMagazine = 0;
            if (magazine != null)
                amountAvailableMagazine = magazine.NumeroCopieTotale - magazine.NumeroCopieVendute - magazine.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, amountAvailableMagazine + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private void FocusOnSelectedMagazine()
        {
            string IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            magazine = MagazineList.FirstOrDefault(p => p.IdMagazine == IdMagazineSelected.ToInt());
        }
    }
}
