using EdicolaManager.ExtensionMethods;
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
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private MagazineModel magazine;
        public MagazineReturnedWindow()
        {
            InitializeComponent();
            magazine = new MagazineModel(_connection);
            GetListOfMagazine();
        }

        private void GetListOfMagazine()
        {
            MagazineList = magazine.GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale).ToList();
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
            var Magazine = MagazineList.Where(p => p.IdMagazine == IdMagazineSelected.ToInt()).FirstOrDefault();
            if (Magazine != null)
            {
                Magazine.NumeroCopieRese += cbNumeroCopie.SelectedValue.ToString().ToInt();
                magazine.UpdateMagazine();
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
            var Magazine = MagazineList.Where(p => p.IdMagazine == IdMagazineSelected.ToInt()).FirstOrDefault();
            int numeroCopieMagazine = 0;
            if (Magazine != null)
                numeroCopieMagazine = Magazine.NumeroCopieTotale - Magazine.NumeroCopieVendute - Magazine.NumeroCopieRese;
            var numeroCopie = Enumerable.Range(0, numeroCopieMagazine + 1);
            cbNumeroCopie.ItemsSource = numeroCopie;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMagazineIntoHistory(Magazine Magazine, string amountOfReturnedMagazine)
        {
            var history = new CronologiaModel(_connection)
            {
                IdMagazine = Magazine.IdMagazine,
                IdPeriodico = Magazine.IdPeriodico,
                NumeroMagazineResi = amountOfReturnedMagazine.ToInt(),
                Data = DateTime.Now
            };
            history.CreateHistoryRecord();
        }
    }
}
