using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        private List<ViewHistory> HistoryList = null;

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

        private string GetMagazineName()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(txtMagazine.Text))
                result = txtMagazine.Text.Trim();
            return result;
        }

        public StatsWindow()
        {
            InitializeComponent();
            SetDefaultDateTimeOnDatePicker();
        }

        private void dtStartingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void SetDefaultDateTimeOnDatePicker()
        {
            dtEndingDate.SelectedDate = DateTime.Today;
            dtStartingDate.SelectedDate = DateTime.Today;
        }

        private void dtEndingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void txtISSN_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void LoadUpdatedHistoryRecordFromFilters()
        {
            List<ViewHistory> HistoryList = null;
            if (dtStartingDate.SelectedDate.HasValue && dtEndingDate.SelectedDate.HasValue)
            {
                HistoryList = Cronologia.GetHistoryBetweenDates(dtStartingDate.SelectedDate.Value,
                         dtEndingDate.SelectedDate.Value, 1000, 0);
                if (HistoryList != null && !string.IsNullOrEmpty(GetISSN()))
                    HistoryList = HistoryList.Where(p => p.ISSN.Contains(GetISSN())).ToList();

                if (HistoryList != null && !string.IsNullOrEmpty(GetPeriodicName()))
                    HistoryList = HistoryList.Where(p => p.Periodico.ToLower().Contains(GetPeriodicName().ToLower())).ToList();

                if (HistoryList != null && !string.IsNullOrEmpty(GetMagazineName()))
                    HistoryList = HistoryList.Where(p => p.Magazine.ToLower().Contains(GetMagazineName().ToLower())).ToList();

                gridHistory.ItemsSource = HistoryList;
            }
            if (HistoryList != null)
                lvlPrezzoTot.Content = HistoryList.Sum(p => p.Prezzo_Totale);
        }

        private void txtPeriodicName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void txtMagazine_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }


    }
}
