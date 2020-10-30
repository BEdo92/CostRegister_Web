using CostRegApp2.Data;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CostRegContext _context;
        private ICategoryRepository _categoryRepository;
        private IShopRepository _shopRepository;
        private ICostRepository _costRepository;
        private IIncomeRepository _incomeRepository;
        private ICostPlansRepository _costPlanRepository;
        private IUserRepository _userRepository;
        private ISettingsRepository _settingsRepository;

        public UnitOfWork(CostRegContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        public IShopRepository ShopRepository => _shopRepository ??= new ShopRepository(_context);

        public ICostRepository CostRepository => _costRepository ??= new CostRepository(_context);

        public IIncomeRepository IncomeRepository => _incomeRepository ??= new IncomeRepository(_context);

        public ICostPlansRepository CostPlansRepository => _costPlanRepository ??= new CostPlanRepository(_context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public ISettingsRepository SettingsRepository => _settingsRepository ??= new SettingsRepository(_context);

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
