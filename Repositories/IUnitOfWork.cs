using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IShopRepository ShopRepository { get; }
        ICostRepository CostRepository { get; }
        IIncomeRepository IncomeRepository { get; }
        ICostPlansRepository CostPlansRepository { get; }
        IUserRepository UserRepository { get; }

        Task<bool> Complete();
        bool HasAnyChanges();
    }
}
