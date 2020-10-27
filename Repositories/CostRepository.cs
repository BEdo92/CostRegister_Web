using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class CostRepository : CostRegRepository, ICostRepository
    {
        public CostRepository(CostRegContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Costs>> GetCostsOfUser(int id)
        {
            return await GetCostsOfUser(id, true);
        }

        public async Task<IEnumerable<Costs>> GetCostsOfUser(int id, bool showAllRows)
        {
            List<Costs> costsOfUser = new List<Costs>();

            if (showAllRows)
            {
                costsOfUser = await _context.Costs.Where(u => u.User.UserId == id)
                                                  .Include(c => c.Category)
                                                  .Include(s => s.Shop)
                                                  .OrderByDescending(order => order.DateOfCost)
                                                  .ToListAsync();
            }
            else
            {
                costsOfUser = await _context.Costs.Where(u => u.User.UserId == id)
                                                  .Include(c => c.Category)
                                                  .Include(s => s.Shop)
                                                  .OrderByDescending(order => order.DateOfCost)
                                                  .Take(10)
                                                  .ToListAsync();

            }

            return costsOfUser;
        }
    }
}
