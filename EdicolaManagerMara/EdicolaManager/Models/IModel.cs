using System.Linq;

namespace EdicolaManager.Models
{
    public interface IModel<T> where T : class
    {
        IQueryable<T> Get();
        bool Create();
        bool Update();
    }
}
