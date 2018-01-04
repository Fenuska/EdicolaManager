using EdicolaManagerBL.Architecture.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager.Models
{
    public class RivistaDto
    {
        protected readonly RepositoryContext _connection;
        public string Nome { get; set; }
        public int IdTipologia { get; set; }
        public int IdPeriodico { get; set; }
        public DateTime DataDiConsegna { get; set; }
        public DateTime DataDiReso { get; set; }
        public int IdRivista { get; set; }
        public int? Numero { get; set; }
        public decimal Prezzo { get; set; }
        public int NumeroCopieTotale { get; set; }
        public int NumeroCopieRese { get; set; }
        public int NumeroCopieVendute { get; set; }
        public string ISSN { get; set; }

        protected RivistaDto()
        {

        }

        public RivistaDto(RepositoryContext connection)
        {
            _connection = connection;
        }

        public IQueryable<RivistaDto> GetMagazine()
        {
            return _connection.Magazine;
        }

        public List<RivistaDto> GetAvailableMagazineList()
        {
            return GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale)
                .ToList();
        }

        public void CreateMagazine()
        {
            try
            {
                var magazine = new RivistaDto()
                {
                    Nome = Nome,
                    IdTipologia = IdTipologia,
                    IdPeriodico = IdPeriodico,
                    DataDiConsegna = DataDiConsegna,
                    DataDiReso = DataDiReso,
                    IdRivista = IdRivista,
                    Numero = Numero,
                    NumeroCopieRese = 0,
                    NumeroCopieTotale = NumeroCopieTotale,
                    NumeroCopieVendute = 0,
                    Prezzo = Prezzo,
                    ISSN = ISSN
                };

                _connection.Magazine.Add(magazine);
                _connection.SaveChanges();
                IdRivista = magazine.IdRivista;
            }
            catch { }
        }

        public void UpdateMagazine()
        {
            try
            {
                var magazine = GetMagazine().FirstOrDefault(p => p.IdRivista == IdRivista);

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

                _connection.SaveChanges();
            }
            catch { }
        }

        //public List<Dashboard> GetMagazineOverview()
        //{
        //    return _connection.Dashboards.Where(p => p.Copie_presenti > 0).ToList();
        //}
    }
}

