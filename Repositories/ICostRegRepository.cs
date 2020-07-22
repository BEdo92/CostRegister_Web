using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ICostRegRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetCostsOfUser(int id);
        Task<IEnumerable<User>> GetIncomeOfUser(int id);
        Task<IEnumerable<User>> GetCostPlanOfUser(int id);
        Task<IEnumerable<Categories>> GetCategories();
        Task<IEnumerable<Shops>> GetShops();
    }
}
