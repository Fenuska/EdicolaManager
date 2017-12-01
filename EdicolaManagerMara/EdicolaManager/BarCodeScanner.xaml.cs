using System.Text;
using System.Windows.Input;

namespace EdicolaManager
{
    public class BarcodeScanner
    {
       public string resultCode = string.Empty;

        public void Read(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
                resultCode += e.Key.ToString().Remove(0,1);
        }
    }
}