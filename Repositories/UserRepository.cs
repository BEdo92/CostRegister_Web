using CostRegApp2.Data;
using System.Linq;

namespace CostRegApp2.Repositories
{
    public class UserRepository : CostRegRepository, IUserRepository
    {
        public UserRepository(CostRegContext context) : base(context)
        {
        }

        public bool DeleteUser(int userId)
        {
            var deletedEntity = _context.Remove(_context.Users.FirstOrDefault(user => user.UserId == userId));
            return deletedEntity != null;
        }
    }
}
