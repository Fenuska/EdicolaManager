using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public class CronologiaModel
    {
        private readonly DBLinqDataContext _connection;

        public DateTime Data;
        public int IdMagazine;
        public int IdPeriodico;
        public int NumeroMagazineResi;
        public int NumeroMagazineVenduti;

        public CronologiaModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public List<ViewHistory> GetHistoryBetweenDates(DateTime dtStart, DateTime dtEnd,
            int amountOfRecordsToTake, int amountOfRecordsToSkip)
        {
            List<ViewHistory> result = null;
            if (dtStart != null && dtEnd != null && amountOfRecordsToSkip >= 0 && amountOfRecordsToTake >= 0)
            {
                result = _connection.ViewHistories.Where(p => p.Data.Date <= dtEnd.Date &&
                p.Data.Date >= dtStart.Date)
                    .Skip(amountOfRecordsToSkip).Take(amountOfRecordsToTake).ToList();
            }

            return result;
        }

        public void CreateHistoryRecord()
        {
            var historyDto = new Cronologia
            {
                Data = Data,
                IdMagazine = IdMagazine,
                IdPeriodico = IdPeriodico,
                NumeroMagazineResi = NumeroMagazineResi,
                NumeroMagazineVenduti = NumeroMagazineVenduti
            };

            _connection.Cronologias.InsertOnSubmit(historyDto);
            _connection.SubmitChanges();
        }

        public void CreateHistoryRecordOfAList(List<CronologiaModel> historyList)
        {
            if (historyList.Any())
            {
                var historyDto = historyList.Select(p => new Cronologia
                {
                    Data = p.Data,
                    IdMagazine = p.IdMagazine,
                    IdPeriodico = p.IdPeriodico,
                    NumeroMagazineResi = p.NumeroMagazineResi,
                    NumeroMagazineVenduti = p.NumeroMagazineVenduti
                });

                _connection.Cronologias.InsertAllOnSubmit<Cronologia>(historyDto);
                _connection.SubmitChanges();
            }
        }
    }
}
