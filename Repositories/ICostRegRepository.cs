using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ICostRegRepository
    {
        void Add<T>(T entity);
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Costs>> GetCostsOfUser(int id);
        Task<IEnumerable<Income>> GetIncomeOfUser(int id);
        Task<IEnumerable<CostPlans>> GetCostPlanOfUser(int id);
        Task<IEnumerable<Categories>> GetCategories();
        Task<IEnumerable<Shops>> GetShops();
        Task<int> GetIdOutOfShopName(string shopName);
        Task<int> GetIdOutOfCategoryName(string categoryName);
       string GetCategoryFromId(int id);
    }
}
