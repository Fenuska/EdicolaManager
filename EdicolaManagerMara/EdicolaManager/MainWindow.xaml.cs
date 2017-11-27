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
            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);
            this.PreviewKeyUp += new KeyEventHandler(MainWindow_PreviewKeyUp);
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

        private void txtISSN_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadDashboardFromFilters();
        }

        private void ReloadDashboardFromFilters()
        {
            var MagazineOverview = magazine.GetMagazineOverview();
            if (!string.IsNullOrEmpty(GetISSN()))
                MagazineOverview = MagazineOverview.Where(p => p.ISSN.Contains(GetISSN())).ToList();
            if (!string.IsNullOrEmpty(GetPeriodicName()))
                MagazineOverview = MagazineOverview.Where(p => p.Periodico.ToLower().Contains(GetPeriodicName().ToLower())).ToList();
            gridPeriodici.ItemsSource = MagazineOverview;
        }

        private void ReloadMagazineGrid()
        {
            gridPeriodici.ItemsSource = magazine.GetMagazineOverview();
        }

        private string GetISSN()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(txtISSN.Text))
                result = txtISSN.Text.Trim();
            return result;
        }

        private string GetPeriodicName()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(txtPeriodicName.Text))
                result = txtPeriodicName.Text.Trim();
            return result;
        }
    }
}
