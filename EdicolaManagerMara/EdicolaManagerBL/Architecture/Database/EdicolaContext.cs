using EdicolaManager.Models;
using System.Data.Entity;

namespace EdicolaManagerBL.Architecture.Database
{
    public class RepositoryContext : DbContext, IRepository
    {
        public RepositoryContext() : base()
        {

        }

        public DbSet<RivistaDto> Magazine { get; set; }
        public DbSet<CronologiaDto> Cronologia { get; set; }

    }
}
