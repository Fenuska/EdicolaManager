using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public partial class Magazine
    {
        public static List<Magazine> getMagazineList()
        {
            List<Magazine> lista = new List<Magazine>();

            using (DBLinqDataContext ctx = new DBLinqDataContext())
            {
                lista = ctx.Magazines.ToList();
            }
            return lista;
        }

        public void createMagazine()
        {
            try
            {
                var temp = new Magazine()
                {
                    Nome = this.Nome,
                    IdTipologia = this.IdTipologia,
                    IdPeriodico = this.IdPeriodico,
                    DataDiConsegna = this.DataDiConsegna,
                    DataDiReso = this.DataDiReso,
                    IdMagazine = this.IdMagazine,
                    Numero = this.Numero,
                    NumeroCopieRese = 0,
                    NumeroCopieTotale = this.NumeroCopieTotale,
                    NumeroCopieVendute = 0,
                    Prezzo = this.Prezzo,
                    ISSN = this.ISSN
                };

                using (DBLinqDataContext ctx = new DBLinqDataContext())
                {
                    ctx.Magazines.InsertOnSubmit(temp);
                    ctx.SubmitChanges();
                }
                this.IdMagazine = temp.IdMagazine;
            }
            catch { }
        }

        public void updateMagazine()
        {
            try
            {
                using (var ctx = new DBLinqDataContext())
                {
                    var temp = ctx.Magazines.Where(p => p.IdMagazine == this.IdMagazine).SingleOrDefault();
                    temp.Nome = this.Nome;
                    temp.NumeroCopieRese = this.NumeroCopieRese;
                    temp.NumeroCopieVendute = this.NumeroCopieVendute;
                    ctx.SubmitChanges();
                }
            }
            catch { }
        }
    }

}

