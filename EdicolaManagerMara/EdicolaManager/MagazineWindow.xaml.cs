using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for Inserto.xaml
    /// </summary>
    public partial class MagazineWindow : Window
    {
        private int IdPeriodico;
        private List<Tipologia> TipologiaList;

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

        public MagazineWindow()
        {
            InitializeComponent();
            GetListaTipologie();
            SetDefaultDateToDatePicker();
        }

        private void SetDefaultDateToDatePicker()
        {
            dtDataDiConsegna.SelectedDate = DateTime.Today;
        }

        public MagazineWindow(int IdPeriodico)
        {
            InitializeComponent();
            SetPeriodico(IdPeriodico);
            GetListaTipologie();
        }

        private void SetPeriodico(int IdPeriodico)
        {
            this.IdPeriodico = IdPeriodico;
        }

        private void GetListaTipologie()
        {
            TipologiaList = Tipologia.getListaTipologia();
            cbTipologia.ItemsSource = TipologiaList;
        }

        private void btnInserto_Click(object sender, RoutedEventArgs e)
        {
            CreateInserto();
        }

        private void CreateInserto()
        {
            if (validateFields())
            {
                Magazine inserto = new Magazine();
                inserto.IdPeriodico = this.IdPeriodico;
                inserto.IdTipologia = ConvertStringIntoInt(NormalizeString(cbTipologia.SelectedValue.ToString()));
                inserto.DataDiConsegna = GetDateFromDatePicker(dtDataDiConsegna);
                inserto.DataDiReso = SetDataDiReso(dtDataDiReso, inserto.IdTipologia);
                inserto.Nome = NormalizeString(GetNome());
                inserto.Numero = ConvertStringIntoInt(NormalizeString(GetNumero()));
                inserto.NumeroCopieTotale = ConvertStringIntoInt(NormalizeString(GetQuantita()));
                inserto.Prezzo = ConvertStringIntoDecimal(NormalizeString(GetPrezzo()));
                inserto.ISSN = GetISSN();

                inserto.CreateMagazine();

                closeWindow();
            }
            else
                MessageBox.Show("Alcuni campi non sono corretti. Impossibile aggiungere l'inserto");
        }

        private bool validateFields()
        {
            bool result = true;
            decimal prezzo = 0;
            int numeroCopie = 0;
            result &= cbTipologia.SelectedValue != null && !string.IsNullOrEmpty(cbTipologia.SelectedValue.ToString());
            result &= !string.IsNullOrEmpty(GetNome());
            result &= decimal.TryParse(GetPrezzo(), out prezzo);
            result &= int.TryParse(GetNumero(), out numeroCopie);
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
            if (tipologia != null && tipologia.Giorni != null)
                result = result.AddDays((int)tipologia.Giorni);
            if (tipologia != null && tipologia.Mesi != null)
                result = result.AddMonths((int)tipologia.Mesi);
            return result;
        }

        private string NormalizeString(string value)
        {
            return value = value?.Replace('.', ',');
        }

        private int ConvertStringIntoInt(string value)
        {
            int result = -1;

            int.TryParse(value, out result);

            return result;
        }

        private decimal ConvertStringIntoDecimal(string value)
        {
            decimal result = 0;

            decimal.TryParse(value, out result);

            return result;
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
