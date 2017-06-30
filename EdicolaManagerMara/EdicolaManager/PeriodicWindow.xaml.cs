using System.Windows;

namespace EdicolaManager
{
    /// <summary>
    /// Interaction logic for createPeriodic.xaml
    /// </summary>
    public partial class PeriodicoWindow : Window
    {
        public PeriodicoWindow()
        {
            InitializeComponent();
            GetListaPeriodici();
        }

        private void GetListaPeriodici()
        {
            cbPeriodico.ItemsSource = Periodico.getListaPeriodici();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addPeriodico();
        }

        private void addPeriodico()
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
                    Periodico per = new Periodico();
                    per.Nome = cbPeriodico.Text.Trim();
                    per.createPeriodico();
                    IdPeriodico = per.IdPeriodico;
                }
                OpenInsertoWindow(IdPeriodico);
                closeWindow();
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

        private void closeWindow()
        {
            this.Close();
        }
    }
}
