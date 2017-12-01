using EdicolaManager.Models;
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
            InitializeComponent();
            magazine = new MagazineModel(_connection);
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
            PeriodicoWindow cp = new PeriodicoWindow();
            cp.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ReloadMagazineGrid();
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
            ReloadDashboardFromFilters();
        }
        
        private void ReloadDashboardFromFilters()
        {
            var MagazineOverview = magazine.GetMagazineOverview();
            if (!string.IsNullOrEmpty(GetTxtIssn()))
                MagazineOverview = MagazineOverview.Where(p => p.ISSN.Contains(GetTxtIssn())).ToList();
            if (!string.IsNullOrEmpty(GetTxtPeriodico()))
                MagazineOverview = MagazineOverview.Where(p => p.Periodico.ToLower().Contains(GetTxtPeriodico().ToLower())).ToList();
            if (!string.IsNullOrEmpty(GetTxtRivista()))
                MagazineOverview = MagazineOverview.Where(p => p.Rivista.ToLower().Contains(GetTxtRivista().ToLower())).ToList();
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
    }
}
