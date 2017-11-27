using System.Collections.Generic;
using System.Linq;

namespace EdicolaManager
{
    public partial class Periodico
    {
        public static List<Periodico> getListaPeriodici()
        {
            List<Periodico> lista = new List<Periodico>();

            using (DBLinqDataContext ctx = new DBLinqDataContext())
            {
                lista = ctx.Periodicos.ToList();
            }
            return lista;
        }

        public void createPeriodico()
        {
            try
            {
                using (DBLinqDataContext ctx = new DBLinqDataContext())
                {
                    var temp = new Periodico()
                    {
                        Nome = this.Nome,
                    };

                    ctx.Periodicos.InsertOnSubmit(temp);
                    ctx.SubmitChanges();
                    this.IdPeriodico = temp.IdPeriodico;
                }
            }
            catch { }
        }

        public void updatePeriodico()
        {
            try
            {
                using (var ctx = new DBLinqDataContext())
                {
                    var temp = ctx.Periodicos.Where(p => p.IdPeriodico == this.IdPeriodico).SingleOrDefault();
                    temp.Nome = this.Nome;
                    ctx.SubmitChanges();
                }
            }
            catch { }
        }
    }
}
