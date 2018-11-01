using System;
using System.Data.Entity;
using System.Linq;

namespace EdicolaManager.Models
{
    public class DashboardModel : IModel<Dashboard>
    {
        readonly DBLinqDataContext _connection;

        public DashboardModel(DBLinqDataContext connection)
        {
            _connection = connection;
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dashboard> Get()
        {
            return _connection.Dashboards.AsNoTracking();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dashboard> GetMagazineOverview()
        {

            return Get().Where(p => p.Copie_presenti > 0);
        }
    }
}

