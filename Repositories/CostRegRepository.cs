using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class CostRegRepository : ICostRegRepository
    {
        private readonly CostRegContext _context;

        public CostRegRepository(CostRegContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetCostsOfUser(int id)
        {
            var user = await _context.Users.Include(u => u.Costs)
                                           .Where(i => i.UserId == id)
                                           .ToListAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetIncomeOfUser(int id)
        {
            var user = await _context.Users.Include(u => u.Income)
                                           .Where(i => i.UserId == id)
                                           .ToListAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetCostPlanOfUser(int id)
        {
            var user = await _context.Users.Include(u => u.CostPlans)
                                           .Where(i => i.UserId == id)
                                           .ToListAsync();
            return user;
        }

        public async Task<IEnumerable<Categories>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<Shops>> GetShops()
        {
            var shops = await _context.Shops.ToListAsync();

            return shops;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
