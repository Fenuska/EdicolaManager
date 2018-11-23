using EdicolaManager.Models;
using Extensions.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for Inserto.xaml
    /// </summary>
    public partial class MagazineWindow : Window
    {
        int IdPeriodico;
        IEnumerable<Tipologia> TipologiaList;
        readonly DBLinqDataContext _connection = new DBLinqDataContext();
        TipologiaModel tipologia;
        private BarcodeScanner scanner;
        private IEnumerable<Periodico> listaPeriodici;

        public MagazineWindow()
        {
            try
            {
                InitializeComponent();
                tipologia = new TipologiaModel(_connection);
                GetListaTipologie();
                SetDefaultDateToDatePicker();
                scanner = new BarcodeScanner();
                listaPeriodici = new PeriodicoModel(_connection).Get();
            }
            catch (Exception)
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel caricamento della pagina");
            }

        }

        public MagazineWindow(int IdPeriodico)
        {
            try
            {
                InitializeComponent();
                tipologia = new TipologiaModel(_connection);
                SetPeriodico(IdPeriodico);
                GetListaTipologie();
                scanner = new BarcodeScanner();
                listaPeriodici = new PeriodicoModel(_connection).Get();
            }
            catch (Exception)
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore nel caricamento della pagina");
            }
        }

        private void btnInserto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateMagazine();
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore durante la creazione della rivista");
            }
        }

        private void txtISSN_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                scanner.Read(e);
                if (e.Key == Key.Return)
                {
                    var magazine = new MagazineModel(_connection).Get().OrderByDescending(p => p.Numero)
                        .FirstOrDefault(p => p.ISSN == scanner.resultCode);
                    if (magazine != null)
                    {
                        cbTipologia.SelectedValue = magazine.IdTipologia;
                        txtNomePeriodico.Text = listaPeriodici.FirstOrDefault(p => p.IdPeriodico == magazine.IdPeriodico)?.Nome ?? string.Empty;
                        txtNomeRivista.Text = magazine.Nome;
                        txtNumero.Text = (magazine.Numero + 1)?.ToString() ?? string.Empty;
                        txtPrezzo.Text = magazine.Prezzo.ToString();
                    }
                    scanner.resultCode = string.Empty;
                }
            }
            catch
            {
                ///TODO: to be implemented
                MessageBox.Show("Errore durante la lettura del codice a barre");
            }
        }

        private void SetDefaultDateToDatePicker()
        {
            dtDataDiConsegna.SelectedDate = DateTime.Today;
        }

        private void SetDefaultDateToReturnDatePicker()
        {
            dtDataDiReso.SelectedDate = DateTime.Today;
        }

        private void SetPeriodico(int idPeriodico)
        {
            this.IdPeriodico = idPeriodico;
        }

        private void GetListaTipologie()
        {
            TipologiaList = tipologia.Get().ToList();
            cbTipologia.ItemsSource = TipologiaList;
        }

        private void CreateMagazine()
        {
            if (ValidateFields())
            {
                var magazine = new MagazineModel(_connection);
                magazine.IdPeriodico = CreatePeriodic().IdPeriodico;
                magazine.IdTipologia = cbTipologia.SelectedValue.ToInt();
                magazine.DataDiConsegna = GetDateFromDatePicker(dtDataDiConsegna);
                magazine.DataDiReso = SetDataDiReso(dtDataDiReso, magazine.IdTipologia);
                magazine.Nome = NormalizeString(GetNomeRivista());
                magazine.Numero = NormalizeString(GetNumero()).ToInt();
                magazine.NumeroCopieTotale = NormalizeString(GetQuantita()).ToInt();
                magazine.Prezzo = NormalizeString(GetPrezzo()).ToDecimal();
                magazine.ISSN = GetISSN();

                magazine.Create();

                CloseWindow();
            }
            else
                MessageBox.Show("Alcuni campi non sono corretti. Impossibile aggiungere l'inserto");
        }

        private Periodico CreatePeriodic()
        {
            var nomePeriodico = NormalizeString(GetNomePeriodico());
            if (!listaPeriodici.Any(p => p.Nome == nomePeriodico))
            {
                var periodic = new PeriodicoModel(_connection);
                periodic.Nome = NormalizeString(GetNomePeriodico());

                periodic.Create();

                return new Periodico { IdPeriodico = periodic.IdPeriodico, Nome = periodic.Nome };
            }
            return listaPeriodici.FirstOrDefault(p => p.Nome == nomePeriodico);
        }

        private bool ValidateFields()
        {
            bool result = true;
            result &= !string.IsNullOrEmpty(cbTipologia.SelectedValue?.ToString());
            result &= !string.IsNullOrEmpty(GetNomeRivista());
            result &= !string.IsNullOrEmpty(GetNomePeriodico());
            result &= decimal.TryParse(GetPrezzo(), out decimal prezzo);
            result &= int.TryParse(GetNumero(), out int numeroCopie);
            result &= int.TryParse(GetQuantita(), out numeroCopie);

            Regex regex = new Regex(@"^\d+$");
            result &= regex.IsMatch(GetISSN());

            return result;
        }

        private DateTime GetDateFromDatePicker(DatePicker dp)
        {
            DateTime result = DateTime.Now;
            if (dp.SelectedDate.HasValue)
                result = dp.SelectedDate.Value;
            return result;
        }

        private DateTime SetDataDiReso(DatePicker dp, int IdTipologia)
        {
            DateTime result = DateTime.Now;
            if (dp.SelectedDate.HasValue)
                result = dp.SelectedDate.Value;
            else
                result = UpdateDateFromTipologia(IdTipologia, result);
            return result;
        }

        private DateTime UpdateDateFromTipologia(int IdTipologia, DateTime result)
        {
            var tipologiaSelezionata = TipologiaList.FirstOrDefault(p => p.IdTipologia == IdTipologia);

            result = result.AddDays(tipologiaSelezionata?.Giorni?.ToInt() ?? 0);
            result = result.AddMonths(tipologiaSelezionata?.Mesi?.ToInt() ?? 0);
            return result;
        }

        private string NormalizeString(string value)
        {
            return value = value?.Replace('.', ',');
        }

        private void CloseWindow()
        {
            Close();
        }

        private string GetPrezzo()
        {
            return txtPrezzo.Text?.Trim();
        }

        private string GetQuantita()
        {
            return txtQuantita.Text?.Trim();
        }

        private string GetNomeRivista()
        {
            return txtNomeRivista.Text?.Trim();
        }

        private string GetNomePeriodico()
        {
            return txtNomePeriodico.Text?.Trim();
        }

        private string GetNumero()
        {
            return txtNumero.Text?.Trim();
        }

        private string GetISSN()
        {
            return txtISSN.Text?.Trim();
        }

        private void cbTipologia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtDataDiReso.SelectedDate = null;
            dtDataDiReso.SelectedDate = SetDataDiReso(dtDataDiReso, ((Tipologia)(cbTipologia.SelectedItem))?.IdTipologia ?? 0);
        }
    }
}
