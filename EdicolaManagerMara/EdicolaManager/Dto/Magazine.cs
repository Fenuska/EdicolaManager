using System.Linq;

namespace EdicolaManager
{
    public partial class Magazine
    {
        protected static DBLinqDataContext _connection = new DBLinqDataContext();

        public static IQueryable<Magazine> GetMagazine()
        {
            return _connection.Magazines;
        }

        public void CreateMagazine()
        {
            try
            {
                var temp = new Magazine()
                {
                    Nome = Nome,
                    IdTipologia = IdTipologia,
                    IdPeriodico = IdPeriodico,
                    DataDiConsegna = DataDiConsegna,
                    DataDiReso = DataDiReso,
                    IdMagazine = IdMagazine,
                    Numero = Numero,
                    NumeroCopieRese = 0,
                    NumeroCopieTotale = NumeroCopieTotale,
                    NumeroCopieVendute = 0,
                    Prezzo = Prezzo,
                    ISSN = ISSN
                };

                _connection.Magazines.InsertOnSubmit(temp);
                _connection.SubmitChanges();
                IdMagazine = temp.IdMagazine;
            }
            catch { }
        }

        public void updateMagazine()
        {
            try
            {
                var temp = GetMagazine().Where(p => p.IdMagazine == this.IdMagazine).SingleOrDefault();
                temp.Nome = this.Nome;
                temp.NumeroCopieRese = this.NumeroCopieRese;
                temp.NumeroCopieVendute = this.NumeroCopieVendute;
                _connection.SubmitChanges();
            }
            catch { }
        }
    }
}

