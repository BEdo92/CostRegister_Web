using CostRegApp2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shops>> GetShops();
        Task<int> GetIdOutOfShopName(string shopName);
    }
}
