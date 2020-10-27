using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ICategoryRepository : ICostRegRepository
    {
        Task<int> GetIdOutOfCategoryName(string categoryName);
        string GetCategoryFromId(int id);
        Task<IEnumerable<Categories>> GetCategoriesAsync();
    }
}
