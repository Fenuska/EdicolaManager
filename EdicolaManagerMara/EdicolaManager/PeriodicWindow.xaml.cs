using EdicolaManager.Models;
using System.Windows;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for createPeriodic.xaml
    /// </summary>
    public partial class PeriodicoWindow : Window
    {
        private readonly DBLinqDataContext _connection;
        private readonly PeriodicoModel periodico;

        public PeriodicoWindow()
        {
            InitializeComponent();
            _connection = new DBLinqDataContext();
            periodico = new PeriodicoModel(_connection);
            GetListaPeriodici();
        }

        private void GetListaPeriodici()
        {
            cbPeriodico.ItemsSource = periodico.GetListaPeriodici();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPeriodico();
        }

        private void AddPeriodico()
        {
            if (!string.IsNullOrEmpty(cbPeriodico.Text.Trim()))
            {
                int IdPeriodico = 0;
                if (cbPeriodico.SelectedValue != null)
                {
                    IdPeriodico = int.Parse(cbPeriodico.SelectedValue.ToString());
                }
                else
                {
                    var per = new PeriodicoModel(_connection)
                    {
                        Nome = cbPeriodico.Text.Trim()
                    };

                    per.CreatePeriodico();
                    IdPeriodico = per.IdPeriodico;
                }
                OpenInsertoWindow(IdPeriodico);
                CloseWindow();
            }
            else
                MessageBox.Show("Il nome non è valido");
        }

        private static void OpenInsertoWindow(int IdPeriodico)
        {
            MagazineWindow insWindow = new MagazineWindow(IdPeriodico);
            insWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void CloseWindow()
        {
            this.Close();
        }
    }
}
