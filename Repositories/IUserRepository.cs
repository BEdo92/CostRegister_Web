using CostRegApp2.Data;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface IUserRepository : ICostRegRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);

        bool DeleteUser(int userId);
    }
}
