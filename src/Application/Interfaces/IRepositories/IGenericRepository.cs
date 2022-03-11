using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> GetAll(bool tracking);
        Task<T> GetByIdAsync(Guid id);
        void Update(T entity);
        void Remove(T entity);
        IQueryable<T> Search(Expression<Func<T, bool>> predicate, bool tracking);
    }
}
