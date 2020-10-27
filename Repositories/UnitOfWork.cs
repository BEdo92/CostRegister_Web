using CostRegApp2.Data;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CostRegContext _context;

        public UnitOfWork(CostRegContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public IShopRepository ShopRepository => new ShopRepository(_context);

        public ICostRepository CostRepository => new CostRepository(_context);

        public IIncomeRepository IncomeRepository => new IncomeRepository(_context);

        public ICostPlansRepository CostPlansRepository => new CostPlanRepository(_context);

        public IAuthRepository AuthRepository => new AuthRepository(_context);

        public IUserRepository UserRepository => new UserRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasAnyChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
