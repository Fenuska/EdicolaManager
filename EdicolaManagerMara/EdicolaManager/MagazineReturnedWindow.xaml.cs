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
        private List<Magazine> MagazineList;
        
        public MagazineReturnedWindow()
        {
            InitializeComponent();
            GetListOfMagazine();
        }

        private void GetListOfMagazine()
        {
            MagazineList = Magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
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

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            UpdateMagazine();
            GetListOfMagazine();
            UpdateAmountOfCopies();
        }

        private void UpdateMagazine()
        {
            string IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            var Magazine = MagazineList.Where(p => p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected)).FirstOrDefault();
            if (Magazine != null)
            {
                Magazine.NumeroCopieRese += ConvertStringIntoInt(cbNumeroCopie.SelectedValue.ToString());
                Magazine.updateMagazine();
                AddMagazineIntoHistory(Magazine, cbNumeroCopie.SelectedValue.ToString());
            }
            else
                MessageBox.Show("Rivista inesistente");
        }

        private void cbInserto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAmountOfCopies();
        }

        private void UpdateAmountOfCopies()
        {
            string IdMagazineSelected = cbInserto.SelectedValue != null ? cbInserto.SelectedValue.ToString() : "0";
            var Magazine = MagazineList.Where(p => p.IdMagazine == ConvertStringIntoInt(IdMagazineSelected)).FirstOrDefault();
            int numeroCopieMagazine = 0;
            if (Magazine != null)
                numeroCopieMagazine = Magazine.NumeroCopieTotale - Magazine.NumeroCopieVendute - Magazine.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieMagazine + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private int ConvertStringIntoInt(string value)
        {
            int result = -1;

            int.TryParse(value, out result);

            return result;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMagazineIntoHistory(EdicolaManager.Magazine Magazine, string amountOfReturnedMagazine)
        {
            Cronologia history = new Cronologia();
            history.IdMagazine = Magazine.IdMagazine;
            history.IdPeriodico = Magazine.IdPeriodico;
            history.NumeroMagazineResi = ConvertStringIntoInt(amountOfReturnedMagazine) ;
            history.Data = DateTime.Now;
            history.CreateHistoryRecord();
        }
    }
}
