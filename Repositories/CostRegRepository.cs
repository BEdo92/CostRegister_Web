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


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Costs>> GetCostsOfUser(int id)
        {
            var costsOfUser = _context.Costs.Where(u => u.User.UserId == id)
                .Include(c => c.Category)
                .Include(s => s.Shop)
                .ToListAsync();

            return await costsOfUser;
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public async Task<IEnumerable<Income>> GetIncomeOfUser(int id)
        {
            var incomeOfUser = await _context.Income.Where(u => u.User.UserId == id).ToListAsync();

            return incomeOfUser;
        }

        public async Task<IEnumerable<CostPlans>> GetCostPlanOfUser(int id)
        {
            var costPlanOfUser = await _context.CostPlans
                .Where(u => u.User.UserId == id)
                .Include(c => c.Category)
                .ToListAsync();

            return costPlanOfUser;
        }

        public async Task<IEnumerable<Categories>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories;
        }

        public string GetCategoryFromId(int id)
        {
            var categories = _context.Categories.ToList();

            return categories.FirstOrDefault(c => c.CategoryId == id).CategoryName;
        }

        public async Task<IEnumerable<Shops>> GetShops()
        {
            var shops = await _context.Shops.ToListAsync();

            return shops;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<int> GetIdOutOfShopName(string shopName)
        {
            var retrievedData = await _context.Shops.FirstOrDefaultAsync(sn => sn.ShopName == shopName);
            return retrievedData.ShopId;
        }

        public async Task<int> GetIdOutOfCategoryName(string categoryName)
        {
            var retrievedData = await _context.Categories.FirstOrDefaultAsync(sn => sn.CategoryName == categoryName);
            return retrievedData.CategoryId;
        }
    }
}
