using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class ShopRepository : CostRegRepository, IShopRepository
    {
        public ShopRepository(CostRegContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shops>> GetShops()
        {
            var shops = await _context.Shops.ToListAsync();

            return shops;
        }

        public async Task<int> GetIdOutOfShopName(string shopName)
        {
            var retrievedData = await _context.Shops.FirstOrDefaultAsync(sn => sn.ShopName == shopName);
            return retrievedData.ShopId;
        }
    }
}
