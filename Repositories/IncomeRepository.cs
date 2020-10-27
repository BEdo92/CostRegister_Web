using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class IncomeRepository : CostRegRepository, IIncomeRepository
    {
        public IncomeRepository(CostRegContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Income>> GetIncomeOfUser(int id)
        {
            var incomeOfUser = await _context.Income.Where(u => u.User.UserId == id).ToListAsync();

            return incomeOfUser;
        }
    }
}
