using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class CostPlanRepository : CostRegRepository, ICostPlansRepository
    {
        public CostPlanRepository(CostRegContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CostPlans>> GetCostPlanOfUser(int id)
        {
            var costPlanOfUser = await _context.CostPlans
                .Where(u => u.User.UserId == id)
                .Include(c => c.Category)
                .ToListAsync();

            return costPlanOfUser;
        }
    }
}
