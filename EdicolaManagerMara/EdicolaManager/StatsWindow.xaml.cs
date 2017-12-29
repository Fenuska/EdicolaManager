using EdicolaManager.Models;
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
        private List<ViewHistory> historyList = null;
        private readonly DBLinqDataContext _connection;
        private readonly CronologiaModel cronologia;

        public StatsWindow()
        {
            InitializeComponent();

            _connection = new DBLinqDataContext();
            cronologia = new CronologiaModel(_connection);

            SetDefaultDateTimeOnDatePicker();
        }

        private void dtStartingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void dtEndingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void txtISSN_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void txtPeriodicName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
        }

        private void txtMagazine_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadUpdatedHistoryRecordFromFilters();
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

        private string GetMagazineName()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(txtMagazine.Text))
                result = txtMagazine.Text.Trim();
            return result;
        }

        private void SetDefaultDateTimeOnDatePicker()
        {
            dtEndingDate.SelectedDate = DateTime.Today;
            dtStartingDate.SelectedDate = DateTime.Today;
        }

        private void LoadUpdatedHistoryRecordFromFilters()
        {
            if (dtStartingDate.SelectedDate.HasValue && dtEndingDate.SelectedDate.HasValue)
            {
                historyList = cronologia.GetHistoryBetweenDates(dtStartingDate.SelectedDate.Value,
                         dtEndingDate.SelectedDate.Value, 1000, 0);
                if (historyList.Any())
                {
                    if (!string.IsNullOrEmpty(GetISSN()))
                        historyList = historyList.Where(p => p.ISSN.Contains(GetISSN())).ToList();

                    if (!string.IsNullOrEmpty(GetPeriodicName()))
                        historyList = historyList.Where(p => p.Periodico.ToLower().Contains(GetPeriodicName().ToLower())).ToList();

                    if (!string.IsNullOrEmpty(GetMagazineName()))
                        historyList = historyList.Where(p => p.Magazine.ToLower().Contains(GetMagazineName().ToLower())).ToList();
                }
                gridHistory.ItemsSource = historyList;
            }
            if (historyList != null)
                lvlPrezzoTot.Content = historyList.Sum(p => p.Prezzo_Totale);
        }

        private void gridHistory_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                if (e.PropertyType == typeof(System.DateTime))
                    (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy HH:mm:ss";
            }
            catch
            {
                ///TODO: to be implemented
            }
        }
    }
}
