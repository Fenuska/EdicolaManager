using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager.Models
{
    public class MagazineModel : IModel<Magazine>
    {
        protected static DBLinqDataContext _connection;
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

        protected MagazineModel()
        {

        }

        public MagazineModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public IQueryable<Magazine> Get()
        {
            return _connection.Magazines;
        }

        public List<MagazineModel> GetAvailableMagazineList()
        {
            return Get().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale)
                .Select(p => new MagazineModel
                {
                    Nome = p.Nome,
                    IdTipologia = p.IdTipologia,
                    IdPeriodico = p.IdPeriodico,
                    DataDiConsegna = p.DataDiConsegna,
                    DataDiReso = p.DataDiReso,
                    IdMagazine = p.IdMagazine,
                    ISSN = p.ISSN,
                    Numero = p.Numero,
                    NumeroCopieRese = p.NumeroCopieRese,
                    NumeroCopieTotale = p.NumeroCopieTotale,
                    NumeroCopieVendute = p.NumeroCopieVendute,
                    Prezzo = p.Prezzo
                }).ToList();
        }

        public bool Create()
        {
            bool result = false;
            try
            {
                var magazine = new Magazine()
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

                _connection.Magazines.InsertOnSubmit(magazine);
                _connection.SubmitChanges();
                IdMagazine = magazine.IdMagazine;
                result = true;
            }
            catch
            {
                IdMagazine = 0;
            }
            return result;
        }

        public bool Update()
        {
            bool result;
            try
            {
                var magazine = Get().FirstOrDefault(p => p.IdMagazine == IdMagazine);

                magazine.DataDiConsegna = DataDiConsegna;
                magazine.DataDiReso = DataDiReso;
                magazine.IdPeriodico = IdPeriodico;
                magazine.IdTipologia = IdTipologia;
                magazine.ISSN = ISSN;
                magazine.Nome = Nome;
                magazine.Numero = Numero;
                magazine.NumeroCopieRese = NumeroCopieRese;
                magazine.NumeroCopieTotale = NumeroCopieTotale;
                magazine.NumeroCopieVendute = NumeroCopieVendute;

                _connection.SubmitChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public int GetAmountAvailable()
        {
            return NumeroCopieTotale - NumeroCopieVendute - NumeroCopieRese;
        }
    }
}

