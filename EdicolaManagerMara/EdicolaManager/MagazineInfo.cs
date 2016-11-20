using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdicolaManager
{
    public partial class Magazine
    {
        public static List<Dashboard> GetMagazineOverview()
        {
            List<Dashboard> result = null;
            using (var ctx = new DBLinqDataContext())
            {
                result = ctx.Dashboards.ToList();
            }
            return result;
        }
    }
}
