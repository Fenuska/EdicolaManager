using EdicolaManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private readonly MagazineModel magazine;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                magazine = new MagazineModel(_connection);
                ReloadMagazineGrid();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel caricamento della pagina");
            }
        }

        private void OpenMagazineSoldWindow()
        {
            MagazineSoldWindow window = new MagazineSoldWindow();
            window.Show();
        }

        private void OpenMagazineReturnedWindow()
        {
            MagazineReturnedWindow window = new MagazineReturnedWindow();
            window.Show();
        }

        private void OpenStatsWindow()
        {
            StatsWindow window = new StatsWindow();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPeriodico_Click(object sender, RoutedEventArgs e)
        {
            //PeriodicoWindow cp = new PeriodicoWindow();
            MagazineWindow mw = new MagazineWindow();
            mw.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReloadMagazineGrid();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel caricamento della griglia");
            }
        }

        private void btnInserto_Click(object sender, RoutedEventArgs e)
        {
            MagazineWindow window = new MagazineWindow();
            window.Show();
        }

        private void btnVendita_Click(object sender, RoutedEventArgs e)
        {
            OpenMagazineSoldWindow();
        }

        private void btnResi_Click(object sender, RoutedEventArgs e)
        {
            OpenMagazineReturnedWindow();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            OpenStatsWindow();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ReloadDashboardFromFilters();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel caricamento della griglia con i filtri selezionati");
            }
        }

        private void gridPeriodici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private void ReloadDashboardFromFilters()
        {
            IEnumerable<Dashboard> MagazineOverview = magazine.GetMagazineOverview();
            if (!string.IsNullOrEmpty(GetTxtIssn()))
                MagazineOverview = MagazineOverview.Where(p => p.ISSN.Contains(GetTxtIssn()));
            if (!string.IsNullOrEmpty(GetTxtPeriodico()))
                MagazineOverview = MagazineOverview.Where(p => p.Periodico.ToLower().Contains(GetTxtPeriodico().ToLower()));
            if (!string.IsNullOrEmpty(GetTxtRivista()))
                MagazineOverview = MagazineOverview.Where(p => p.Rivista.ToLower().Contains(GetTxtRivista().ToLower()));
            gridPeriodici.ItemsSource = MagazineOverview;
        }

        private void ReloadMagazineGrid()
        {
            gridPeriodici.ItemsSource = magazine.GetMagazineOverview();
        }

        private string GetTxtIssn()
        {
            return txtISSN?.Text?.Trim();
        }

        private string GetTxtPeriodico()
        {
            return txtPeriodicName?.Text?.Trim();
        }

        private string GetTxtRivista()
        {
            return txtRivista?.Text?.Trim();
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Autore: Luca Fenu");
        }
    }
}
