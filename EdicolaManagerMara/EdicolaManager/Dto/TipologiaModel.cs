using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public class TipologiaModel
    {
        private readonly DBLinqDataContext _connection;

        public TipologiaModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public List<Tipologia> GetListaTipologia()
        {
            return _connection.Tipologias.ToList();
        }
    }
}
