using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlaceholderTextBox;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public MainWindow()
        {
            InitializeComponent();
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

        private void ReloadMagazineGrid()
        {
            gridPeriodici.ItemsSource = Magazine.GetMagazineOverview();
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

        private void OpenMagazineSoldWindow()
        {
            MagazineSoldWindow window = new MagazineSoldWindow();
            window.Show();
        }

        private void btnResi_Click(object sender, RoutedEventArgs e)
        {
            OpenMagazineReturnedWindow();
        }

        private void OpenMagazineReturnedWindow()
        {
            MagazineReturnedWindow window = new MagazineReturnedWindow();
            window.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            OpenStatsWindow();
        }

        private void OpenStatsWindow()
        {
            StatsWindow window = new StatsWindow();
            window.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadDashboardFromFilters();
        }

        private void ReloadDashboardFromFilters()
        {
            var MagazineOverview = Magazine.GetMagazineOverview();
            if (!string.IsNullOrEmpty(GetISSN()))
                MagazineOverview = MagazineOverview.Where(p => p.ISSN.Contains(GetISSN())).ToList();
            if (!string.IsNullOrEmpty(GetPeriodicName()))
                MagazineOverview = MagazineOverview.Where(p => p.Periodico.ToLower().Contains(GetPeriodicName().ToLower())).ToList();
            gridPeriodici.ItemsSource = MagazineOverview;
        }

        private void txtISSN_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadDashboardFromFilters();
        }
    }
}
