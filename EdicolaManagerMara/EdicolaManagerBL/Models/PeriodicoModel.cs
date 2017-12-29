using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager.Models
{
    public class PeriodicoModel
    {
        //public string Nome;
        //public int IdPeriodico;
        //private readonly DBLinqDataContext _connection;

        //public PeriodicoModel(DBLinqDataContext connection)
        //{
        //    _connection = connection;
        //}

        //public List<Periodico> GetListaPeriodici()
        //{
        //    return _connection.Periodicos.ToList();
        //}

        //public void CreatePeriodico()
        //{
        //    try
        //    {
        //        var temp = new Periodico
        //        {
        //            Nome = this.Nome,
        //        };

        //        _connection.Periodicos.InsertOnSubmit(temp);
        //        _connection.SubmitChanges();
        //        this.IdPeriodico = temp.IdPeriodico;
        //    }
        //    catch { }
        //}

        //public void UpdatePeriodico()
        //{
        //    try
        //    {
        //        var temp = _connection.Periodicos.Where(p => p.IdPeriodico == this.IdPeriodico).FirstOrDefault();
        //        temp.Nome = this.Nome;
        //        _connection.SubmitChanges();
        //    }
        //    catch { }
        //}
    }
}
