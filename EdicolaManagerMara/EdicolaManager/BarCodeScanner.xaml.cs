using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EdicolaManager
{
    public partial class MainWindow : Window
    {
        string resultCode = string.Empty;
        StringBuilder mScanData = new StringBuilder();
        KeyConverter mScanKeyConverter = new KeyConverter();

        void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                resultCode = string.Empty;
            else if (e.Key > Key.D0 || e.Key < Key.D9)
                resultCode += e.Key.ToString().Remove(0,1);
        }
    }
}