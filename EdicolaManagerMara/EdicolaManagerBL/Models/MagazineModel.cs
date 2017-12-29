using EdicolaManagerBL.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager.Models
{
    //public class MagazineModel
    //{
    //    protected readonly IDatabase _connection;
    //    public string Nome { get; set; }
    //    public int IdTipologia { get; set; }
    //    public int IdPeriodico { get; set; }
    //    public DateTime DataDiConsegna { get; set; }
    //    public DateTime DataDiReso { get; set; }
    //    public int IdMagazine { get; set; }
    //    public int? Numero { get; set; }
    //    public decimal Prezzo { get; set; }
    //    public int NumeroCopieTotale { get; set; }
    //    public int NumeroCopieRese { get; set; }
    //    public int NumeroCopieVendute { get; set; }
    //    public string ISSN { get; set; }

    //    protected MagazineModel()
    //    {

    //    }

    //    public MagazineModel(IDatabase connection)
    //    {
    //        _connection = connection;
    //    }

    //    //public IQueryable<Magazine> GetMagazine()
    //    //{
    //    //    return _connection.Magazines;
    //    //}

    //    public List<MagazineModel> GetAvailableMagazineList()
    //    {
    //        return GetMagazine().Where(p => p.NumeroCopieRese + p.NumeroCopieVendute < p.NumeroCopieTotale)
    //            .Select(p => new MagazineModel
    //            {
    //                Nome = p.Nome,
    //                IdTipologia = p.IdTipologia,
    //                IdPeriodico = p.IdPeriodico,
    //                DataDiConsegna = p.DataDiConsegna,
    //                DataDiReso = p.DataDiReso,
    //                IdMagazine = p.IdMagazine,
    //                ISSN = p.ISSN,
    //                Numero = p.Numero,
    //                NumeroCopieRese = p.NumeroCopieRese,
    //                NumeroCopieTotale = p.NumeroCopieTotale,
    //                NumeroCopieVendute = p.NumeroCopieVendute,
    //                Prezzo = p.Prezzo
    //            }).ToList();
    //    }

    //    public void CreateMagazine()
    //    {
    //        try
    //        {
    //            var magazine = new Magazine()
    //            {
    //                Nome = Nome,
    //                IdTipologia = IdTipologia,
    //                IdPeriodico = IdPeriodico,
    //                DataDiConsegna = DataDiConsegna,
    //                DataDiReso = DataDiReso,
    //                IdMagazine = IdMagazine,
    //                Numero = Numero,
    //                NumeroCopieRese = 0,
    //                NumeroCopieTotale = NumeroCopieTotale,
    //                NumeroCopieVendute = 0,
    //                Prezzo = Prezzo,
    //                ISSN = ISSN
    //            };

    //            _connection.Magazines.InsertOnSubmit(magazine);
    //            _connection.SubmitChanges();
    //            IdMagazine = magazine.IdMagazine;
    //        }
    //        catch { }
    //    }

    //    public void UpdateMagazine()
    //    {
    //        try
    //        {
    //            var magazine = GetMagazine().FirstOrDefault(p => p.IdMagazine == IdMagazine);

    //            magazine.DataDiConsegna = DataDiConsegna;
    //            magazine.DataDiReso = DataDiReso;
    //            magazine.IdPeriodico = IdPeriodico;
    //            magazine.IdTipologia = IdTipologia;
    //            magazine.ISSN = ISSN;
    //            magazine.Nome = Nome;
    //            magazine.Numero = Numero;
    //            magazine.NumeroCopieRese = NumeroCopieRese;
    //            magazine.NumeroCopieTotale = NumeroCopieTotale;
    //            magazine.NumeroCopieVendute = NumeroCopieVendute;

    //            _connection.SubmitChanges();
    //        }
    //        catch { }
    //    }

    //    public List<Dashboard> GetMagazineOverview()
    //    {
    //        return _connection.Dashboards.Where(p => p.Copie_presenti > 0).ToList();
    //    }
    //}
}

