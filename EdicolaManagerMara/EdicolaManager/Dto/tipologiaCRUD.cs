using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public partial class Tipologia
    {
        public static List<Tipologia> getListaTipologia()
        {
            List<Tipologia> lista = new List<Tipologia>();

            using (var ctx = new DBLinqDataContext())
            {
                lista = ctx.Tipologias.ToList();
            }
            return lista;
        }
    }
}
