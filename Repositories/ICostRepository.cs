using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ICostRepository : ICostRegRepository
    {
        Task<IEnumerable<Costs>> GetCostsOfUser(int id);
        Task<IEnumerable<Costs>> GetCostsOfUser(int id, bool showAllRows);
    }
}
