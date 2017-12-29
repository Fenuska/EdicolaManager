using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdicolaMVC.Models
{
    public class DashboardModel
    {
        public string Nome { get; set; }
        public int IdTipologia { get; set; }
        public int IdPeriodico { get; set; }
        public DateTime DataDiConsegna { get; set; }
        public DateTime DataDiReso { get; set; }
        public int IdMagazine { get; set; }
        public int? Numero { get; set; }
        public decimal Prezzo { get; set; }
        public int NumeroCopieTotale { get; set; }
        public int NumeroCopieRese { get; set; }
        public int NumeroCopieVendute { get; set; }
        public string ISSN { get; set; }
    }
}