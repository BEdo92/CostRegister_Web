using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface IIncomeRepository : ICostRegRepository
    {
        Task<IEnumerable<Income>> GetIncomeOfUser(int id);
    }
}
