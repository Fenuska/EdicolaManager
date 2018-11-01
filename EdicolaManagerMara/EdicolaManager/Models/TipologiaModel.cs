using System.Linq;

namespace EdicolaManager.Models
{
    public class TipologiaModel : IModel<Tipologia>
    {
        private readonly DBLinqDataContext _connection;

        public TipologiaModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public bool Create()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Tipologia> Get()
        {
            return _connection.Tipologias;
        }

        public bool Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
