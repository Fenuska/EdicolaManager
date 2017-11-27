using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public class MagazineModel
    {
        protected static DBLinqDataContext _connection;
        public string Nome;
        public int IdTipologia;
        public int IdPeriodico;
        public DateTime DataDiConsegna;
        public DateTime DataDiReso;
        public int IdMagazine;
        public int Numero;
        public decimal Prezzo;
        public int NumeroCopieTotale;
        public int NumeroCopieRese;
        public int NumeroCopieVendute;
        public string ISSN;


        public MagazineModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public IQueryable<Magazine> GetMagazine()
        {
            return _connection.Magazines;
        }

        public void CreateMagazine()
        {
            try
            {
                var magazine = new Magazine()
                {
                    Nome = this.Nome,
                    IdTipologia = IdTipologia,
                    IdPeriodico = IdPeriodico,
                    DataDiConsegna = DataDiConsegna,
                    DataDiReso = DataDiReso,
                    IdMagazine = IdMagazine,
                    Numero = Numero,
                    NumeroCopieRese = 0,
                    NumeroCopieTotale = this.NumeroCopieTotale,
                    NumeroCopieVendute = 0,
                    Prezzo = Prezzo,
                    ISSN = ISSN
                };

                _connection.Magazines.InsertOnSubmit(magazine);
                _connection.SubmitChanges();
                IdMagazine = magazine.IdMagazine;
            }
            catch { }
        }

        public void UpdateMagazine()
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

        public List<Dashboard> GetMagazineOverview()
        {
            return _connection.Dashboards.ToList();
        }
    }
}

