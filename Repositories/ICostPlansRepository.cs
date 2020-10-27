using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ICostPlansRepository : ICostRegRepository
    {
        Task<IEnumerable<CostPlans>> GetCostPlanOfUser(int id);
    }
}
