using EdicolaManagerBL.Architecture.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager.Models
{
    public class CronologiaDto
    {
        private readonly RepositoryContext _connessione;

        public DateTime Data;
        public int IdRivista;
        public int IdPeriodico;
        public int QuantitaResi;
        public int QuantitaVenduti;

        public CronologiaDto()
        {
        }

        public CronologiaDto(RepositoryContext connection)
        {
            _connessione = connection;
        }

        //public List<ViewHistory> GetHistoryBetweenDates(DateTime dtStart, DateTime dtEnd,
        //    int amountOfRecordsToTake, int amountOfRecordsToSkip)
        //{
        //    List<ViewHistory> result = null;
        //    if (dtStart != null && dtEnd != null && amountOfRecordsToSkip >= 0 && amountOfRecordsToTake >= 0)
        //    {
        //        result = _connection.ViewHistories.Where(p => p.Data.Date <= dtEnd.Date &&
        //        p.Data.Date >= dtStart.Date)
        //            .Skip(amountOfRecordsToSkip).Take(amountOfRecordsToTake).ToList();
        //    }

        //    return result;
        //}

        public void StoricizzaVendita(RivistaDto rivista, int quantita)
        {
            IdRivista = rivista.IdRivista;
            IdPeriodico = rivista.IdPeriodico;
            QuantitaVenduti = quantita;
            Data = DateTime.Now;

            Salva();
        }

        public void StoricizzaReso(RivistaDto rivista, int quantitaDiResi)
        {
            IdRivista = rivista.IdRivista;
            IdPeriodico = rivista.IdPeriodico;
            QuantitaResi = quantitaDiResi;
            Data = DateTime.Now;

            Salva();
        }

        public void Salva()
        {
            _connessione.Cronologia.Add(this);
            _connessione.SaveChanges();
        }

        public void CreateHistoryRecordOfAList(List<CronologiaDto> historyList)
        {
            if (historyList.Any())
            {
                var cronologia = historyList.Select(p => new CronologiaDto
                {
                    Data = p.Data,
                    IdRivista = p.IdRivista,
                    IdPeriodico = p.IdPeriodico,
                    QuantitaResi = p.QuantitaResi,
                    QuantitaVenduti = p.QuantitaVenduti
                });

                _connessione.Cronologia.AddRange(cronologia);
                _connessione.SaveChanges();
            }
        }
    }
}
