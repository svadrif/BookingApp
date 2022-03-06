using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> GetAll(bool tracking);
        Task<T> GetByIdAsync(Guid id);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        IQueryable<T> Search(Expression<Func<T, bool>> predicate, bool tracking);
        Task<int> SaveChangesAsync();
    }
}
