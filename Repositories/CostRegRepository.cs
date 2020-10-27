using CostRegApp2.Data;

namespace CostRegApp2.Repositories
{
    public class CostRegRepository : ICostRegRepository
    {
        protected readonly CostRegContext _context;

        public CostRegRepository(CostRegContext context)
        {
            _context = context;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }
    }
}
