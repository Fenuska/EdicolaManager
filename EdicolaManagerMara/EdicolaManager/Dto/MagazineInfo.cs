using System.Collections.Generic;
using System.Linq;

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
