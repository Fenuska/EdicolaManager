using System;
using System.Linq;

namespace EdicolaManager.Models
{
    public class PeriodicoModel : IModel<Periodico>
    {
        public string Nome;
        public int IdPeriodico;
        readonly DBLinqDataContext _connection;

        public PeriodicoModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public IQueryable<Periodico> Get()
        {
            return _connection.Periodicos;
        }

        public bool Create()
        {
            bool result;
            try
            {
                var periodico = new Periodico
                {
                    Nome = this.Nome,
                };

                _connection.Periodicos.InsertOnSubmit(periodico);
                _connection.SubmitChanges();
                IdPeriodico = periodico.IdPeriodico;
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool Update()
        {
            bool result;
            try
            {
                var periodico = Get().Where(p => p.IdPeriodico == IdPeriodico).FirstOrDefault();
                periodico.Nome = Nome;
                _connection.SubmitChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
