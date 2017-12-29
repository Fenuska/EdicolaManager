using EdicolaManager.ExtensionMethods;
using EdicolaManager.Models;
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
        private int IdPeriodico;
        private List<Tipologia> TipologiaList;
        private readonly DBLinqDataContext _connection = new DBLinqDataContext();
        private TipologiaModel tipologia;
        private BarcodeScanner scanner;

        public MagazineWindow()
        {
            try
            {
                InitializeComponent();
                tipologia = new TipologiaModel(_connection);
                GetListaTipologie();
                SetDefaultDateToDatePicker();
                scanner = new BarcodeScanner();
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
                CreateInserto();
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
                    var magazine = new MagazineModel(_connection).GetMagazine().OrderByDescending(p => p.Numero)
                        .FirstOrDefault(p => p.ISSN == scanner.resultCode);
                    if (magazine != null)
                    {
                        cbTipologia.SelectedValue = magazine.IdTipologia;
                        txtNome.Text = magazine.Nome;
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

        private void SetPeriodico(int idPeriodico)
        {
            this.IdPeriodico = idPeriodico;
        }

        private void GetListaTipologie()
        {
            TipologiaList = tipologia.GetListaTipologia();
            cbTipologia.ItemsSource = TipologiaList;
        }

        private void CreateInserto()
        {
            if (ValidateFields())
            {
                var inserto = new MagazineModel(_connection);
                inserto.IdPeriodico = IdPeriodico;
                inserto.IdTipologia = NormalizeString(cbTipologia.SelectedValue.ToString()).ToInt();
                inserto.DataDiConsegna = GetDateFromDatePicker(dtDataDiConsegna);
                inserto.DataDiReso = SetDataDiReso(dtDataDiReso, inserto.IdTipologia);
                inserto.Nome = NormalizeString(GetNome());
                inserto.Numero = NormalizeString(GetNumero()).ToInt();
                inserto.NumeroCopieTotale = NormalizeString(GetQuantita()).ToInt();
                inserto.Prezzo = NormalizeString(GetPrezzo()).ToDecimal();
                inserto.ISSN = GetISSN();

                inserto.CreateMagazine();

                CloseWindow();
            }
            else
                MessageBox.Show("Alcuni campi non sono corretti. Impossibile aggiungere l'inserto");
        }

        private bool ValidateFields()
        {
            bool result = true;
            result &= !string.IsNullOrEmpty(cbTipologia.SelectedValue?.ToString());
            result &= !string.IsNullOrEmpty(GetNome());
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
            var tipologia = TipologiaList.Where(p => p.IdTipologia == IdTipologia).FirstOrDefault();
            if (tipologia?.Giorni != null)
                result = result.AddDays((int)tipologia.Giorni);
            if (tipologia?.Mesi != null)
                result = result.AddMonths((int)tipologia.Mesi);
            return result;
        }

        private string NormalizeString(string value)
        {
            return value = value?.Replace('.', ',');
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private string GetPrezzo()
        {
            return txtPrezzo.Text?.Trim();
        }

        private string GetQuantita()
        {
            return txtQuantita.Text?.Trim();
        }

        private string GetNome()
        {
            return txtNome.Text?.Trim();
        }

        private string GetNumero()
        {
            return txtNumero.Text?.Trim();
        }

        private string GetISSN()
        {
            return txtISSN.Text?.Trim();
        }


    }
}
