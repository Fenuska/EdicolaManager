using System;
using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public partial class Cronologia
    {
        public static List<ViewHistory> GetHistoryBetweenDates(DateTime dtStart, DateTime dtEnd,
            int amountOfRecordsToTake, int amountOfRecordsToSkip)
        {
            List<ViewHistory> result = null;
            if (dtStart != null && dtEnd != null && amountOfRecordsToSkip >= 0 && amountOfRecordsToTake >= 0)
                using (var ctx = new DBLinqDataContext())
                {
                    result = ctx.ViewHistories.Where(p => p.Data.Date <= dtEnd.Date && 
                    p.Data.Date >= dtStart.Date)
                        .Skip(amountOfRecordsToSkip).Take(amountOfRecordsToTake).ToList();
                }

            return result;
        }

        public void CreateHistoryRecord()
        {
            using (var ctx = new DBLinqDataContext())
            {
                ctx.Cronologias.InsertOnSubmit(this);
                ctx.SubmitChanges();
            }
        }

        public static void CreateHistoryRecordOfAList(List<Cronologia> HistoryList)
        {
            if (HistoryList != null && HistoryList.Count != 0)
                using (var ctx = new DBLinqDataContext())
                {
                    ctx.Cronologias.InsertAllOnSubmit<Cronologia>(HistoryList);
                    ctx.SubmitChanges();
                }
        }
    }
}
