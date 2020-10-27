using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class CategoryRepository : CostRegRepository, ICategoryRepository
    {
        public CategoryRepository(CostRegContext context) : base(context)
        {
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

        public async Task<int> GetIdOutOfCategoryName(string categoryName)
        {
            var retrievedData = await _context.Categories.FirstOrDefaultAsync(sn => sn.CategoryName == categoryName);
            return retrievedData.CategoryId;
        }
    }
}
