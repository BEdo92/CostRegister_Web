namespace CostRegApp2.Repositories
{
    public interface ICostRegRepository
    {
        void Add<T>(T entity);
        void Delete<T>(T entity) where T : class;
    }
}
